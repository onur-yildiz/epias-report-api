using SP.Settings.Models;

namespace SP.User.Service
{
    public interface ISettingsService
    {
        ReportListContainer GetReportListingInfo();
    }
}