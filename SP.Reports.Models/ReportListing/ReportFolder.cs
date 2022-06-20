using MongoDB.Bson.Serialization.Attributes;

namespace SP.Reports.Models.ReportListing
{
    [BsonIgnoreExtraElements]
    public class ReportFolder : IReportFolder
    {
        public ReportFolder(string order, HashSet<ReportName> name)
        {
            Order = order;
            Name = name;
        }

        [BsonRequired]
        [BsonElement("order")]
        public string Order { get; set; }

        [BsonRequired]
        [BsonElement("name")]
        public HashSet<ReportName> Name { get; set; }
    }
}
