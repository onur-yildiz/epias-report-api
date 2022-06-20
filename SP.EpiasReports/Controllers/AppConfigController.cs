using Microsoft.AspNetCore.Mvc;
using SP.AppConfig.Service;

namespace SP.EpiasReport.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AppConfigController : ControllerBase
    {
        private readonly Serilog.ILogger _logger;
        private readonly IAppConfigService _repository;

        public AppConfigController(Serilog.ILogger logger, IAppConfigService repository)
        {
            _logger = logger;
            _repository = repository;
        }

        [HttpGet("report-listing-info")]
        public IEnumerable<dynamic>? GetReportListingInfo([FromHeader] string? authorization)
        {
            return _repository.GetReportListing(authorization);
        }
    }
}