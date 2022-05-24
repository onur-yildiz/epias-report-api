using System.Text.Json.Serialization;

namespace EpiasReportLibrary.RealTimeGeneration
{
    public class HourlyGenerationContainer
    {
        [JsonPropertyName("hourlyGenerations")]
        public HourlyGenerationModelEntity[]? HourlyGenerations { get; set; }

        [JsonPropertyName("statistics")]
        public HourlyGenerationModelStatistic[]? Statistics { get; set; }
    }
}
