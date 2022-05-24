using System.Text.Json.Serialization;

namespace SP.Reports.Models.Dpp
{
    public class DppContainer
    {
        [JsonPropertyName("dppList")]
        public Dpp[]? DppList { get; set; }

        [JsonPropertyName("dppStatistics")]
        public DppStatistic[]? Statistics { get; set; }
    }

}
