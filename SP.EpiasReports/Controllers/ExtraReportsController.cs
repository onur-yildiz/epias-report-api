using Microsoft.AspNetCore.Mvc;
using SP.ExtraReports.Models;
using SP.ExtraReports.Service;
using SP.Reports.Models.RequestParams;

namespace SP.EpiasReport.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ExtraReportsController : ControllerBase
    {
        private readonly Serilog.ILogger _logger;
        private readonly IExtraReportsService _repository;

        public ExtraReportsController(Serilog.ILogger logger, IExtraReportsService repository)
        {
            _logger = logger;
            _repository = repository;
        }

        [HttpGet("hourly-generations")]
        public Task<IEnumerable<HourlyGenerationsByType>> GetHourlyGenerations([FromQuery] DateIntervalRequestParams r)
        {
            return _repository.GetHourlyGenerations(r);
        }

        [HttpGet("consumption-statistics")]
        public Task<IConsumptionStatistics> GetConsumptionStatistics([FromQuery] string period)
        {
            return _repository.GetConsumptionStatistics(period);
        }
    }
}