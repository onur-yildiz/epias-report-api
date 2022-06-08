

using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using SP.Settings.Models;

namespace SP.User.Service
{
    public class SettingsService : ISettingsService
    {
        readonly IMongoDatabase _db;

        public SettingsService(IMongoClient client)
        {
            this._db = client.GetDatabase("cluster0");
        }

        public ReportListContainer GetReportListingInfo()
        {
            var reportsCol = _db.GetCollection<BsonDocument>("reports");
            var reportsDocs = reportsCol.Find(_ => true).ToList();
            return reportsDocs.Select(doc => BsonSerializer.Deserialize<ReportListContainer>(doc)).Last();
        }
    }
}
