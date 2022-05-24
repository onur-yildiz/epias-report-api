using EpiasReportHttp.Api;

namespace EpiasReportHttp
{
    public class ApiSettings
    {
        public string BaseUrl { get; set; } = String.Empty;
        public ApiPaths Paths { get; set; } = new ApiPaths();
    }
}