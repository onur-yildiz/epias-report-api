using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SP.Reports.Models.RealTimeConsumption
{
    public class HourlyConsumptionContainer
    {
        [JsonPropertyName("hourlyConsumptions")]
        public HourlyConsumptionModelEntity[]? HourlyConsumptions { get; set; }

        [JsonPropertyName("statistics")]
        public HourlyConsumptionStatisticModel[]? Statistics { get; set; }
    }
}
