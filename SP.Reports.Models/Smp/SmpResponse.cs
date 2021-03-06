using System.Text.Json.Serialization;

namespace SP.Reports.Models.Smp
{
    public class SmpResponse : IResponseBase<SmpContainer>
    {
        [JsonPropertyName("resultCode")]
        public string? ResultCode { get; set; }

        [JsonPropertyName("resultDescription")]
        public string? ResultDescription { get; set; }

        [JsonPropertyName("body")]
        public SmpContainer? Body { get; set; }
    }
}
