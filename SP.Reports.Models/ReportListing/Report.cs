using MongoDB.Bson.Serialization.Attributes;

namespace SP.Reports.Models.ReportListing
{
    [BsonIgnoreExtraElements]
    public class Report : IReport
    {
        public Report(string order, string key, string endpoint, bool ısActive, HashSet<string> roles, HashSet<ReportName> name)
        {
            Order = order;
            Key = key;
            Endpoint = endpoint;
            IsActive = ısActive;
            Roles = roles;
            Name = name;
        }

        [BsonRequired]
        [BsonElement("order")]
        public string Order { get; set; }

        [BsonRequired]
        [BsonElement("key")]
        public string Key { get; set; }

        [BsonRequired]
        [BsonElement("endpoint")]
        public string Endpoint { get; set; }

        [BsonRequired]
        [BsonElement("isActive")]
        public bool IsActive { get; set; }

        [BsonRequired]
        [BsonElement("roles")]
        public HashSet<string> Roles { get; set; }

        [BsonRequired]
        [BsonElement("name")]
        public HashSet<ReportName> Name { get; set; }

    }
}
