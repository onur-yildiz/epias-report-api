using System.Text.Json.Serialization;

namespace EpiasReportLibrary.Dpp
{
    public class Dpp
    {
        [JsonPropertyName("akarsu")]
        public double? Akarsu { get; set; }

        [JsonPropertyName("barajli")]
        public double? Barajli { get; set; }

        [JsonPropertyName("biokutle")]
        public double? Biokutle { get; set; }

        [JsonPropertyName("diger")]
        public double? Diger { get; set; }

        [JsonPropertyName("dogalgaz")]
        public double? Dogalgaz { get; set; }

        [JsonPropertyName("fuelOil")]
        public double? FuelOil { get; set; }

        [JsonPropertyName("ithalKomur")]
        public double? IthalKomur { get; set; }

        [JsonPropertyName("jeotermal")]
        public double? Jeotermal { get; set; }

        [JsonPropertyName("linyit")]
        public double? Linyit { get; set; }

        [JsonPropertyName("nafta")]
        public double? Nafta { get; set; }

        [JsonPropertyName("ruzgar")]
        public double? Ruzgar { get; set; }

        [JsonPropertyName("saat")]
        public string? Saat { get; set; }

        [JsonPropertyName("tarih")]
        public string? Tarih { get; set; }

        [JsonPropertyName("tasKomur")]
        public double? TasKomur { get; set; }

        [JsonPropertyName("toplam")]
        public double? Toplam { get; set; }
    }
}
