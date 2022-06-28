using Microsoft.AspNetCore.Http;
using MongoDB.Driver;
using SP.Exceptions;
using SP.Reports.Models;
using SP.Reports.Models.ReportListing;
using SP.Reports.Models.RequestBody;
using SP.Users.Models;
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

        public IEnumerable<Report> GetReports()
        {
            return _reports.Find(_ => true).ToEnumerable();
        }

        public void UpdateRoles(string reportKey, IUpdateReportRolesRequestBody r)
        {
            var update = Builders<Report>.Update.Set("roles", r.Roles);
            var result = _reports.UpdateOne(report => report.Key == reportKey, update);

            if (!result.IsAcknowledged)
                throw HttpResponseException.DatabaseError("Could not assign role.");
        }

        public void UpdateIsActive(string reportKey, IUpdateReportIsActiveRequestBody r)
        {
            var update = Builders<Report>.Update.Set("isActive", r.IsActive);
            var result = _reports.UpdateOne(report => report.Key == reportKey, update);

            if (!result.IsAcknowledged)
                throw HttpResponseException.DatabaseError("Could not update active state.");
        }
    }
}