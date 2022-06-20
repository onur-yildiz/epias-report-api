namespace SP.ExtraReports.Models
{
    public interface IConsumptionStatistics
    {
        IEnumerable<MostConsumedPeriod> MostConsumedDays { get; set; }
        IEnumerable<MostConsumedPeriod> MostConsumedHours { get; set; }
    }
}