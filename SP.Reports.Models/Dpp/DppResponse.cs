using System.Text.Json.Serialization;

namespace SP.Reports.Models.Dpp
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
