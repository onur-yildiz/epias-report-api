using System.Text.Json.Serialization;

namespace SP.Reports.Models.McpSmps
{
    public class McpSmpContainer
    {
        [JsonPropertyName("mcpSmps")]
        public McpSmp[]? McpSmps { get; set; }

        [JsonPropertyName("statistics")]
        public McpSmpStatistic[]? Statistics { get; set; }
    }
}
