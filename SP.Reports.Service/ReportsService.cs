using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using SP.Exceptions;
using SP.Reports.Models;
using SP.Reports.Models.Api;
using SP.Reports.Models.ReportListing;
using SP.Reports.Models.RequestParams;
using SP.User.Models;
using SP.Utils.Jwt;

namespace SP.Reports.Service
{
    public class ReportsService : IReportsService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        readonly IJwtUtils _jwtUtils;
        readonly IMongoCollection<Account> _users;
        readonly IMongoCollection<Report> _reports;
        readonly IMongoCollection<ReportFolder> _reportFolders;

        public ReportsService(IHttpClientFactory httpClientFactory, IMongoClient client, IJwtUtils jwtUtils)
        {
            var db = client.GetDatabase("cluster0");
            this._users = db.GetCollection<Account>("users");
            this._reports = db.GetCollection<Report>("reports");
            this._reportFolders = db.GetCollection<ReportFolder>("report-folders");
            _httpClientFactory = httpClientFactory;
            _jwtUtils = jwtUtils;
        }

        public async Task<TBody?> GetData<TBody, TContainer>(object? r, string endpoint) where TBody : class where TContainer : IResponseBase<TBody>
        {
            var httpClient = _httpClientFactory.CreateClient("EpiasAPI");
            var query = r == null ? "" : ReportsResponseUtils.GenerateQueryFromObject(r);
            var response = await httpClient.GetAsync(endpoint + query);
            response.EnsureSuccessStatusCode();
            return await ReportsResponseUtils.ExtractBody<TBody, TContainer>(response);
        }

        public IEnumerable<dynamic>? GetReportListing(string? authToken = null)
        {
            var userId = authToken != null ? _jwtUtils.ValidateToken(authToken) : null;
            var user = _users.Find(u => u.Id == userId).FirstOrDefault();

            var reports = _reports.Find(r => r.IsActive).ToEnumerable().Where(r => user?.IsAdmin == true || r.Roles.Count == 0 || r.Roles.Any(role => user?.Roles.Contains(role) == true));
            var reportFolders = _reportFolders.Find(_ => true).ToEnumerable();

            var listingInfo = new List<dynamic>(reports.Count() + reportFolders.Count());
            listingInfo.AddRange(reports);
            listingInfo.AddRange(reportFolders);
            return listingInfo;
        }

        public IEnumerable<Report>? GetReports()
        {
            return _reports.Find(_ => true).ToEnumerable();
        }

        public void UpdateRoles(UpdateReportRolesRequestParams r)
        {
            var update = Builders<Report>.Update.Set("roles", r.Roles);
            var result = _reports.UpdateOne(report => report.Key == r.Key, update);

            if (!result.IsAcknowledged)
                throw new HttpResponseException(StatusCodes.Status502BadGateway, new { message = "Could not assign role." });
        }

        public void UpdateIsActive(UpdateReportIsActiveRequestParams r)
        {
            var update = Builders<Report>.Update.Set("isActive", r.IsActive);
            var result = _reports.UpdateOne(report => report.Key == r.Key, update);

            if (!result.IsAcknowledged)
                throw new HttpResponseException(StatusCodes.Status502BadGateway, new { message = "Could not update active state." });
        }
    }
}