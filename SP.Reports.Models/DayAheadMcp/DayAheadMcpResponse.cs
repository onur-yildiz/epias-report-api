using System.Text.Json.Serialization;

namespace SP.Reports.Models.DayAheadMcp
{
    public class DayAheadMcpResponse : IResponseBase<DayAheadMcpContainer>
    {
        [JsonPropertyName("body")]
        public DayAheadMcpContainer? Body { get; set; }

        [JsonPropertyName("resultCode")]
        public string? ResultCode { get; set; }

        [JsonPropertyName("resultDescription")]
        public string? ResultDescription { get; set; }
    }
}
