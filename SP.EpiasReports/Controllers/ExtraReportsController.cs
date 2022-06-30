using Microsoft.AspNetCore.Mvc;
using SP.EpiasReports.Models;
using SP.EpiasReports.Swagger;
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
        private readonly IExtraReportsService _service;

        public ExtraReportsController(Serilog.ILogger logger, IExtraReportsService service)
        {
            _logger = logger;
            _service = service;
        }

        /// <summary>
        /// Hourly electric generation values categorized by renewable and non-renewable energy types.
        /// </summary>
        /// <param name="r"></param>
        /// <returns></returns>
        [SwaggerHeader("x-api-key", description: "API key, can be acquired by a registered user", isRequired: true)]
        [HttpGet("hourly-generations")]
        public async Task<ApiResponse<IEnumerable<HourlyGenerationsByType>>> GetHourlyGenerations([FromQuery] DateIntervalRequestParams r)
        {
            return ApiResponse<IEnumerable<HourlyGenerationsByType>>.Success(await _service.GetHourlyGenerations(r));
        }

        /// <summary>
        /// Consumption statistics of a month for the periods which consumed the most electricity Includes hour with the highest consume for each day and top 5 electricity consumed days 
        /// </summary>
        /// <param name="period">Date of the period. Month is picked from the date. Any date in the desired month is accepted. (yyyy-MM-dd)</param>
        /// <returns></returns>
        [SwaggerHeader("x-api-key", description: "API key, can be acquired by a registered user", isRequired: true)]
        [HttpGet("consumption-statistics")]
        public async Task<ApiResponse<ConsumptionStatistics>> GetConsumptionStatistics([FromQuery] string period)
        {
            return ApiResponse<ConsumptionStatistics>.Success(await _service.GetConsumptionStatistics(period));
        }
    }
}