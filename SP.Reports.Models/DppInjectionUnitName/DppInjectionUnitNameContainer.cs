using System.Text.Json.Serialization;

namespace SP.Reports.Models.DppInjectionUnitName
{
    public class DppInjectionUnitNameContainer
    {
        [JsonPropertyName("injectionUnitNames")]
        public UevcbNew[]? InjectionUnitNames { get; set; }
    }
}
