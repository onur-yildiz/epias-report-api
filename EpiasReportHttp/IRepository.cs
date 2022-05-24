using EpiasReportHttp.RequestParams;
using EpiasReportLibrary.Dpp;
using EpiasReportLibrary.DppInjectionUnitName;
using EpiasReportLibrary.IdmVolume;
using EpiasReportLibrary.IntraDayAof;
using EpiasReportLibrary.McpSmps;
using EpiasReportLibrary.Organizations;
using EpiasReportLibrary.RealTimeGeneration;
using EpiasReportLibrary.Smp;

namespace EpiasReportHttp
{
    public interface IRepository
    {
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