using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using SP.EpiasReports.Models;
using System.ComponentModel.DataAnnotations;

namespace SP.Roles.Models
{
    [BsonIgnoreExtraElements]
    public class Role : MongoDbEntity, IRole
    {
        /// <summary>
        /// Name of the role
        /// </summary>
        [Required]
        [BsonRequired]
        [BsonElement("name")]
        public string Name { get; set; } = String.Empty;
    }
}
