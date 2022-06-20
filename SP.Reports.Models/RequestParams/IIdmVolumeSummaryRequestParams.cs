namespace SP.Reports.Models.RequestParams
{
    public interface IIdmVolumeSummaryRequestParams : IDateIntervalRequestParams
    {
        string Period { get; set; }
    }
}