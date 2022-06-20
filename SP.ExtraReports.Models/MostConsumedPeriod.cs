using MongoDB.Bson.Serialization.Attributes;

namespace SP.ExtraReports.Models
{
    public class MostConsumedPeriod : IMostConsumedPeriod
    {
        public MostConsumedPeriod(string date, double consumption, double ratio)
        {
            Date = date;
            Consumption = consumption;
            Ratio = ratio;
        }

        [BsonRequired]
        [BsonElement("date")]
        public string Date { get; set; }

        [BsonRequired]
        [BsonElement("consumption")]
        public double Consumption { get; set; }

        [BsonRequired]
        [BsonElement("ratio")]
        public double Ratio { get; set; }
    }
}
