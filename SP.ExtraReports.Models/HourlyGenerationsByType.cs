using MongoDB.Bson.Serialization.Attributes;
using SP.Reports.Models.RealTimeGeneration;

namespace SP.ExtraReports.Models
{
    [BsonIgnoreExtraElements]
    public class HourlyGenerationsByType
    {
        public HourlyGenerationsByType(HourlyGenerationModelEntity h)
        {
            Renewable = new Renewable
            {
                Biomass = h.Biomass,
                DammedHydro = h.DammedHydro,
                Geothermal = h.Geothermal,
                River = h.River,
                Sun = h.Sun,
                Wind = h.Wind,
            };
            NonRenewable = new NonRenewable
            {
                AsphaltiteCoal = h.AsphaltiteCoal,
                BlackCoal = h.BlackCoal,
                Fueloil = h.Fueloil,
                GasOil = h.GasOil,
                ImportCoal = h.ImportCoal,
                Lignite = h.Lignite,
                Lng = h.Lng,
                Naphta = h.Naphta,
                NaturalGas = h.NaturalGas,
                Nucklear = h.Nucklear,
                Wasteheat = h.Wasteheat
            };

            ImportExport = h.ImportExport;
            RenewableTotal = Renewable.GetType().GetProperties().Aggregate(0.0, (prev, curr) => prev + (double?)curr.GetValue(Renewable, null) ?? 0);
            NonRenewableTotal = NonRenewable.GetType().GetProperties().Aggregate(0.0, (prev, curr) => prev + (double?)curr.GetValue(NonRenewable, null) ?? 0);
            Total = RenewableTotal + NonRenewableTotal;
            Date = DateTime.Parse(h.Date!);
        }

        [BsonRequired]
        [BsonElement("renewable")]
        public Renewable Renewable { get; set; }

        [BsonRequired]
        [BsonElement("nonRenewable")]
        public NonRenewable NonRenewable { get; set; }

        [BsonRequired]
        [BsonElement("importExport")]
        public double ImportExport { get; set; }

        [BsonRequired]
        [BsonElement("renewableTotal")]
        public double RenewableTotal { get; set; }

        [BsonRequired]
        [BsonElement("nonRenewableTotal")]
        public double NonRenewableTotal { get; set; }

        [BsonRequired]
        [BsonElement("total")]
        public double Total { get; set; }

        [BsonRequired]
        [BsonDateTimeOptions(Kind = DateTimeKind.Utc, Representation = MongoDB.Bson.BsonType.DateTime)]
        [BsonElement("date")]
        public DateTime Date { get; set; }
    }
}