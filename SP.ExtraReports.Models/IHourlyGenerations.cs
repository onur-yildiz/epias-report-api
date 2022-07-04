namespace SP.ExtraReports.Models
{
    /// <summary>
    /// Hourly electric generation values categorized by renewable and non-renewable energy types.
    /// </summary>
    public interface IHourlyGenerations
    {
        /// <summary>
        /// Date
        /// </summary>
        DateTime Date { get; set; }

        /// <summary>
        /// Import/export values from unknown sources
        /// </summary>
        double ImportExport { get; set; }

        /// <summary>
        /// Non-renewable energy sources
        /// </summary>
        NonRenewable NonRenewable { get; set; }

        /// <summary>
        /// Total generated non-renewable electricity
        /// </summary>
        double NonRenewableTotal { get; set; }

        /// <summary>
        /// Renewable energy sources
        /// </summary>
        Renewable Renewable { get; set; }

        /// <summary>
        /// Total generated renewable electricity
        /// </summary>
        double RenewableTotal { get; set; }

        /// <summary>
        /// Total generated electricity
        /// </summary>
        double Total { get; set; }
    }
}