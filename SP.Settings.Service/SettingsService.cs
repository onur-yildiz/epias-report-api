

using Microsoft.AspNetCore.Http;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using SP.Exceptions;
using SP.Reports.Models.ReportListing;
using SP.Settings.Models;
using SP.User.Models;
using SP.Utils.Jwt;

namespace SP.Settings.Service
{
    public class SettingsService : ISettingsService
    {
        readonly IMongoCollection<BsonDocument> _roles;
        readonly IMongoCollection<Account> _users;
        readonly IMongoCollection<Report> _reports;

        public SettingsService(IMongoClient client, IJwtUtils jwtUtils)
        {
            var db = client.GetDatabase("cluster0");
            this._roles = db.GetCollection<BsonDocument>("roles");
            this._users = db.GetCollection<Account>("users");
            this._reports = db.GetCollection<Report>("reports");
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
        public void DeleteRole(Role role)
        {
            var builder = Builders<BsonDocument>.Filter;
            var filter = builder.Eq("name", role.Name);
            var result = _roles.DeleteOne(filter);
            if (!result.IsAcknowledged) throw new HttpResponseException(StatusCodes.Status502BadGateway, new { message = "Could not delete role." });
            _users.UpdateMany(u => u.Roles.Contains(role.Name), Builders<Account>.Update.Pull("roles", role.Name));
            _reports.UpdateMany(r => r.Roles.Contains(role.Name), Builders<Report>.Update.Pull("roles", role.Name));
        }
    }
}
