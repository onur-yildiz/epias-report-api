using System.Text.Json.Serialization;

namespace SP.Reports.Models.Smp
{
    public class Smp
    {
        [JsonPropertyName("date")]
        public string? Date { get; set; }

        [JsonPropertyName("price")]
        public double? Price { get; set; }

        [JsonPropertyName("smpDirection")]
        public string? SmpDirection { get; set; }

        [JsonPropertyName("smpDirectionId")]
        public int? SmpDirectionId { get; set; }

        [JsonPropertyName("nextHour")]
        public string? NextHour { get; set; }
    }
}
