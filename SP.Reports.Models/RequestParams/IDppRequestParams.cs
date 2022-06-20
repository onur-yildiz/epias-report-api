namespace SP.Reports.Models.RequestParams
{
    public interface IDppRequestParams: IDateIntervalRequestParams
    {
        string? OrganizationEIC { get; set; }

        string? UevcbEIC { get; set; }
    }
}