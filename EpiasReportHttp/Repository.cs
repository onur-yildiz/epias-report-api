using EpiasReportHttp.RequestParams;
using EpiasReportLibrary.Dpp;
using EpiasReportLibrary.DppInjectionUnitName;
using EpiasReportLibrary.IdmVolume;
using EpiasReportLibrary.IntraDayAof;
using EpiasReportLibrary.McpSmps;
using EpiasReportLibrary.Organizations;
using EpiasReportLibrary.RealTimeGeneration;
using EpiasReportLibrary.Smp;
using Microsoft.Extensions.Options;

namespace EpiasReportHttp
{
    public class Repository : IRepository
    {
        readonly ApiSettings _api;

        public Repository(IOptions<ApiSettings> option)
        {
            _api = option.Value;
        }

        public async Task<McpSmpContainer?> GetMcpSmps(DateIntervalRequestParams r)
        {
            var url = _api.BaseUrl + _api.Paths.McpSmp;
            var response = await HttpHelper.Fetch<McpSmpResponse>($"{url}?startDate={r.StartDate}&endDate={r.EndDate}");
            return response?.Body;
        }

        public async Task<HourlyGenerationContainer?> GetRealTimeGeneration(DateIntervalRequestParams r)
        {
            var url = _api.BaseUrl + _api.Paths.RealTimeGeneration;
            var response = await HttpHelper.Fetch<HourlyGenerationResponse>($"{url}?startDate={r.StartDate}&endDate={r.EndDate}");
            return response?.Body;
        }

        public async Task<DppContainer?> GetDpp(DppRequestParams r)
        {
            var url = _api.BaseUrl + _api.Paths.Dpp;
            var response = await HttpHelper.Fetch<DppResponse>($"{url}?startDate={r.StartDate}&endDate={r.EndDate}&organizationEIC={r.OrganizationEIC}&uevcbEIC={r.UevcbEIC}");
            return response?.Body;
        }

        public async Task<IdmAofContainer?> GetIntraDayAof(DateIntervalRequestParams r)
        {
            var url = _api.BaseUrl + _api.Paths.IntraDayAof;
            var response = await HttpHelper.Fetch<IntraDayAofResponse>($"{url}?startDate={r.StartDate}&endDate={r.EndDate}");
            return response?.Body;
        }

        public async Task<IdmVolumeContainer?> GetIntraDayVolumeSummary(IdmVolumeSummaryRequestParams r)
        {
            var url = _api.BaseUrl + _api.Paths.IntraDayVolumeSummary;
            var response = await HttpHelper.Fetch<IdmVolumeResponse>($"{url}?startDate={r.StartDate}&endDate={r.EndDate}&period={r.Period}");
            return response?.Body;
        }

        public async Task<SmpContainer?> GetSmp(DateIntervalRequestParams r)
        {
            var url = _api.BaseUrl + _api.Paths.Smp;
            var response = await HttpHelper.Fetch<SmpResponse>($"{url}?startDate={r.StartDate}&endDate={r.EndDate}");
            return response?.Body;
        }

        public async Task<OrganizationContainer?> GetDppOrganization()
        {
            var url = _api.BaseUrl + _api.Paths.DppOrganization;
            var response = await HttpHelper.Fetch<OrganizationResponse>(url);
            return response?.Body;
        }

        public async Task<DppInjectionUnitNameContainer?> GetDppInjectionUnitName(DppInjectionUnitNameRequestParams r)
        {
            var url = _api.BaseUrl + _api.Paths.DppInjectionUnitName;
            var response = await HttpHelper.Fetch<DppInjectionUnitNameResponse>($"{url}?organizationEIC={r.OrganizationEIC}");
            return response?.Body;
        }
    }
}