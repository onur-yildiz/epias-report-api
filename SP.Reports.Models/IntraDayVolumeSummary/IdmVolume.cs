using System.Text.Json.Serialization;

namespace SP.Reports.Models.IdmVolume
{
    public class IdmVolume
    {
        [JsonPropertyName("date")]
        public string? Date { get; set; }

        [JsonPropertyName("period")]
        public int? Period { get; set; }

        [JsonPropertyName("volume")]
        public double? Volume { get; set; }

        [JsonPropertyName("periodType")]
        public string? PeriodType { get; set; }
    }
}
