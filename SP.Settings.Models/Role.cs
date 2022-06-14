using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.Settings.Models
{
    [BsonIgnoreExtraElements]
    public class Role
    {
        [Required]
        [BsonRequired]
        [BsonElement("name")]
        public string Name { get; set; }
    }
}
