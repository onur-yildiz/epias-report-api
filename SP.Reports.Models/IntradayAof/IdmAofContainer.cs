using System.Text.Json.Serialization;

namespace SP.Reports.Models.IntraDayAof
{
    public class IdmAofContainer
    {
        [JsonPropertyName("idmAofList")]
        public IdmAof[]? IdmAofList { get; set; }

        [JsonPropertyName("statistics")]
        public MarketStatistic[]? Statistics { get; set; }
    }
}
