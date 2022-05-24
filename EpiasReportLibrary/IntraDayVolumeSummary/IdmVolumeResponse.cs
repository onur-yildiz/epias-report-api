using System.Text.Json.Serialization;

namespace EpiasReportLibrary.IdmVolume
{
    public class IdmVolumeResponse : IResponseBase<IdmVolumeContainer>
    {
        [JsonPropertyName("resultCode")]
        public string? ResultCode { get; set; }

        [JsonPropertyName("resultDescription")]
        public string? ResultDescription { get; set; }

        [JsonPropertyName("body")]
        public IdmVolumeContainer? Body { get; set; }
    }
}
