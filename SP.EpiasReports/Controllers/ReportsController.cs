using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SP.Authorization;
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
        private readonly ApiPaths _paths;

        public ReportsController(Serilog.ILogger logger, IReportsService repository, IOptions<ApiPaths> option)
        {
            _logger = logger;
            _repository = repository;
            _paths = option.Value;
        }

        [Authorize(AdminRestricted = true)]
        [HttpGet("")]
        public ActionResult<List<object>> GetReports()
        {
            return Ok(_repository.GetReports());
        }

        [Authorize(AdminRestricted = true)]
        [HttpPatch("{reportKey}/is-active")]
        public ActionResult UpdateIsActive(string reportKey, [FromBody] UpdateReportIsActiveRequestParams r)
        {
            _repository.UpdateIsActive(reportKey, r);
            return Ok();
        }

        [Authorize(AdminRestricted = true)]
        [HttpPatch("{reportKey}/roles")]
        public ActionResult UpdateRoles(string reportKey, [FromBody] UpdateReportRolesRequestParams r)
        {
            _repository.UpdateRoles(reportKey, r);
            return Ok();
        }

        //[HttpGet("McpSmp")]
        //public async Task<ActionResult<McpSmpContainer?>> GetMcpSmp([FromQuery] DateIntervalRequestParams r)
        //{
        //    return Ok(await _repository.GetData<McpSmpContainer, McpSmpResponse>(r, _paths.McpSmp));
        //}

        [HttpGet("dam-mcp")]
        public async Task<ActionResult<DayAheadMcpContainer?>> GetDayAheadMcp([FromQuery] DateIntervalRequestParams r)
        {
            return Ok(await _repository.GetData<DayAheadMcpContainer, DayAheadMcpResponse>(r, _paths.DayAheadMcp));
        }

        [HttpGet("rtg")]
        public async Task<ActionResult<HourlyGenerationContainer?>> GetRealTimeGeneration([FromQuery] DateIntervalRequestParams r)
        {
            return Ok(await _repository.GetData<HourlyGenerationContainer, HourlyGenerationResponse>(r, _paths.RealTimeGeneration));
        }

        [HttpGet("fdpp")]
        public async Task<ActionResult<DppContainer?>> GetFdpp([FromQuery] DppRequestParams r)
        {
            return Ok(await _repository.GetData<DppContainer, DppResponse>(r, _paths.Dpp));
        }

        [HttpGet("idm-wap")]
        public async Task<ActionResult<IdmAofContainer?>> GetIntraDayAof([FromQuery] DateIntervalRequestParams r)
        {
            return Ok(await _repository.GetData<IdmAofContainer, IntraDayAofResponse>(r, _paths.IntraDayAof));
        }

        [HttpGet("idm-sum")]
        public async Task<ActionResult<IntraDaySummaryContainer?>> GetIntraDaySummary([FromQuery] DateIntervalRequestParams r)
        {
            return Ok(await _repository.GetData<IntraDaySummaryContainer, IntraDaySummaryResponse>(r, _paths.IntraDaySummary));
        }

        [HttpGet("idm-mq")]
        public async Task<ActionResult<IntraDaySummaryContainer?>> GetIntraDayMatchingQuantity([FromQuery] DateIntervalRequestParams r)
        {
            return Ok(await _repository.GetData<IntraDaySummaryContainer, IntraDaySummaryResponse>(r, _paths.IntraDaySummary));
        }

        [HttpGet("idm-vs")]
        public async Task<ActionResult<IdmVolumeContainer?>> GetIntraDayVolumeSummary([FromQuery] IdmVolumeSummaryRequestParams r)
        {
            return Ok(await _repository.GetData<IdmVolumeContainer, IdmVolumeResponse>(r, _paths.IntraDayVolumeSummary));
        }

        [HttpGet("bpm-smp")]
        public async Task<ActionResult<SmpContainer?>> GetSmp([FromQuery] DateIntervalRequestParams r)
        {
            return Ok(await _repository.GetData<SmpContainer, SmpResponse>(r, _paths.Smp));
        }

        [HttpGet("dpporg")]
        public async Task<ActionResult<OrganizationContainer?>> GetDppOrganization()
        {
            return Ok(await _repository.GetData<OrganizationContainer, OrganizationResponse>(null, _paths.DppOrganization));
        }

        [HttpGet("dppiun")]
        public async Task<ActionResult<DppInjectionUnitNameContainer?>> GetDppInjectionUnitName([FromQuery] DppInjectionUnitNameRequestParams r)
        {
            return Ok(await _repository.GetData<DppInjectionUnitNameContainer, DppInjectionUnitNameResponse>(r, _paths.DppInjectionUnitName));
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