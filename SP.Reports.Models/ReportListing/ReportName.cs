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

        [BsonRequired]
        [BsonElement("lang")]
        public string Lang { get; set; }

        [BsonRequired]
        [BsonElement("short")]
        public string Short { get; set; }

        [BsonRequired]
        [BsonElement("long")]
        public string Long { get; set; }
    }
}
