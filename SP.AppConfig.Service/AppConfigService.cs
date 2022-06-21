using MongoDB.Driver;
using SP.Reports.Models.ReportListing;
using SP.Users.Models;
using SP.Utils.Jwt;

namespace SP.AppConfig.Service
{
    public class AppConfigService : IAppConfigService
    {
        readonly IJwtUtils _jwtUtils;
        readonly IMongoCollection<Account> _users;
        readonly IMongoCollection<Report> _reports;
        readonly IMongoCollection<ReportFolder> _reportFolders;

        public AppConfigService(IHttpClientFactory httpClientFactory, IMongoClient client, IJwtUtils jwtUtils)
        {
            var db = client.GetDatabase("cluster0");
            this._users = db.GetCollection<Account>("users");
            this._reports = db.GetCollection<Report>("reports");
            this._reportFolders = db.GetCollection<ReportFolder>("report-folders");
            _jwtUtils = jwtUtils;
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

    }
}