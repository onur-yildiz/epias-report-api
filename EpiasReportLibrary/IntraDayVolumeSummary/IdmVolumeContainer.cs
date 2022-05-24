using System.Text.Json.Serialization;

namespace EpiasReportLibrary.IdmVolume
{
    public class IdmVolumeContainer
    {
        [JsonPropertyName("volumes")]
        public IdmVolume[]? Volumes { get; set; }

        [JsonPropertyName("statistics")]
        public MarketStatistic[]? Statistics { get; set; }
    }
}
