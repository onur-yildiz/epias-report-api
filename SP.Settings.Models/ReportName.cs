using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.Settings.Models
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
