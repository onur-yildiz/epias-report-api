namespace SP.AppConfig.Service
{
    public interface IAppConfigService
    {
        IEnumerable<dynamic>? GetReportListing(string? authToken = null);
    }
}