using MongoDB.Bson.Serialization.Attributes;

namespace SP.ExtraReports.Models
{
    [BsonIgnoreExtraElements]
    public class ConsumptionStatistics : IConsumptionStatistics
    {
        public ConsumptionStatistics(Period period, IEnumerable<MostConsumedPeriod> mostConsumedHours, IEnumerable<MostConsumedPeriod> mostConsumedDays)
        {
            Period = period;
            MostConsumedHours = mostConsumedHours;
            MostConsumedDays = mostConsumedDays;
        }

        [BsonElement("period")]
        public Period Period { get; set; }

        [BsonRequired]
        [BsonElement("mostConsumedHours")]
        public IEnumerable<MostConsumedPeriod> MostConsumedHours { get; set; }

        [BsonRequired]
        [BsonElement("mostConsumedDays")]
        public IEnumerable<MostConsumedPeriod> MostConsumedDays { get; set; }
    }
}
