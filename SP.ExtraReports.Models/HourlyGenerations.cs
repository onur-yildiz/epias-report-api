using MongoDB.Bson.Serialization.Attributes;
using SP.EpiasReports.Models;
using SP.Reports.Models.RealTimeGeneration;

namespace SP.ExtraReports.Models
{
    /// <summary>
    /// Hourly electric generation values categorized by renewable and non-renewable energy types.
    /// </summary>
    public class HourlyGenerations : MongoDbEntity, IHourlyGenerations
    {
        public HourlyGenerations(HourlyGenerationModelEntity h)
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
            Date = DateTime.Parse(h.Date!).ToUniversalTime();
        }

        /// <summary>
        /// Renewable energy sources
        /// </summary>
        [BsonRequired]
        [BsonElement("renewable")]
        public Renewable Renewable { get; set; }

        /// <summary>
        /// Non-renewable energy sources
        /// </summary>
        [BsonRequired]
        [BsonElement("nonRenewable")]
        public NonRenewable NonRenewable { get; set; }

        /// <summary>
        /// Import/export values from unknown sources
        /// </summary>
        [BsonRequired]
        [BsonElement("importExport")]
        public double ImportExport { get; set; }

        /// <summary>
        /// Total generated renewable electricity
        /// </summary>
        [BsonRequired]
        [BsonElement("renewableTotal")]
        public double RenewableTotal { get; set; }

        /// <summary>
        /// Total generated non-renewable electricity
        /// </summary>
        [BsonRequired]
        [BsonElement("nonRenewableTotal")]
        public double NonRenewableTotal { get; set; }

        /// <summary>
        /// Total generated electricity
        /// </summary>
        [BsonRequired]
        [BsonElement("total")]
        public double Total { get; set; }

        /// <summary>
        /// Date
        /// </summary>
        [BsonRequired]
        [BsonDateTimeOptions(Kind = DateTimeKind.Utc, Representation = MongoDB.Bson.BsonType.DateTime)]
        [BsonElement("date")]
        public DateTime Date { get; set; }
    }
}