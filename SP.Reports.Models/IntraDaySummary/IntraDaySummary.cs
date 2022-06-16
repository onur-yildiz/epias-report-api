using System.Text.Json.Serialization;

namespace SP.Reports.Models.IntraDaySummary
{
    public class IntraDaySummary
    {
        [JsonPropertyName("contract")]
        public string? Contract { get; set; }

        [JsonPropertyName("date")]
        public string? Date { get; set; }

        [JsonPropertyName("id")]
        public Int64? Id { get; set; }

        [JsonPropertyName("maxAskPrice")]
        public double? MaxAskPrice { get; set; }

        [JsonPropertyName("maxBidPrice")]
        public double? MaxBidPrice { get; set; }

        [JsonPropertyName("maxMatchPrice")]
        public double? MaxMatchPrice { get; set; }

        [JsonPropertyName("minAskPrice")]
        public double? MinAskPrice { get; set; }

        [JsonPropertyName("minBidPrice")]
        public double? MinBidPrice { get; set; }

        [JsonPropertyName("minMatchPrice")]
        public double? MinMatchPrice { get; set; }

        [JsonPropertyName("quantityOfAsk")]
        public double? QuantityOfAsk { get; set; }

        [JsonPropertyName("quantityOfBid")]
        public double? QuantityOfBid { get; set; }

        [JsonPropertyName("tradingVolume")]
        public double? TradingVolume { get; set; }

        [JsonPropertyName("volume")]
        public double? Volume { get; set; }
    }
}
