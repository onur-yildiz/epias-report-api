using Moq;
using SP.DAL.Interfaces;

namespace SP.Reports.Service.Tests.Mocks
{
    internal class DependencyMocks
    {
        public Mock<IReportRepository> ReportRepository = new();
        public Mock<IHttpClientFactory> HttpClientFactory = new();
        public Mock<HttpClient> HttpClient = new();
    }
}
