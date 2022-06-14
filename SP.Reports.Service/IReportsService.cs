using SP.Reports.Models;
using SP.Reports.Models.Dpp;
using SP.Reports.Models.DppInjectionUnitName;
using SP.Reports.Models.IdmVolume;
using SP.Reports.Models.IntraDayAof;
using SP.Reports.Models.McpSmps;
using SP.Reports.Models.Organizations;
using SP.Reports.Models.RealTimeGeneration;
using SP.Reports.Models.ReportListing;
using SP.Reports.Models.RequestParams;
using SP.Reports.Models.Smp;

namespace SP.Reports.Service
{
    public interface IReportsService
    {
        Task<T?> GetData<T, V>(object? r, string endpoint) where T : class where V : IResponseBase<T>;
        IEnumerable<dynamic>? GetReportListing(string? authToken = null);
        IEnumerable<Report>? GetReports();
        void UpdateRoles(UpdateReportRolesRequestParams r);
        void UpdateIsActive(UpdateReportIsActiveRequestParams r);
    }
}