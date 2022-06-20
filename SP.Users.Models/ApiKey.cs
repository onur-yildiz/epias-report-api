using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.Users.Models
{
    [BsonIgnoreExtraElements]
    public class ApiKey
    {
        [BsonRequired]
        [BsonElement("key")]
        public string Key { get; set; }

        [BsonRequired]
        [BsonElement("app")]
        public string App { get; set; }
    }
}
