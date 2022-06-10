using SP.Settings.Models;

namespace SP.User.Service
{
    public interface ISettingsService
    {
        IEnumerable<ReportHierarchyItem>? GetReportListingInfo(string? authToken = null);
        Role? GetRole(string role);
        void CreateRole(Role role);
        IEnumerable<Role>? GetRoles();
    }
}