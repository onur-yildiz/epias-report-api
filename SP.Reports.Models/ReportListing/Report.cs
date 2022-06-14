using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.Reports.Models.ReportListing
{
    [BsonIgnoreExtraElements]
    public class Report
    {
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
