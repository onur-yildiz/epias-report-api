using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

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
