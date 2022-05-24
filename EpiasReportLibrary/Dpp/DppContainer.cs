using System.Text.Json.Serialization;

namespace EpiasReportLibrary.Dpp
{
    public class DppContainer
    {
        [JsonPropertyName("dppList")]
        public Dpp[]? DppList { get; set; }

        [JsonPropertyName("dppStatistics")]
        public DppStatistic[]? Statistics { get; set; }
    }

}
