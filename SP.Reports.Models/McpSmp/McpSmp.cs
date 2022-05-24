using System.Text.Json.Serialization;

namespace SP.Reports.Models.McpSmps
{
    public class McpSmp
    {
        [JsonPropertyName("date")]
        public string? Date { get; set; }

        [JsonPropertyName("mcp")]
        public double? Mcp { get; set; }

        [JsonPropertyName("smp")]
        public double? Smp { get; set; }

        [JsonPropertyName("mcpState")]
        public string? McpState { get; set; }

        [JsonPropertyName("smpDirection")]
        public string? SmpDirection { get; set; }
    }
}
