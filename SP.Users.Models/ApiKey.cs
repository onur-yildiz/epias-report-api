using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using SP.EpiasReports.Models;

namespace SP.Users.Models
{
    public class ApiKey : MongoDbEntity, IApiKey
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

        /// <summary>
        /// Owner's ID
        /// </summary>
        [BsonRequired]
        [BsonElement("userId")]
        public ObjectId UserId { get; set; }

        /// <summary>
        /// API Key
        /// </summary>
        [BsonRequired]
        [BsonElement("key")]
        public string Key { get; set; }

        /// <summary>
        /// App name
        /// </summary>
        [BsonRequired]
        [BsonElement("app")]
        public string App { get; set; }
    }
}
