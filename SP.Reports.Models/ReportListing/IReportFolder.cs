namespace SP.Reports.Models.ReportListing
{
    public interface IReportFolder
    {
        HashSet<ReportName> Name { get; set; }
        string Order { get; set; }
    }
}