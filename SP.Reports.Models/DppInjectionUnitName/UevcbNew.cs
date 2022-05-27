using System.Text.Json.Serialization;

namespace SP.Reports.Models.DppInjectionUnitName
{
    public class UevcbNew
    {
        [JsonPropertyName("eic")]
        public string? Eic { get; set; }

        [JsonPropertyName("id")]
        public int? Id { get; set; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }
    }
}
