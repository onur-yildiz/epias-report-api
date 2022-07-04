using MongoDB.Bson.Serialization.Attributes;
using SP.EpiasReports.Models;

namespace SP.Reports.Models.ReportListing
{
    public class ReportFolder : MongoDbEntity, IReportFolder
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
