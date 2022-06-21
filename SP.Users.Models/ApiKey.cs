using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SP.Users.Models
{
    [BsonIgnoreExtraElements]
    public class ApiKey : IApiKey
    {
        public ApiKey(ObjectId userId, string key, string app)
        {
            UserId = userId;
            Key = key;
            App = app;
        }

        public ApiKey(string userId, string key, string app)
        {
            UserId = ObjectId.Parse(userId);
            Key = key;
            App = app;
        }

        [BsonRequired]
        [BsonElement("userId")]
        public ObjectId UserId { get; set; }

        [BsonRequired]
        [BsonElement("key")]
        public string Key { get; set; }

        [BsonRequired]
        [BsonElement("app")]
        public string App { get; set; }
    }
}
