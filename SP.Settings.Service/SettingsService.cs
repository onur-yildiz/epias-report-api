

using Microsoft.AspNetCore.Http;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using SP.Exceptions;
using SP.Settings.Models;
using SP.User.Models;
using SP.Utils.Jwt;

namespace SP.User.Service
{
    public class SettingsService : ISettingsService
    {
        //readonly IMongoDatabase _db;
        readonly IMongoCollection<BsonDocument> _roles;
        readonly IMongoCollection<BsonDocument> _reports;
        readonly IMongoCollection<Account> _users;
        readonly IJwtUtils _jwtUtils;

        public SettingsService(IMongoClient client, IJwtUtils jwtUtils)
        {
            var db = client.GetDatabase("cluster0");
            this._roles = db.GetCollection<BsonDocument>("roles");
            this._reports = db.GetCollection<BsonDocument>("report-listing-info");
            this._users = db.GetCollection<Account>("users");
            this._jwtUtils = jwtUtils;
        }

        private List<ReportHierarchyItem> FilterRoutes(List<ReportHierarchyItem> routes, string[] userRoles)
        {
            routes.RemoveAll(route =>
            {
                if (route.Children != null)
                {
                    var filteredChildren = FilterRoutes(route.Children.ToList(), userRoles);
                    if (filteredChildren.Count == 0) return true;
                    route.Children = filteredChildren;
                    return false;
                };
                if (route.Roles == null) return false;
                return !route.Roles.All(r => userRoles.Contains(r));
            });
            return routes;
        }

        // TODO DO BETTER
        public IEnumerable<ReportHierarchyItem>? GetReportListingInfo(string? authToken = null)
        {
            var reportsDocs = _reports.Find(_ => true).ToList();
            var reportHierarchy = reportsDocs.Select(doc => BsonSerializer.Deserialize<ReportHierarchyItem>(doc)).ToList();
            var userId = authToken != null ? _jwtUtils.ValidateToken(authToken) : null;
            var userRoles = new List<string>();
            if (userId != null) userRoles.AddRange(_users.Find(u => u.Id == userId).Project(u => u.Roles).FirstOrDefault());
            var filteredRoutes = FilterRoutes(reportHierarchy, userRoles.ToArray());
            return filteredRoutes;
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
            var filter = builder.Eq("name", role);
            var roleDoc = _roles.Find(filter).FirstOrDefault();
            if (roleDoc != null) throw new HttpResponseException(StatusCodes.Status400BadRequest, new { message = "Role already exists." });
            _roles.InsertOne(role.ToBsonDocument());
        }
    }
}
