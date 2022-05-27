using Microsoft.Extensions.Options;
using SP.Reports.Models.Api;
using SP.Reports.Models.Dpp;
using SP.Reports.Models.DppInjectionUnitName;
using SP.Reports.Models.IdmVolume;
using SP.Reports.Models.IntraDayAof;
using SP.Reports.Models.McpSmps;
using SP.Reports.Models.Organizations;
using SP.Reports.Models.RealTimeGeneration;
using SP.Reports.Models.RequestParams;
using SP.Reports.Models.Smp;
using SP.Reports.Service;

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

        public async Task<T?> GetContent<T>(string pathAndQueries)
        {
            var httpClient = _httpClientFactory.CreateClient("EpiasAPI");
            var response = await httpClient.GetAsync(pathAndQueries);
            response.EnsureSuccessStatusCode();
            return await ReportsResponseUtils.ExtractContent<T>(response);
        }

        public async Task<McpSmpContainer?> GetMcpSmps(DateIntervalRequestParams r)
        {
            var endpoint = _paths.McpSmp;
            var content = await GetContent<McpSmpResponse>($"{endpoint}?startDate={r.StartDate}&endDate={r.EndDate}");
            return content?.Body;
        }

        public async Task<HourlyGenerationContainer?> GetRealTimeGeneration(DateIntervalRequestParams r)
        {
            var endpoint = _paths.RealTimeGeneration;
            var content = await GetContent<HourlyGenerationResponse>($"{endpoint}?startDate={r.StartDate}&endDate={r.EndDate}");
            return content?.Body;
        }

        public async Task<DppContainer?> GetDpp(DppRequestParams r)
        {
            var endpoint = _paths.Dpp;
            var content = await GetContent<DppResponse>($"{endpoint}?startDate={r.StartDate}&endDate={r.EndDate}&organizationEIC={r.OrganizationEIC}&uevcbEIC={r.UevcbEIC}");
            return content?.Body;
        }

        public async Task<IdmAofContainer?> GetIntraDayAof(DateIntervalRequestParams r)
        {
            var endpoint = _paths.IntraDayAof;
            var content = await GetContent<IntraDayAofResponse>($"{endpoint}?startDate={r.StartDate}&endDate={r.EndDate}");
            return content?.Body;
        }

        public async Task<IdmVolumeContainer?> GetIntraDayVolumeSummary(IdmVolumeSummaryRequestParams r)
        {
            var endpoint = _paths.IntraDayVolumeSummary;
            var content = await GetContent<IdmVolumeResponse>($"{endpoint}?startDate={r.StartDate}&endDate={r.EndDate}&period={r.Period}");
            return content?.Body;
        }

        public async Task<SmpContainer?> GetSmp(DateIntervalRequestParams r)
        {
            var endpoint = _paths.Smp;
            var content = await GetContent<SmpResponse>($"{endpoint}?startDate={r.StartDate}&endDate={r.EndDate}");
            return content?.Body;
        }

        public async Task<OrganizationContainer?> GetDppOrganization()
        {
            var endpoint = _paths.DppOrganization;
            var content = await GetContent<OrganizationResponse>(endpoint!);
            return content?.Body;
        }

        public async Task<DppInjectionUnitNameContainer?> GetDppInjectionUnitName(DppInjectionUnitNameRequestParams r)
        {
            var endpoint = _paths.DppInjectionUnitName;
            var content = await GetContent<DppInjectionUnitNameResponse>($"{endpoint}?organizationEIC={r.OrganizationEIC}");
            return content?.Body;
        }
    }
}