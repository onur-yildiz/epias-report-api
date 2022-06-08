namespace SP.Reports.Models.Api
{
    public class ApiSettings
    {
        public string BaseUrl { get; set; } = String.Empty;
        public ApiPaths? Paths { get; set; }
    }
}