using MongoDB.Bson.Serialization.Attributes;

namespace SP.ExtraReports.Models
{
    public class NonRenewable
    {
        [BsonRequired]
        [BsonElement("fueloil")]
        public double Fueloil { get; set; }

        [BsonRequired]
        [BsonElement("gasOil")]
        public double GasOil { get; set; }

        [BsonRequired]
        [BsonElement("blackCoal")]
        public double BlackCoal { get; set; }

        [BsonRequired]
        [BsonElement("lignite")]
        public double Lignite { get; set; }

        [BsonRequired]
        [BsonElement("naturalGas")]
        public double NaturalGas { get; set; }

        [BsonRequired]
        [BsonElement("lng")]
        public double Lng { get; set; }

        [BsonRequired]
        [BsonElement("naphta")]
        public double Naphta { get; set; }

        [BsonRequired]
        [BsonElement("importCoal")]
        public double ImportCoal { get; set; }

        [BsonRequired]
        [BsonElement("asphaltiteCoal")]
        public double AsphaltiteCoal { get; set; }

        [BsonRequired]
        [BsonElement("nucklear")]
        public double Nucklear { get; set; }

        [BsonRequired]
        [BsonElement("wasteheat")]
        public double Wasteheat { get; set; }
    }
}
