﻿using MongoDB.Bson.Serialization.Attributes;

namespace SP.Reports.Models.ReportListing
{
    public class ReportName
    {
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
