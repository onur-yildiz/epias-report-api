using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SP.EpiasReports.Models
{
    public abstract class MongoDbEntity : IMongoDbEntity
    {
        [BsonId]
        [BsonIgnoreIfDefault]
        public ObjectId Id { get; set; }
    }
}
