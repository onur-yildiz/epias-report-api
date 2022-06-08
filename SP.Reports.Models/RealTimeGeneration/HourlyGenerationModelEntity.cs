using System.Text.Json.Serialization;

namespace SP.Reports.Models.RealTimeGeneration
{
    public class HourlyGenerationModelEntity
    {
        [JsonPropertyName("asphaltiteCoal")]
        public double AsphaltiteCoal { get; set; }

        [JsonPropertyName("biomass")]
        public double Biomass { get; set; }

        [JsonPropertyName("blackCoal")]
        public double BlackCoal { get; set; }

        [JsonPropertyName("dammedHydro")]
        public double DammedHydro { get; set; }

        [JsonPropertyName("date")]
        public string? Date { get; set; }

        [JsonPropertyName("fueloil")]
        public double Fueloil { get; set; }

        [JsonPropertyName("gasOil")]
        public double GasOil { get; set; }

        [JsonPropertyName("geothermal")]
        public double Geothermal { get; set; }

        [JsonPropertyName("importCoal")]
        public double ImportCoal { get; set; }

        [JsonPropertyName("importExport")]
        public double ImportExport { get; set; }

        [JsonPropertyName("lignite")]
        public double Lignite { get; set; }

        [JsonPropertyName("lng")]
        public double Lng { get; set; }

        [JsonPropertyName("naphta")]
        public double Naphta { get; set; }

        [JsonPropertyName("naturalGas")]
        public double NaturalGas { get; set; }

        [JsonPropertyName("nucklear")]
        public double Nucklear { get; set; }

        [JsonPropertyName("river")]
        public double River { get; set; }

        [JsonPropertyName("sun")]
        public double Sun { get; set; }

        [JsonPropertyName("wind")]
        public double Wind { get; set; }

        [JsonPropertyName("wasteheat")]
        public double Wasteheat { get; set; }

        [JsonPropertyName("total")]
        public double Total { get; set; }

    }
}
