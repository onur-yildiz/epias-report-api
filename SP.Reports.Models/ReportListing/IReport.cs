namespace SP.Reports.Models.ReportListing
{
    public interface IReport
    {
        /// <summary>
        /// Report's active state
        /// </summary>
        bool IsActive { get; set; }

        /// <summary>
        /// Report's unique key
        /// </summary>
        string Key { get; set; }

        /// <summary>
        /// Report name
        /// </summary>
        HashSet<ReportName> Name { get; set; }

        /// <summary>
        /// Report's location in menu hierarchy
        /// </summary>
        string Order { get; set; }

        /// <summary>
        /// Roles which can access this report.
        /// </summary>
        HashSet<string> Roles { get; set; }
    }
}