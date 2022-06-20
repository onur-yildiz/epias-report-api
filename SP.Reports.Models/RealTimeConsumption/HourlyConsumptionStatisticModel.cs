using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SP.Reports.Models.RealTimeConsumption
{
    public class HourlyConsumptionStatisticModel
    {
        [JsonPropertyName("consumptionAvg")]
        public double? ConsumptionAvg { get; set; }

        [JsonPropertyName("consumptionMax")]
        public double? ConsumptionMax { get; set; }

        [JsonPropertyName("consumptionMin")]
        public double? ConsumptionMin { get; set; }

        [JsonPropertyName("consumptionSum")]
        public double? ConsumptionSum { get; set; }

        [JsonPropertyName("date")]
        public string? Date { get; set; }
    }
}
