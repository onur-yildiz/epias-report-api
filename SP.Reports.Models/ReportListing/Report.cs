using MongoDB.Bson.Serialization.Attributes;

namespace SP.Reports.Models.ReportListing
{
    [BsonIgnoreExtraElements]
    public class Report : IReport
    {
        public Report(string order, string key, bool isActive, HashSet<string> roles, HashSet<ReportName> name)
        {
            Order = order;
            Key = key;
            IsActive = isActive;
            Roles = roles;
            Name = name;
        }

        /// <summary>
        /// Report's location in menu hierarchy
        /// </summary>
        [BsonRequired]
        [BsonElement("order")]
        public string Order { get; set; }

        /// <summary>
        /// Report's unique key
        /// </summary>
        [BsonRequired]
        [BsonElement("key")]
        public string Key { get; set; }

        /// <summary>
        /// Report's active state
        /// </summary>
        [BsonRequired]
        [BsonElement("isActive")]
        public bool IsActive { get; set; }

        /// <summary>
        /// Roles which can access this report.
        /// </summary>
        [BsonRequired]
        [BsonElement("roles")]
        public HashSet<string> Roles { get; set; }

        /// <summary>
        /// Report name
        /// </summary>
        [BsonRequired]
        [BsonElement("name")]
        public HashSet<ReportName> Name { get; set; }

    }
}