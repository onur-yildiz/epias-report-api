using System.Text.Json.Serialization;

namespace SP.Reports.Models.IntraDayAof
{
    public class IntraDayAofResponse : IResponseBase<IdmAofContainer>
    {
        [JsonPropertyName("resultCode")]
        public string? ResultCode { get; set; }

        [JsonPropertyName("resultDescription")]
        public string? ResultDescription { get; set; }

        [JsonPropertyName("body")]
        public IdmAofContainer? Body { get; set; }
    }
}
