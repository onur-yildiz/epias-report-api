namespace SP.Reports.Models.RequestParams
{
    public class IdmVolumeSummaryRequestParams : IDateIntervalRequestParams
    {
        public string StartDate { get; set; } = String.Empty;
        public string EndDate { get; set; } = String.Empty;
        public string Period { get; set; } = String.Empty;
    }
}
