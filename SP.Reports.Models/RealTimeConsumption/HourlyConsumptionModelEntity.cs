using System.Text.Json.Serialization;

namespace SP.Reports.Models.RealTimeConsumption
{
    public class HourlyConsumptionModelEntity
    {
        [JsonPropertyName("consumption")]
        public double? Consumption { get; set; }

        [JsonPropertyName("date")]
        public string? Date { get; set; }
    }
}
