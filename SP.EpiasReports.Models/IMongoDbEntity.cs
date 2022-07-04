using MongoDB.Bson;

namespace SP.EpiasReports.Models
{
    public interface IMongoDbEntity
    {
        ObjectId Id { get; set; }
    }
}