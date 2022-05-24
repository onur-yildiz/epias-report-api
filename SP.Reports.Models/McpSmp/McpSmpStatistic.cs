using System.Text.Json.Serialization;

namespace SP.Reports.Models.McpSmps
{
    public class McpSmpStatistic
    {
        [JsonPropertyName("date")]
        public string? Date { get; set; }

        [JsonPropertyName("mcpAvg")]
        public double? McpAvg { get; set; }

        [JsonPropertyName("mcpMax")]
        public double? McpMax { get; set; }

        [JsonPropertyName("mcpMin")]
        public double? McpMin { get; set; }

        [JsonPropertyName("mcpWeightedAverage")]
        public double? McpWeightedAverage { get; set; }

        [JsonPropertyName("smpAvg")]
        public double? SmpAvg { get; set; }

        [JsonPropertyName("smpMax")]
        public double? SmpMax { get; set; }

        [JsonPropertyName("smpMin")]
        public double? SmpMin { get; set; }

        [JsonPropertyName("smpWeightedAverage")]
        public double? SmpWeightedAverage { get; set; }
    }
}
