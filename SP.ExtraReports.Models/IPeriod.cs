namespace SP.ExtraReports.Models
{
    public interface IPeriod
    {
        DateTime End { get; set; }
        DateTime Start { get; set; }
    }
}