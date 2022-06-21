using System.Text.Json.Serialization;

namespace SP.Reports.Models.RealTimeConsumption
{
    public class HourlyConsumptionContainer
    {
        [JsonPropertyName("hourlyConsumptions")]
        public HourlyConsumptionModelEntity[]? HourlyConsumptions { get; set; }

        [JsonPropertyName("statistics")]
        public HourlyConsumptionStatisticModel[]? Statistics { get; set; }
    }
}
