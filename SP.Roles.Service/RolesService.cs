

using Microsoft.AspNetCore.Http;
using MongoDB.Driver;
using SP.Exceptions;
using SP.Reports.Models.ReportListing;
using SP.Roles.Models;
using SP.Users.Models;
using SP.Utils.Jwt;

namespace SP.Roles.Service
{
    public class RolesService : IRolesService
    {
        readonly IMongoCollection<Role> _roles;
        readonly IMongoCollection<Account> _users;
        readonly IMongoCollection<Report> _reports;

        public RolesService(IMongoClient client, IJwtUtils jwtUtils)
        {
            var db = client.GetDatabase("cluster0");
            this._roles = db.GetCollection<Role>("roles");
            this._users = db.GetCollection<Account>("users");
            this._reports = db.GetCollection<Report>("reports");
        }

        public IEnumerable<IRole>? GetRoles()
        {
            var roles = _roles.Find(_ => true).ToList();
            if (roles == null) throw new HttpResponseException(StatusCodes.Status404NotFound, new { message = "Could not find any roles." });
            return roles;
        }

        public IRole? GetRole(string roleName)
        {
            var builder = Builders<Role>.Filter;
            var filter = builder.Eq("name", roleName);
            var role = _roles.Find(filter).FirstOrDefault();
            if (role == null) throw new HttpResponseException(StatusCodes.Status404NotFound, new { message = "Role does not exist." });
            return role;
        }

        public void CreateRole(IRole role)
        {
            var builder = Builders<Role>.Filter;
            var filter = builder.Eq("name", role.Name);
            var existingRole = _roles.Find(filter).FirstOrDefault();
            if (existingRole != null) throw new HttpResponseException(StatusCodes.Status400BadRequest, new { message = "Role already exists." });
            _roles.InsertOne((Role)role);
        }
        public void DeleteRole(string roleName)
        {
            var builder = Builders<Role>.Filter;
            var filter = builder.Eq("name", roleName);
            var result = _roles.DeleteOne(filter);
            if (!result.IsAcknowledged) throw new HttpResponseException(StatusCodes.Status502BadGateway, new { message = "Could not delete role." });
            _users.UpdateMany(u => u.Roles.Contains(roleName), Builders<Account>.Update.Pull("roles", roleName));
            _reports.UpdateMany(r => r.Roles.Contains(roleName), Builders<Report>.Update.Pull("roles", roleName));
        }
    }
}
