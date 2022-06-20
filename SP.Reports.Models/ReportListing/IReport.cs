namespace SP.Reports.Models.ReportListing
{
    public interface IReport
    {
        string Endpoint { get; set; }
        bool IsActive { get; set; }
        string Key { get; set; }
        HashSet<ReportName> Name { get; set; }
        string Order { get; set; }
        HashSet<string> Roles { get; set; }
    }
}