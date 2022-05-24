using System.Text.Json.Serialization;

namespace EpiasReportLibrary
{
    public class MarketStatistic
    {
        [JsonPropertyName("date")]
        public string? Date { get; set; }

        [JsonPropertyName("min")]
        public double? Min { get; set; }

        [JsonPropertyName("max")]
        public double? Max { get; set; }

        [JsonPropertyName("average")]
        public double? Average { get; set; }

        [JsonPropertyName("weightedAverage")]
        public double? WeightedAverage { get; set; }

        [JsonPropertyName("summary")]
        public double? Summary { get; set; }
    }
}
