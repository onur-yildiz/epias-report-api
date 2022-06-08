using Microsoft.Extensions.Options;
using SP.Reports.Models;
using SP.Reports.Models.Api;

namespace SP.Reports.Service
{
    public class ReportsService : IReportsService
    {
        private readonly ApiPaths _paths;
        private readonly IHttpClientFactory _httpClientFactory;

        public ReportsService(IOptions<ApiPaths> option, IHttpClientFactory httpClientFactory)
        {
            _paths = option.Value;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<TBody?> GetData<TBody, TContainer>(object? r, string endpoint) where TBody : class where TContainer : IResponseBase<TBody>
        {
            var httpClient = _httpClientFactory.CreateClient("EpiasAPI");
            var query = r == null ? "" : ReportsResponseUtils.GenerateQueryFromObject(r);
            var response = await httpClient.GetAsync(endpoint + query);
            response.EnsureSuccessStatusCode();
            return await ReportsResponseUtils.ExtractBody<TBody, TContainer>(response);
        }
    }
}