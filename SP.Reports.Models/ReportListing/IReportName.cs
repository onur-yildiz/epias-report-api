namespace SP.Reports.Models.ReportListing
{
    public interface IReportName
    {
        /// <summary>
        /// Language code
        /// </summary>
        string Lang { get; set; }

        /// <summary>
        /// Long name
        /// </summary>
        string Long { get; set; }

        /// <summary>
        /// Short name
        /// </summary>
        string Short { get; set; }
    }
}