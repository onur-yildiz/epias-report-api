using MongoDB.Bson;
using MongoDB.Driver;
using SP.DAL.Interfaces;
using SP.Utils.Jwt;

namespace SP.AppConfig.Service
{
    public class AppConfigService : IAppConfigService
    {
        readonly IJwtUtils _jwtUtils;
        readonly IUserRepository _userRepository;
        readonly IReportRepository _reportRepository;
        readonly IReportFolderRepository _reportFolderRepository;

        public AppConfigService(IUserRepository userRepository, IReportRepository reportRepository, IReportFolderRepository reportFolderRepository, IJwtUtils jwtUtils)
        {
            _userRepository = userRepository;
            _reportRepository = reportRepository;
            _reportFolderRepository = reportFolderRepository;
            _jwtUtils = jwtUtils;
        }

        public IEnumerable<dynamic>? GetReportListing(string? authToken = null)
        {
            var userId = authToken == null ? null : _jwtUtils.ValidateToken(authToken);
            var user = userId == null ? null : _userRepository.GetById((ObjectId)userId);

            var reports = _reportRepository.Get(r => r.IsActive).Where(r => user?.IsAdmin == true || r.Roles.Count == 0 || r.Roles.Any(role => user?.Roles.Contains(role) == true));
            var reportFolders = _reportFolderRepository.Get();

            var reportListing = new List<dynamic>(reports.Count() + reportFolders.Count());
            reportListing.AddRange(reports);
            reportListing.AddRange(reportFolders);
            return reportListing;
        }

    }
}