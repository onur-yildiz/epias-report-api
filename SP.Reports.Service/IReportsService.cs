using SP.Reports.Models.Dpp;
using SP.Reports.Models.DppInjectionUnitName;
using SP.Reports.Models.IdmVolume;
using SP.Reports.Models.IntraDayAof;
using SP.Reports.Models.McpSmps;
using SP.Reports.Models.Organizations;
using SP.Reports.Models.RealTimeGeneration;
using SP.Reports.Models.RequestParams;
using SP.Reports.Models.Smp;

namespace SP.Reports.Service
{
    public interface IReportsService
    {
        Task<T?> GetContent<T>(string pathAndQueries);
        Task<McpSmpContainer?> GetMcpSmps(DateIntervalRequestParams r);
        Task<HourlyGenerationContainer?> GetRealTimeGeneration(DateIntervalRequestParams r);
        Task<DppContainer?> GetDpp(DppRequestParams r);
        Task<IdmAofContainer?> GetIntraDayAof(DateIntervalRequestParams r);
        Task<IdmVolumeContainer?> GetIntraDayVolumeSummary(IdmVolumeSummaryRequestParams r);
        Task<SmpContainer?> GetSmp(DateIntervalRequestParams r);
        Task<OrganizationContainer?> GetDppOrganization();
        Task<DppInjectionUnitNameContainer?> GetDppInjectionUnitName(DppInjectionUnitNameRequestParams r);
    }
}