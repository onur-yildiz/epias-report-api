using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SP.Authorization;
using SP.EpiasReports.Swagger;
using SP.Reports.Models.Api;
using SP.Reports.Models.DayAheadMcp;
using SP.Reports.Models.Dpp;
using SP.Reports.Models.DppInjectionUnitName;
using SP.Reports.Models.IdmVolume;
using SP.Reports.Models.IntraDayAof;
using SP.Reports.Models.IntraDaySummary;
using SP.Reports.Models.McpSmps;
using SP.Reports.Models.Organizations;
using SP.Reports.Models.RealTimeGeneration;
using SP.Reports.Models.ReportListing;
using SP.Reports.Models.RequestParams;
using SP.Reports.Models.Smp;
using SP.Reports.Service;
using System.ComponentModel.DataAnnotations;

namespace SP.EpiasReport.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("[controller]")]
    public class ReportsController : ControllerBase
    {
        private readonly Serilog.ILogger _logger;
        private readonly IReportsService _repository;
        private readonly ApiPaths _paths;

        public ReportsController(Serilog.ILogger logger, IReportsService repository, IOptions<ApiPaths> option)
        {
            _logger = logger;
            _repository = repository;
            _paths = option.Value;
        }

        [SwaggerHeader("Authorization", isRequired: true)]
        [Authorize(AdminRestricted = true)]
        [HttpGet("")]
        public IEnumerable<Report>? GetReports()
        {
            return _repository.GetReports();
        }

        [SwaggerHeader("Authorization", isRequired: true)]
        [Authorize(AdminRestricted = true)]
        [HttpPatch("{reportKey}/is-active")]
        public ActionResult UpdateIsActive(string reportKey, [FromBody][Required] UpdateReportIsActiveRequestParams r)
        {
            _repository.UpdateIsActive(reportKey, r);
            return Ok();
        }

        [SwaggerHeader("Authorization", isRequired: true)]
        [Authorize(AdminRestricted = true)]
        [HttpPatch("{reportKey}/roles")]
        public ActionResult UpdateRoles(string reportKey, [FromBody][Required] UpdateReportRolesRequestParams r)
        {
            _repository.UpdateRoles(reportKey, r);
            return Ok();
        }

        //[HttpGet("McpSmp")]
        //public  Task<McpSmpContainer?> GetMcpSmp([FromQuery] DateIntervalRequestParams r)
        //{
        //    return _repository.GetData<McpSmpContainer, McpSmpResponse>(r, _paths.McpSmp);
        //}

        [SwaggerHeader("Authorization")]
        [HttpGet("dam-mcp")]
        public Task<DayAheadMcpContainer?> GetDayAheadMcp([FromQuery][Required] DateIntervalRequestParams r)
        {
            return _repository.GetData<DayAheadMcpContainer, DayAheadMcpResponse>(r, _paths.DayAheadMcp);
        }

        [SwaggerHeader("Authorization")]
        [HttpGet("rtg")]
        public Task<HourlyGenerationContainer?> GetRealTimeGeneration([FromQuery][Required] DateIntervalRequestParams r)
        {
            return _repository.GetData<HourlyGenerationContainer, HourlyGenerationResponse>(r, _paths.RealTimeGeneration);
        }

        [SwaggerHeader("Authorization")]
        [HttpGet("fdpp")]
        public async Task<DppContainer?> GetFdpp([FromQuery][Required] DppRequestParams r)
        {
            return await _repository.GetData<DppContainer, DppResponse>(r, _paths.Dpp);
        }

        [SwaggerHeader("Authorization")]
        [HttpGet("idm-wap")]
        public Task<IdmAofContainer?> GetIntraDayAof([FromQuery][Required] DateIntervalRequestParams r)
        {
            return _repository.GetData<IdmAofContainer, IntraDayAofResponse>(r, _paths.IntraDayAof);
        }

        [SwaggerHeader("Authorization")]
        [HttpGet("idm-sum")]
        public Task<IntraDaySummaryContainer?> GetIntraDaySummary([FromQuery][Required] DateIntervalRequestParams r)
        {
            return _repository.GetData<IntraDaySummaryContainer, IntraDaySummaryResponse>(r, _paths.IntraDaySummary);
        }

        [SwaggerHeader("Authorization")]
        [HttpGet("idm-mq")]
        public Task<IntraDaySummaryContainer?> GetIntraDayMatchingQuantity([FromQuery][Required] DateIntervalRequestParams r)
        {
            return _repository.GetData<IntraDaySummaryContainer, IntraDaySummaryResponse>(r, _paths.IntraDaySummary);
        }

        [SwaggerHeader("Authorization")]
        [HttpGet("idm-vs")]
        public Task<IdmVolumeContainer?> GetIntraDayVolumeSummary([FromQuery][Required] IdmVolumeSummaryRequestParams r)
        {
            return _repository.GetData<IdmVolumeContainer, IdmVolumeResponse>(r, _paths.IntraDayVolumeSummary);
        }

        [SwaggerHeader("Authorization")]
        [HttpGet("bpm-smp")]
        public Task<SmpContainer?> GetSmp([FromQuery][Required] DateIntervalRequestParams r)
        {
            return _repository.GetData<SmpContainer, SmpResponse>(r, _paths.Smp);
        }

        [SwaggerHeader("Authorization")]
        [HttpGet("dpporg")]
        public Task<OrganizationContainer?> GetDppOrganization()
        {
            return _repository.GetData<OrganizationContainer, OrganizationResponse>(null, _paths.DppOrganization);
        }

        [SwaggerHeader("Authorization")]
        [HttpGet("dppiun")]
        public Task<DppInjectionUnitNameContainer?> GetDppInjectionUnitName([FromQuery][Required] DppInjectionUnitNameRequestParams r)
        {
            return _repository.GetData<DppInjectionUnitNameContainer, DppInjectionUnitNameResponse>(r, _paths.DppInjectionUnitName);
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("/error-development")]
        public IActionResult HandleErrorDevelopment([FromServices] IHostEnvironment hostEnvironment)
        {
            if (!hostEnvironment.IsDevelopment())
            {
                return NotFound();
            }

            var exceptionHandlerFeature = HttpContext.Features.Get<IExceptionHandlerFeature>()!;

            _logger.Error("{@error}", exceptionHandlerFeature.Error);
            return Problem(
                detail: exceptionHandlerFeature.Error.StackTrace,
                title: exceptionHandlerFeature.Error.Message
            );
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("/error")]
        public IActionResult HandleError()
        {
            var exceptionHandlerFeature = HttpContext.Features.Get<IExceptionHandlerFeature>()!;
            _logger.Error("{@error}", exceptionHandlerFeature.Error);
            return Problem();
        }
    }
}