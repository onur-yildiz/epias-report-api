using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SP.Reports.Models.RealTimeConsumption
{
    public class HourlyConsumptionModelEntity
    {
        [JsonPropertyName("consumption")]
        public double? Consumption { get; set; }

        [JsonPropertyName("date")]
        public string? Date { get; set; }
    }
}
