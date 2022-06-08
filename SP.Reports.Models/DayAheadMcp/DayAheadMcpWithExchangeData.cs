using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SP.Reports.Models.DayAheadMcp
{
    public class DayAheadMcpWithExchangeData
    {
        [JsonPropertyName("date")]
        public string? Date { get; set; }

        [JsonPropertyName("price")]
        public double? Price { get; set; }

        [JsonPropertyName("priceUsd")]
        public double? PriceUsd { get; set; }

        [JsonPropertyName("priceEur")]
        public double? PriceEur { get; set; }

    }
}
