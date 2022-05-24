namespace SP.Reports.Models.RequestParams
{
    public class DppRequestParams : IDateIntervalRequestParams
    {
        public string EndDate { get; set; } = String.Empty;
        public string StartDate { get; set; } = String.Empty;
        public string OrganizationEIC { get; set; } = String.Empty;
        public string UevcbEIC { get; set; } = String.Empty;
    }
}
