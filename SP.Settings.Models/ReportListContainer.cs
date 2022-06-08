using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.Settings.Models
{
    public class ReportListContainer
    {
        [BsonId]
        public ObjectId Id { get; set; }

        [BsonRequired]
        [BsonElement("version")]
        public string Version { get; set; }

        [BsonRequired]
        [BsonElement("folderSystem")]
        public Report[] FolderSystem { get; set; }
    }
}
