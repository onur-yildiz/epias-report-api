using MongoDB.Bson.Serialization.Attributes;

namespace SP.ExtraReports.Models
{    
    /// <summary>
    /// Period which has consumed the most electricity
    /// </summary>
    public class MostConsumedPeriod : IMostConsumedPeriod
    {
        public MostConsumedPeriod(string date, double consumption, double ratio)
        {
            Date = date;
            Consumption = consumption;
            Ratio = ratio;
        }

        /// <summary>
        /// Date
        /// </summary>
        [BsonRequired]
        [BsonElement("date")]
        public string Date { get; set; }

        /// <summary>
        /// Consumption value
        /// </summary>
        [BsonRequired]
        [BsonElement("consumption")]
        public double Consumption { get; set; }

        /// <summary>
        /// Ratio of consumption compared to whole period
        /// </summary>
        [BsonRequired]
        [BsonElement("ratio")]
        public double Ratio { get; set; }
    }
}
