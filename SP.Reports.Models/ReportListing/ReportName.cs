using MongoDB.Bson.Serialization.Attributes;

namespace SP.Reports.Models.ReportListing
{
    public class ReportName : IReportName
    {
        public ReportName(string lang, string @short, string @long)
        {
            Lang = lang;
            Short = @short;
            Long = @long;
        }

        /// <summary>
        /// Language code
        /// </summary>
        [BsonRequired]
        [BsonElement("lang")]
        public string Lang { get; set; }

        /// <summary>
        /// Short name
        /// </summary>
        [BsonRequired]
        [BsonElement("short")]
        public string Short { get; set; }

        /// <summary>
        /// Long name
        /// </summary>
        [BsonRequired]
        [BsonElement("long")]
        public string Long { get; set; }
    }
}
