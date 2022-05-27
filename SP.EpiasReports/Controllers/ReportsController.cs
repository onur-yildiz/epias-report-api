using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SP.Reports.Models.DppInjectionUnitName;
using SP.Reports.Models.IdmVolume;
using SP.Reports.Models.IntraDayAof;
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

        public ReportsController(Serilog.ILogger logger, IReportsService repository)
        {
            _logger = logger;
            _repository = repository;
        }

        [HttpGet("McpSmp")]
        public async Task<ActionResult<McpSmpContainer?>> GetMcpSmp([FromBody] DateIntervalRequestParams r)
        {
            return Ok(await _repository.GetMcpSmps(r));
        }

        [HttpGet("RealTimeGeneration")]
        public async Task<ActionResult<HourlyGenerationContainer?>> GetRealTimeGeneration([FromBody] DateIntervalRequestParams r)
        {
            return Ok(await _repository.GetRealTimeGeneration(r));
        }

        [HttpGet("Dpp")]
        public async Task<ActionResult<HourlyGenerationContainer?>> GetDpp([FromBody] DppRequestParams r)
        {
            return Ok(await _repository.GetDpp(r));
        }

        [HttpGet("IntraDayAof")]
        public async Task<ActionResult<IdmAofContainer?>> GetIntraDayAof([FromBody] DateIntervalRequestParams r)
        {
            return Ok(await _repository.GetIntraDayAof(r));
        }

        [HttpGet("IntraDayVolumeSummary")]
        public async Task<ActionResult<IdmVolumeContainer?>> GetIntraDayVolumeSummary([FromBody] IdmVolumeSummaryRequestParams r)
        {
            return Ok(await _repository.GetIntraDayVolumeSummary(r));
        }

        [HttpGet("Smp")]
        public async Task<ActionResult<SmpContainer?>> GetSmp([FromBody] DateIntervalRequestParams r)
        {
            return Ok(await _repository.GetSmp(r));
        }

        [HttpGet("DppOrganization")]
        public async Task<ActionResult<OrganizationContainer?>> GetDppOrganization()
        {
            return Ok(await _repository.GetDppOrganization());
        }

        [HttpGet("DppInjectionUnitName")]
        public async Task<ActionResult<DppInjectionUnitNameContainer?>> GetDppInjectionUnitName([FromBody] DppInjectionUnitNameRequestParams r)
        {
            return Ok(await _repository.GetDppInjectionUnitName(r));
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