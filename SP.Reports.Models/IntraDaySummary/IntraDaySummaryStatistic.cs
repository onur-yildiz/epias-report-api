using System.Text.Json.Serialization;
namespace SP.Reports.Models.IntraDaySummary
{
    public class IntraDaySummaryStatistic
    {


        [JsonPropertyName("date")]
        public string? Date { get; set; }

        [JsonPropertyName("maxAskPriceMax")]
        public double? MaxAskPriceMax { get; set; }

        [JsonPropertyName("maxAskPriceMin")]
        public double? MaxAskPriceMin { get; set; }

        [JsonPropertyName("maxBidPriceMax")]
        public double? MaxBidPriceMax { get; set; }

        [JsonPropertyName("maxBidPriceMin")]
        public double? MaxBidPriceMin { get; set; }

        [JsonPropertyName("maxMatchPriceMax")]
        public double? MaxMatchPriceMax { get; set; }

        [JsonPropertyName("maxMatchPriceMin")]
        public double? MaxMatchPriceMin { get; set; }

        [JsonPropertyName("minAskPriceMax")]
        public double? MinAskPriceMax { get; set; }

        [JsonPropertyName("minAskPriceMin")]
        public double? MinAskPriceMin { get; set; }

        [JsonPropertyName("minBidPriceMax")]
        public double? MinBidPriceMax { get; set; }

        [JsonPropertyName("minBidPriceMin")]
        public double? MinBidPriceMin { get; set; }

        [JsonPropertyName("minMatchPriceMax")]
        public double? MinMatchPriceMax { get; set; }

        [JsonPropertyName("minMatchPriceMin")]
        public double? MinMatchPriceMin { get; set; }

        [JsonPropertyName("quantityOfAskAvg")]
        public double? QuantityOfAskAvg { get; set; }

        [JsonPropertyName("quantityOfAskMax")]
        public double? QuantityOfAskMax { get; set; }

        [JsonPropertyName("quantityOfAskMin")]
        public double? QuantityOfAskMin { get; set; }

        [JsonPropertyName("quantityOfAskSum")]
        public double? QuantityOfAskSum { get; set; }

        [JsonPropertyName("quantityOfBidAvg")]
        public double? QuantityOfBidAvg { get; set; }

        [JsonPropertyName("quantityOfBidMax")]
        public double? QuantityOfBidMax { get; set; }

        [JsonPropertyName("quantityOfBidMin")]
        public double? QuantityOfBidMin { get; set; }

        [JsonPropertyName("quantityOfBidSum")]
        public double? QuantityOfBidSum { get; set; }

        [JsonPropertyName("tradingVolumeAvg")]
        public double? TradingVolumeAvg { get; set; }

        [JsonPropertyName("tradingVolumeMax")]
        public double? TradingVolumeMax { get; set; }

        [JsonPropertyName("tradingVolumeMin")]
        public double? TradingVolumeMin { get; set; }

        [JsonPropertyName("tradingVolumeSum")]
        public double? TradingVolumeSum { get; set; }

        [JsonPropertyName("volumeAvg")]
        public double? VolumeAvg { get; set; }

        [JsonPropertyName("volumeMax")]
        public double? VolumeMax { get; set; }

        [JsonPropertyName("volumeMin")]
        public double? VolumeMin { get; set; }

        [JsonPropertyName("volumeSum")]
        public double? VolumeSum { get; set; }
    }
}

