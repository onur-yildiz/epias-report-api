using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace SP.Roles.Models
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
