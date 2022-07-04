using MongoDB.Bson;
using MongoDB.Driver;
using SP.DAL.Interfaces;
using SP.Users.Models;

namespace SP.DAL
{
    public class ApiKeyRepository : GenericRepository<ApiKey>, IApiKeyRepository
    {
        public ApiKeyRepository(IMongoDatabase database) : base(database, "api-keys")
        {
        }
    }
}
