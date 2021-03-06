using MongoDB.Bson.Serialization.Attributes;

namespace SP.ExtraReports.Models
{
    /// <summary>
    /// Generated electricity by renewable sources
    /// </summary>
    public class Renewable : IRenewable
    {
        [BsonRequired]
        [BsonElement("sun")]
        public double Sun { get; set; }

        [BsonRequired]
        [BsonElement("wind")]
        public double Wind { get; set; }

        [BsonRequired]
        [BsonElement("biomass")]
        public double Biomass { get; set; }

        [BsonRequired]
        [BsonElement("dammedHydro")]
        public double DammedHydro { get; set; }

        [BsonRequired]
        [BsonElement("river")]
        public double River { get; set; }

        [BsonRequired]
        [BsonElement("geothermal")]
        public double Geothermal { get; set; }
    }
}
