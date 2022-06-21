namespace SP.ExtraReports.Models
{
    public interface IMostConsumedPeriod
    {
        double Consumption { get; set; }

        string Date { get; set; }

        double Ratio { get; set; }
    }
}