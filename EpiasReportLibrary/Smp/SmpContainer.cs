using System.Text.Json.Serialization;

namespace EpiasReportLibrary.Smp
{
    public class SmpContainer
    {
        [JsonPropertyName("smpList")]
        public Smp[]? SmpList { get; set; }

        [JsonPropertyName("statistics")]
        public MarketStatistic[]? Statistics { get; set; }
    }
}
