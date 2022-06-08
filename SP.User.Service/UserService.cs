using Microsoft.AspNetCore.Http;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using SP.Exceptions;
using SP.User.Models;
using SP.User.Models.RequestParams;
using SP.User.Service.Jwt;

namespace SP.User.Service
{
    public class UserService : IUserService
    {
        //readonly IMongoClient _client;
        readonly IMongoCollection<BsonDocument> _users;
        readonly IJwtUtils _jwtUtils;

        public UserService(IMongoClient client, IJwtUtils jwtUtils)
        {
            //this._client = client;
            this._users = client.GetDatabase("cluster0").GetCollection<BsonDocument>("users");
            _jwtUtils = jwtUtils;
        }

        public Account? GetAccountById(ObjectId id)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("_id", id);
            var bsonUser = _users.Find(filter).FirstOrDefault();
            if (bsonUser == null) return null;
            return BsonSerializer.Deserialize<Account>(bsonUser);
        }

        public AuthUserData GetUserDataByToken(string token)
        {
            var userId = _jwtUtils.ValidateToken(token);
            if (userId == null)
                throw new HttpResponseException(statusCode: StatusCodes.Status400BadRequest, new { message = "Be sure to provide an elligible token." });

            var account = GetAccountById((ObjectId)userId);
            if (account == null)
                throw new HttpResponseException(statusCode: StatusCodes.Status404NotFound, new { message = "Account does not exist." });

            var refreshedToken = _jwtUtils.GenerateToken(account.Id);
            return new AuthUserData(account, refreshedToken);
        }

        public bool IsAccountExisting(string email)
        {
            try
            {
                var filter = Builders<BsonDocument>.Filter.Eq("email", email);
                var count = _users.Find(filter).CountDocuments();
                return count > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public AuthUserData Register(UserRegisterRequestParams r)
        {
            if (IsAccountExisting(r.Email))
                throw new HttpResponseException(statusCode: StatusCodes.Status409Conflict, new { message = "Account already exists." });
            var (hashedPassword, salt) = CryptographyUtils.Encrypt(r.Password);
            var user = new Account
            {
                Id = ObjectId.GenerateNewId(),
                Email = r.Email,
                Name = r.Name,
                Password = hashedPassword,
                Salt = salt,
                LanguageCode = r.LanguageCode,
                IsActive = true,
                Roles = new HashSet<string>()
            };

            _users.InsertOne(user.ToBsonDocument());
            var token = _jwtUtils.GenerateToken(user.Id);
            return new AuthUserData(user, token);
        }

        public AuthUserData Login(UserLoginRequestParams r)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("email", r.Email);
            var userBson = _users.Find(filter).FirstOrDefault();

            if (userBson == null)
                throw new HttpResponseException(StatusCodes.Status404NotFound, new { message = "Account does not exist." });

            var user = BsonSerializer.Deserialize<Account>(userBson);

            if (!CryptographyUtils.IsPasswordCorrect(r.Password, user.Password, user.Salt))
                throw new HttpResponseException(statusCode: 401, new { message = "Incorrect password." });

            var token = _jwtUtils.GenerateToken(user.Id);
            return new AuthUserData(user, token);
        }

        // TODO DRY
        public void AssignRole(UpdateRoleRequestParams r)
        {
            var builder = Builders<BsonDocument>.Filter;
            var emailFilter = builder.Eq("email", r.AssigneeEmail);
            var roleFilter = builder.And(emailFilter, builder.Eq("roles", r.Role));
            var isRoleExisting = _users.Find(roleFilter).Any();
            if (isRoleExisting) return;
            var update = Builders<BsonDocument>.Update.Push("roles", r.Role);
            var result = _users.UpdateOne(emailFilter, update);

            if (!result.IsAcknowledged)
                throw new HttpResponseException(StatusCodes.Status502BadGateway, new { message = "Could not assign role." });
        }

        public void RemoveRole(UpdateRoleRequestParams r)
        {
            var builder = Builders<BsonDocument>.Filter;
            var emailFilter = builder.Eq("email", r.AssigneeEmail);
            var roleFilter = builder.And(emailFilter, builder.Eq("roles", r.Role));
            var isRoleExisting = _users.Find(roleFilter).Any();
            if (!isRoleExisting) return;
            var update = Builders<BsonDocument>.Update.Pull("roles", r.Role);
            var result = _users.UpdateOne(emailFilter, update);

            if (!result.IsAcknowledged)
                throw new HttpResponseException(StatusCodes.Status502BadGateway, new { message = "Could not remove role." });
        }

        public void UpdateIsActive(UpdateIsActiveRequestParams r)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("email", r.AssigneeEmail);
            var update = Builders<BsonDocument>.Update.Set("isActive", r.IsActive);
            var result = _users.UpdateOne(filter, update);

            if (!result.IsAcknowledged)
                throw new HttpResponseException(StatusCodes.Status502BadGateway, new { message = "Could not update active state." });
        }
    }
}
