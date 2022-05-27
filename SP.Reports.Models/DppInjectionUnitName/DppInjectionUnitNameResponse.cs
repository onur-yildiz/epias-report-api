using System.Text.Json.Serialization;

namespace SP.Reports.Models.DppInjectionUnitName
{
    public class DppInjectionUnitNameResponse : IResponseBase<DppInjectionUnitNameContainer>
    {
        [JsonPropertyName("body")]
        public DppInjectionUnitNameContainer? Body { get; set; }

        [JsonPropertyName("resultCode")]
        public string? ResultCode { get; set; }

        [JsonPropertyName("resultDescription")]
        public string? ResultDescription { get; set; }
    }
}
