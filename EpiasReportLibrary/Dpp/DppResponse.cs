using System.Text.Json.Serialization;

namespace EpiasReportLibrary.Dpp
{
    public class DppResponse : IResponseBase<DppContainer>
    {
        [JsonPropertyName("body")]
        public DppContainer? Body { get; set; }

        [JsonPropertyName("resultCode")]
        public string? ResultCode { get; set; }

        [JsonPropertyName("requestDescription")]
        public string? ResultDescription { get; set; }
    }
}
