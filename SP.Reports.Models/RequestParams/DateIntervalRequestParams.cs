namespace SP.Reports.Models.RequestParams
{
    public class DateIntervalRequestParams : IDateIntervalRequestParams
    {
        public string StartDate { get; set; } = string.Empty;
        public string EndDate { get; set; } = string.Empty;
    }
}