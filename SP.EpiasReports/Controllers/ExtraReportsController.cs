using Microsoft.AspNetCore.Mvc;
using SP.EpiasReports.Swagger;
using SP.ExtraReports.Models;
using SP.ExtraReports.Service;
using SP.Reports.Models.RequestParams;
using System.ComponentModel.DataAnnotations;

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

        [SwaggerHeader("x-api-key", description: "API key, can be acquired by a registered user", isRequired: true)]
        [HttpGet("hourly-generations")]
        public Task<IEnumerable<HourlyGenerationsByType>> GetHourlyGenerations([FromQuery][Required] DateIntervalRequestParams r)
        {
            return _repository.GetHourlyGenerations(r);
        }

        [SwaggerHeader("x-api-key", description: "API key, can be acquired by a registered user", isRequired: true)]
        [HttpGet("consumption-statistics")]
        public Task<IConsumptionStatistics> GetConsumptionStatistics([FromQuery] string period)
        {
            return _repository.GetConsumptionStatistics(period);
        }
    }
}