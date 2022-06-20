namespace SP.Reports.Models.Api
{
    public interface IApiSettings
    {
        string BaseUrl { get; set; }
        ApiPaths? Paths { get; set; }
    }
}