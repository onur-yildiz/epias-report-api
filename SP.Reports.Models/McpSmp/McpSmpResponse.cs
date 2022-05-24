using System.Text.Json.Serialization;

namespace SP.Reports.Models.McpSmps
{
    public class McpSmpResponse : IResponseBase<McpSmpContainer>
    {
        [JsonPropertyName("body")]
        public McpSmpContainer? Body { get; set; }

        [JsonPropertyName("resultCode")]
        public string? ResultCode { get; set; }

        [JsonPropertyName("resultDescription")]
        public string? ResultDescription { get; set; }
    }
}
