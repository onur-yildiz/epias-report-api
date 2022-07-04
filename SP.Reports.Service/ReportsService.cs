using MongoDB.Driver;
using SP.DAL.Interfaces;
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
        readonly IReportRepository _reportRepository;

        public ReportsService(IHttpClientFactory httpClientFactory, IReportRepository reportRepository)
        {
            _reportRepository = reportRepository;
            _httpClientFactory = httpClientFactory;
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
            return _reportRepository.Get();
        }

        public Report GetReportByKey(string key)
        {
            var report = _reportRepository.GetFirst(r => r.Key == key);
            if (report == null) throw HttpResponseException.NotExists("Report");
            return report;
        }

        public void UpdateRoles(string reportKey, IUpdateReportRolesRequestBody r)
        {
            var report = _reportRepository.UpdateOne_Set(report => report.Key == reportKey, "roles", r.Roles);

            if (report == null)
                throw HttpResponseException.DatabaseError();
        }

        public void UpdateIsActive(string reportKey, IUpdateReportIsActiveRequestBody r)
        {
            var report = _reportRepository.UpdateOne_Set(report => report.Key == reportKey, "isActive", r.IsActive);

            if (report == null)
                throw HttpResponseException.DatabaseError();
        }
    }
}