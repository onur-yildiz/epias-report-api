namespace SP.Reports.Models.Api
{
    public class ApiSettings
    {
        public ApiSettings(string baseUrl, ApiPaths? paths)
        {
            BaseUrl = baseUrl;
            Paths = paths;
        }

        public string BaseUrl { get; set; }
        public ApiPaths? Paths { get; set; }
    }
}