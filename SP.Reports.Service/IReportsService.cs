using SP.Reports.Models;
using SP.Reports.Models.ReportListing;
using SP.Reports.Models.RequestBody;

namespace SP.Reports.Service
{
    public interface IReportsService
    {
        Task<T?> GetData<T, V>(object? r, string endpoint) where T : class where V : IResponseBase<T>;
        IEnumerable<Report>? GetReports();
        void UpdateRoles(string reportKey, UpdateReportRolesRequestBody r);
        void UpdateIsActive(string reportKey, UpdateReportIsActiveRequestBody r);
    }
}