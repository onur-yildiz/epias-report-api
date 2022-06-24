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
using SP.Reports.Models.Organizations;
using SP.Reports.Models.RealTimeGeneration;
using SP.Reports.Models.ReportListing;
using SP.Reports.Models.RequestBody;
using SP.Reports.Models.RequestParams;
using SP.Reports.Models.Smp;
using SP.Reports.Service;

namespace SP.EpiasReport.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("[controller]")]
    public class ReportsController : ControllerBase
    {
        private readonly Serilog.ILogger _logger;
        private readonly IReportsService _repository;
        private readonly IApiPaths _paths;

        public ReportsController(Serilog.ILogger logger, IReportsService repository, IOptions<ApiPaths> option)
        {
            _logger = logger;
            _repository = repository;
            _paths = option.Value;
        }

        /// <summary>
        /// Serves all reports' info.
        /// </summary>
        /// <returns></returns>
        [SwaggerHeader("Authorization", isRequired: true)]
        [Authorize(AdminRestricted = true)]
        [HttpGet("")]
        public IEnumerable<IReport>? GetReports()
        {
            return _repository.GetReports();
        }

        /// <summary>
        /// Update a report's active state.
        /// </summary>
        /// <param name="reportKey">Key assigned for the report (ex: dam-mcp, bpm-smp, idm-wap)</param>
        /// <param name="r"></param>
        /// <returns></returns>
        [SwaggerHeader("Authorization", isRequired: true)]
        [Authorize(AdminRestricted = true)]
        [HttpPatch("{reportKey}/is-active")]
        public IActionResult UpdateIsActive(string reportKey, [FromBody] UpdateReportIsActiveRequestBody r)
        {
            _repository.UpdateIsActive(reportKey, r);
            return Ok();
        }

        /// <summary>
        /// Update authorized roles for the report.
        /// </summary>
        /// <param name="reportKey">Key assigned for the report (ex: dam-mcp, bpm-smp, idm-wap)</param>
        /// <param name="r"></param>
        /// <returns></returns>
        [SwaggerHeader("Authorization", isRequired: true)]
        [Authorize(AdminRestricted = true)]
        [HttpPatch("{reportKey}/roles")]
        public IActionResult UpdateRoles(string reportKey, [FromBody] UpdateReportRolesRequestBody r)
        {
            _repository.UpdateRoles(reportKey, r);
            return Ok();
        }

        //[HttpGet("McpSmp")]
        //public  Task<McpSmpContainer?> GetMcpSmp([FromQuery] IDateIntervalRequestParams r)
        //{
        //    return _repository.GetData<McpSmpContainer, McpSmpResponse>(r, _paths.McpSmp);
        //}

        /// <summary>
        /// Day Ahead Markets - Market Clearing Price Report
        /// </summary>
        /// <param name="r"></param>
        /// <returns></returns>
        [SwaggerHeader("Authorization")]
        [HttpGet("dam-mcp")]
        public Task<DayAheadMcpContainer?> GetDayAheadMcp([FromQuery] DateIntervalRequestParams r)
        {
            return _repository.GetData<DayAheadMcpContainer, DayAheadMcpResponse>(r, _paths.DayAheadMcp);
        }

        /// <summary>
        /// Real-time Generation Report
        /// </summary>
        /// <param name="r"></param>
        /// <returns></returns>
        [SwaggerHeader("Authorization")]
        [HttpGet("rtg")]
        public Task<HourlyGenerationContainer?> GetRealTimeGeneration([FromQuery] DateIntervalRequestParams r)
        {
            return _repository.GetData<HourlyGenerationContainer, HourlyGenerationResponse>(r, _paths.RealTimeGeneration);
        }

        /// <summary>
        /// Final Daily Production Program Report
        /// </summary>
        /// <param name="r"></param>
        /// <returns></returns>
        [SwaggerHeader("Authorization")]
        [HttpGet("dpp")]
        public async Task<DppContainer?> GetDpp([FromQuery] DppRequestParams r)
        {
            return await _repository.GetData<DppContainer, DppResponse>(r, _paths.Dpp);
        }

        /// <summary>
        /// Intra-day Markets - Weighted Average Price Report
        /// </summary>
        /// <param name="r"></param>
        /// <returns></returns>
        [SwaggerHeader("Authorization")]
        [HttpGet("idm-wap")]
        public Task<IdmAofContainer?> GetIntraDayAof([FromQuery] DateIntervalRequestParams r)
        {
            return _repository.GetData<IdmAofContainer, IntraDayAofResponse>(r, _paths.IntraDayAof);
        }

        /// <summary>
        /// Intra-day Markets - Summary Report
        /// </summary>
        /// <param name="r"></param>
        /// <returns></returns>
        [SwaggerHeader("Authorization")]
        [HttpGet("idm-sum")]
        public Task<IntraDaySummaryContainer?> GetIntraDaySummary([FromQuery] DateIntervalRequestParams r)
        {
            return _repository.GetData<IntraDaySummaryContainer, IntraDaySummaryResponse>(r, _paths.IntraDaySummary);
        }

        // TODO RETURN ONLY THE MATCHING QUANTITY DATA
        /// <summary>
        /// Intra-day Markets - Matching Quantity Report
        /// </summary>
        /// <param name="r"></param>
        /// <returns></returns>
        [SwaggerHeader("Authorization")]
        [HttpGet("idm-mq")]
        public Task<IntraDaySummaryContainer?> GetIntraDayMatchingQuantity([FromQuery] DateIntervalRequestParams r)
        {
            return _repository.GetData<IntraDaySummaryContainer, IntraDaySummaryResponse>(r, _paths.IntraDaySummary);
        }

        /// <summary>
        /// Intra-day Markets - Volume Summary Report
        /// </summary>
        /// <param name="r"></param>
        /// <returns></returns>
        [SwaggerHeader("Authorization")]
        [HttpGet("idm-vs")]
        public Task<IdmVolumeContainer?> GetIntraDayVolumeSummary([FromQuery] IdmVolumeSummaryRequestParams r)
        {
            return _repository.GetData<IdmVolumeContainer, IdmVolumeResponse>(r, _paths.IntraDayVolumeSummary);
        }

        /// <summary>
        /// Balancing Power Market - System Marginal Price Report
        /// </summary>
        /// <param name="r"></param>
        /// <returns></returns>
        [SwaggerHeader("Authorization")]
        [HttpGet("bpm-smp")]
        public Task<SmpContainer?> GetSmp([FromQuery] DateIntervalRequestParams r)
        {
            return _repository.GetData<SmpContainer, SmpResponse>(r, _paths.Smp);
        }

        /// <summary>
        /// Serves DPP Organizations.
        /// </summary>
        /// <returns></returns>
        [SwaggerHeader("Authorization")]
        [HttpGet("dpporg")]
        public Task<OrganizationContainer?> GetDppOrganization()
        {
            return _repository.GetData<OrganizationContainer, OrganizationResponse>(null, _paths.DppOrganization);
        }

        /// <summary>
        /// Serves DPP Injection Unit Names.
        /// </summary>
        /// <param name="r"></param>
        /// <returns></returns>
        [SwaggerHeader("Authorization")]
        [HttpGet("dppiun")]
        public Task<DppInjectionUnitNameContainer?> GetDppInjectionUnitName([FromQuery] DppInjectionUnitNameRequestParams r)
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