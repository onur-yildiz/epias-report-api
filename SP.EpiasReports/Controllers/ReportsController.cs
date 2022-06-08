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

        [HttpGet("McpSmp")]
        public async Task<ActionResult<McpSmpContainer?>> GetMcpSmp([FromQuery] DateIntervalRequestParams r)
        {
            return Ok(await _repository.GetData<McpSmpContainer, McpSmpResponse>(r, _paths.McpSmp));
        }

        [HttpGet("DayAheadMcp")]
        public async Task<ActionResult<DayAheadMcpContainer?>> GetDayAheadMcp([FromQuery] DateIntervalRequestParams r)
        {
            return Ok(await _repository.GetData<DayAheadMcpContainer, DayAheadMcpResponse>(r, _paths.DayAheadMcp));
        }

        [HttpGet("RealTimeGeneration")]
        public async Task<ActionResult<HourlyGenerationContainer?>> GetRealTimeGeneration([FromQuery] DateIntervalRequestParams r)
        {
            return Ok(await _repository.GetData<HourlyGenerationContainer, HourlyGenerationResponse>(r, _paths.RealTimeGeneration));
        }

        [HttpGet("Dpp")]
        public async Task<ActionResult<DppContainer?>> GetDpp([FromQuery] DppRequestParams r)
        {
            return Ok(await _repository.GetData<DppContainer, DppResponse>(r, _paths.Dpp));
        }

        [HttpGet("IntraDayAof")]
        public async Task<ActionResult<IdmAofContainer?>> GetIntraDayAof([FromQuery] DateIntervalRequestParams r)
        {
            return Ok(await _repository.GetData<IdmAofContainer, IntraDayAofResponse>(r, _paths.IntraDayAof));
        }

        [HttpGet("IntraDaySummary")]
        public async Task<ActionResult<IntraDaySummaryContainer?>> GetIntraDaySummary([FromQuery] DateIntervalRequestParams r)
        {
            return Ok(await _repository.GetData<IntraDaySummaryContainer, IntraDaySummaryResponse>(r, _paths.IntraDaySummary));
        }

        [HttpGet("IntraDayVolumeSummary")]
        public async Task<ActionResult<IdmVolumeContainer?>> GetIntraDayVolumeSummary([FromQuery] IdmVolumeSummaryRequestParams r)
        {
            return Ok(await _repository.GetData<IdmVolumeContainer, IdmVolumeResponse>(r, _paths.IntraDayVolumeSummary));
        }

        [HttpGet("Smp")]
        public async Task<ActionResult<SmpContainer?>> GetSmp([FromQuery] DateIntervalRequestParams r)
        {
            return Ok(await _repository.GetData<SmpContainer, SmpResponse>(r, _paths.Smp));
        }

        [HttpGet("DppOrganization")]
        public async Task<ActionResult<OrganizationContainer?>> GetDppOrganization()
        {
            return Ok(await _repository.GetData<OrganizationContainer, OrganizationResponse>(null, _paths.DppOrganization));
        }

        [HttpGet("DppInjectionUnitName")]
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