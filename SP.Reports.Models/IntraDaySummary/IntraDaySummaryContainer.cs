using System.Text.Json.Serialization;

namespace SP.Reports.Models.IntraDaySummary
{
    public class IntraDaySummaryContainer
    {
        [JsonPropertyName("intraDaySummaryList")]
        public IntraDaySummary[]? IntraDaySummaryList { get; set; }

        [JsonPropertyName("statistics")]
        public IntraDaySummaryStatistic[]? Statistics { get; set; }
    }
}
