using System.Text.Json.Serialization;

namespace SP.Reports.Models.IntraDaySummary
{
    public class IntraDaySummaryResponse : IResponseBase<IntraDaySummaryContainer>
    {
        [JsonPropertyName("body")]
        public IntraDaySummaryContainer? Body { get; set; }

        [JsonPropertyName("resultCode")]
        public string? ResultCode { get; set; }

        [JsonPropertyName("resultDescription")]
        public string? ResultDescription { get; set; }
    }
}
