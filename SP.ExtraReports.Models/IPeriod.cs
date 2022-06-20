namespace SP.ExtraReports.Models
{
    /// <summary>
    /// Time period
    /// </summary>
    public interface IPeriod
    {
        /// <summary>
        /// Start date of the period (inclusive)
        /// </summary>
        DateTime End { get; set; }

        /// <summary>
        /// End date of the period (inclusive)
        /// </summary>
        DateTime Start { get; set; }
    }
}