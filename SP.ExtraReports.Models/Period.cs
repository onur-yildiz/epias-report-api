using MongoDB.Bson.Serialization.Attributes;

namespace SP.ExtraReports.Models
{
    /// <summary>
    /// Date range of the statistics
    /// </summary>
    public class Period : IPeriod
    {
        /// <summary>
        /// Time period
        /// </summary>
        public Period(DateTime start, DateTime end)
        {
            Start = start;
            End = end;
        }

        /// <summary>
        /// Start date of the period (inclusive)
        /// </summary>
        [BsonRequired]
        [BsonDateTimeOptions(Kind = DateTimeKind.Utc, Representation = MongoDB.Bson.BsonType.DateTime)]
        [BsonElement("start")]
        public DateTime Start { get; set; }

        /// <summary>
        /// End date of the period (inclusive)
        /// </summary>
        [BsonRequired]
        [BsonDateTimeOptions(Kind = DateTimeKind.Utc, Representation = MongoDB.Bson.BsonType.DateTime)]
        [BsonElement("end")]
        public DateTime End { get; set; }
    }
}
