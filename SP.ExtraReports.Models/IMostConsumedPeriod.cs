namespace SP.ExtraReports.Models
{
    /// <summary>
    /// Period which has consumed the most electricity
    /// </summary>
    public interface IMostConsumedPeriod
    {
        /// <summary>
        /// Date
        /// </summary>
        double Consumption { get; set; }

        /// <summary>
        /// Consumption value
        /// </summary>
        string Date { get; set; }

        /// <summary>
        /// Ratio of consumption compared to whole period
        /// </summary>
        double Ratio { get; set; }
    }
}