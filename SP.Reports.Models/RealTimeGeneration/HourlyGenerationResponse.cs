using System.Text.Json.Serialization;

namespace SP.Reports.Models.RealTimeGeneration
{
    public class HourlyGenerationResponse : IResponseBase<HourlyGenerationContainer>
    {
        [JsonPropertyName("body")]
        public HourlyGenerationContainer? Body { get; set; }

        [JsonPropertyName("resultCode")]
        public string? ResultCode { get; set; }

        [JsonPropertyName("resultDescription")]
        public string? ResultDescription { get; set; }
    }
}
