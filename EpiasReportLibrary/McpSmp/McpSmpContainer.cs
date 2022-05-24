using System.Text.Json.Serialization;

namespace EpiasReportLibrary.McpSmps
{
    public class McpSmpContainer
    {
        [JsonPropertyName("mcpSmps")]
        public McpSmp[]? McpSmps { get; set; }

        [JsonPropertyName("statistics")]
        public McpSmpStatistic[]? Statistics { get; set; }
    }
}
