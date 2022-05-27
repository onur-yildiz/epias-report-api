using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SP.User.Models
{
    [BsonIgnoreExtraElements]
    public class UserInfo : IUserInfo
    {
        [BsonId]
        public ObjectId Id { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("email")]
        public string Email { get; set; }
    }
}
