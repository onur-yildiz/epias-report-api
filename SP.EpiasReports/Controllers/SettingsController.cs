using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
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
using SP.Settings.Models;
using SP.User.Service;

namespace SP.EpiasReport.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("[controller]")]
    public class SettingsController : ControllerBase
    {
        private readonly Serilog.ILogger _logger;
        private readonly ISettingsService _repository;

        public SettingsController(Serilog.ILogger logger, ISettingsService repository, IOptions<ApiPaths> option)
        {
            _logger = logger;
            _repository = repository;
        }

        [HttpGet("ReportListingInfo")]
        public ActionResult<List<Report>> GetReportListingInfo()
        {
            return Ok(_repository.GetReportListingInfo());
        }
    }
}