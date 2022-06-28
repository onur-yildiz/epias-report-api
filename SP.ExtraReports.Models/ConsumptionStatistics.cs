using MongoDB.Bson.Serialization.Attributes;

namespace SP.ExtraReports.Models
{
    /// <summary>
    /// Consumption statistics of a month for the periods which consumed the most electricity Includes hour with the highest consume for each day and top 5 electricity consumed days. 
    /// </summary>
    [BsonIgnoreExtraElements]
    public class ConsumptionStatistics : IConsumptionStatistics
    {
        public ConsumptionStatistics(Period period, IEnumerable<MostConsumedPeriod> mostConsumedHours, IEnumerable<MostConsumedPeriod> mostConsumedDays)
        {
            Period = period;
            MostConsumedHours = mostConsumedHours;
            MostConsumedDays = mostConsumedDays;
        }

        /// <summary>
        /// Date range of the statistics
        /// </summary>
        [BsonElement("period")]
        public Period Period { get; set; }

        /// <summary>
        /// List of hours which the electricity is consumed the most that day for each day in the month
        /// </summary>
        [BsonRequired]
        [BsonElement("mostConsumedHours")]
        public IEnumerable<MostConsumedPeriod> MostConsumedHours { get; set; }

        /// <summary>
        /// Top 5 days which the electricity is consumed the most for the month
        /// </summary>
        [BsonRequired]
        [BsonElement("mostConsumedDays")]
        public IEnumerable<MostConsumedPeriod> MostConsumedDays { get; set; }
    }
}
