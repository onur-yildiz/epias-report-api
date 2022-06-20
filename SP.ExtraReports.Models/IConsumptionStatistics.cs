namespace SP.ExtraReports.Models
{
    /// <summary>
    /// Consumption statistics of a month for the periods which consumed the most electricity Includes hour with the highest consume for each day and top 5 electricity consumed days. 
    /// </summary>
    public interface IConsumptionStatistics
    {
        /// <summary>
        /// Date range of the statistics
        /// </summary>
        Period Period { get; set; }

        /// <summary>
        /// List of hours which the electricity is consumed the most that day for each day in the month
        /// </summary>
        IEnumerable<MostConsumedPeriod> MostConsumedDays { get; set; }

        /// <summary>
        /// Top 5 days which the electricity is consumed the most for the month
        /// </summary>
        IEnumerable<MostConsumedPeriod> MostConsumedHours { get; set; }
    }
}