using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

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
