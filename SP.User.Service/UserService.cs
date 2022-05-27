using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
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

        public bool IsAccountExisting(ObjectId id)
        {
            try
            {
                var filter = Builders<BsonDocument>.Filter.Eq("_id", id);
                var count = _users.Find(filter).CountDocuments();
                return count > 0;
            }
            catch (Exception)
            {
                return false;
            }
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

        public string Register(UserRegisterRequestParams r)
        {
            if (IsAccountExisting(r.Email)) throw new Exception { }; // TODO: HANDLE ACCOUNT ALREADY EXISTS
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
            return _jwtUtils.GenerateToken(user);
        }

        public string Login(UserLoginRequestParams r)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("email", r.Email);
            var userBson = _users.Find(filter).FirstOrDefault();
            if (userBson == null) throw new Exception { }; // TODO: ACCOUNT DOES NOT EXIST
            var user = BsonSerializer.Deserialize<Account>(userBson);

            if (!CryptographyUtils.IsPasswordCorrect(r.Password, user.Password, user.Salt)) throw new Exception { }; // TODO: HANDLE INCORRECT PASSWORD

            return _jwtUtils.GenerateToken(user);
        }

        // TODO DRY
        public void AssignRole(UpdateRoleRequestParams r)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("email", r.AssigneeEmail);
            var update = Builders<BsonDocument>.Update.Push("roles", r.Role);
            var result = _users.UpdateOne(filter, update);
            if (!result.IsAcknowledged) throw new Exception { }; // TODO: COULD NOT ASSIGN ROLE
        }

        public void RemoveRole(UpdateRoleRequestParams r)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("email", r.AssigneeEmail);
            var update = Builders<BsonDocument>.Update.Pull("roles", r.Role);
            var result = _users.UpdateOne(filter, update);
            if (!result.IsAcknowledged) throw new Exception { }; // TODO: COULD NOT ASSIGN ROLE
        }

        public void UpdateIsActive(UpdateIsActiveRequestParams r)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("email", r.AssigneeEmail);
            var update = Builders<BsonDocument>.Update.Set("isActive", r.IsActive);
            var result = _users.UpdateOne(filter, update);
            if (!result.IsAcknowledged) throw new Exception { }; // TODO: COULD NOT UPDATE ACTIVE STATE
        }
    }
}
