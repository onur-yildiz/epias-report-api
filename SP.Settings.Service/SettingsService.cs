

using Microsoft.AspNetCore.Http;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using SP.Exceptions;
using SP.Settings.Models;
using SP.User.Models;
using SP.Utils.Jwt;

namespace SP.Settings.Service
{
    public class SettingsService : ISettingsService
    {
        readonly IMongoCollection<BsonDocument> _roles;

        public SettingsService(IMongoClient client, IJwtUtils jwtUtils)
        {
            var db = client.GetDatabase("cluster0");
            this._roles = db.GetCollection<BsonDocument>("roles");
        }

        public IEnumerable<Role>? GetRoles()
        {
            var roles = _roles.Find(_ => true).ToList();
            if (roles == null) throw new HttpResponseException(StatusCodes.Status404NotFound, new { message = "Could not find any roles." });
            return roles.Select(role => BsonSerializer.Deserialize<Role>(role));
        }

        public Role? GetRole(string role)
        {
            var builder = Builders<BsonDocument>.Filter;
            var filter = builder.Eq("name", role);
            var roleDoc = _roles.Find(filter).FirstOrDefault();
            if (roleDoc == null) throw new HttpResponseException(StatusCodes.Status404NotFound, new { message = "Role does not exist." });
            return BsonSerializer.Deserialize<Role>(roleDoc);
        }

        public void CreateRole(Role role)
        {
            var builder = Builders<BsonDocument>.Filter;
            var filter = builder.Eq("name", role.Name);
            var roleDoc = _roles.Find(filter).FirstOrDefault();
            if (roleDoc != null) throw new HttpResponseException(StatusCodes.Status400BadRequest, new { message = "Role already exists." });
            _roles.InsertOne(role.ToBsonDocument());
        }
    }
}
