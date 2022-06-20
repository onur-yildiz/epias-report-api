using MongoDB.Bson.Serialization.Attributes;

namespace SP.ExtraReports.Models
{
    public class Period
    {
        public Period(DateTime start, DateTime end)
        {
            Start = start;
            End = end;
        }

        [BsonRequired]
        [BsonDateTimeOptions(Kind = DateTimeKind.Utc, Representation = MongoDB.Bson.BsonType.DateTime)]
        [BsonElement("start")]
        public DateTime Start { get; set; }

        [BsonRequired]
        [BsonDateTimeOptions(Kind = DateTimeKind.Utc, Representation = MongoDB.Bson.BsonType.DateTime)]
        [BsonElement("end")]
        public DateTime End { get; set; }
    }
}
