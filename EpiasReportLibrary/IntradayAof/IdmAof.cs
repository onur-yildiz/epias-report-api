using System.Text.Json.Serialization;

namespace EpiasReportLibrary.IntraDayAof
{
    public class IdmAof
    {
        [JsonPropertyName("date")]
        public string? Date { get; set; }

        [JsonPropertyName("price")]
        public double? Price { get; set; }
    }
}
