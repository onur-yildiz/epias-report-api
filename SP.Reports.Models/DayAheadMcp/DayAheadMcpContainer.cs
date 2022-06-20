using System.Text.Json.Serialization;

namespace SP.Reports.Models.DayAheadMcp
{
    public class DayAheadMcpContainer
    {
        [JsonPropertyName("dayAheadMCPList")]
        public DayAheadMcpWithExchangeData[]? DayAheadMcpList { get; set; }

        [JsonPropertyName("statistics")]
        public MarketStatistic[]? Statistics { get; set; }
    }
}
