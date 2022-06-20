using SP.Reports.Models;
using SP.Reports.Models.ReportListing;
using SP.Reports.Models.RequestBody;

namespace SP.Reports.Service
{
    public interface IReportsService
    {
        Task<T?> GetData<T, V>(object? r, string endpoint) where T : class where V : IResponseBase<T>;
        IEnumerable<IReport>? GetReports();
        void UpdateRoles(string reportKey, IUpdateReportRolesRequestBody r);
        void UpdateIsActive(string reportKey, IUpdateReportIsActiveRequestBody r);
    }
}