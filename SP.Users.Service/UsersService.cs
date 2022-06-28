using Microsoft.AspNetCore.Http;
using MongoDB.Bson;
using MongoDB.Driver;
using SP.Exceptions;
using SP.Users.Models;
using SP.Users.Models.RequestParams;
using SP.Utils.Cryptography;
using SP.Utils.Jwt;
using System.Security.Cryptography;
using System.Text.RegularExpressions;

namespace SP.Users.Service
{
    public class UsersService : IUsersService
    {
        readonly IMongoCollection<Account> _users;
        readonly IMongoCollection<ApiKey> _apiKeys;
        readonly IJwtUtils _jwtUtils;
        readonly ICryptographyUtils _cryptUtils;

        public UsersService(IMongoClient client, IJwtUtils jwtUtils, ICryptographyUtils cryptUtils)
        {
            _users = client.GetDatabase("cluster0").GetCollection<Account>("users");
            _apiKeys = client.GetDatabase("cluster0").GetCollection<ApiKey>("api-keys");
            _jwtUtils = jwtUtils;
            _cryptUtils = cryptUtils;
        }

        Account ValidateAccount(string token)
        {
            var uid = _jwtUtils.ValidateToken(token);

            if (uid == null)
                throw HttpResponseException.InvalidToken();

            return GetAccountById((ObjectId)uid);
        }

        public Account GetAccountById(ObjectId id)
        {
            var account = _users.Find(u => u.Id == id).FirstOrDefault();

            if (account == null)
                throw HttpResponseException.NotExists("Account");

            return account;
        }

        public AuthUser RefreshToken(string token)
        {
            var account = ValidateAccount(token);
            var refreshedToken = _jwtUtils.GenerateToken(account.Id);
            return new AuthUser(account, refreshedToken);
        }

        public bool IsAccountExisting(string email)
        {
            try
            {
                var count = _users.Find(u => u.Email == email).CountDocuments();
                return count > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public AuthUser Register(IUserRegisterRequestBody r)
        {
            if (IsAccountExisting(r.Email))
                throw HttpResponseException.AlreadyExists("Account");
            var (hashedPassword, salt) = _cryptUtils.Encrypt(r.Password);
            var user = new Account
            (
                id: ObjectId.GenerateNewId(),
                email: r.Email,
                name: r.Name,
                password: hashedPassword,
                salt: salt,
                languageCode: r.LanguageCode,
                isActive: true,
                isAdmin: false,
                roles: new HashSet<string>(),
                apiKeys: new HashSet<string>()
            );

            _users.InsertOne(user);
            var token = _jwtUtils.GenerateToken(user.Id);
            return new AuthUser(user, token);
        }

        public AuthUser Login(IUserLoginRequestBody r)
        {
            var user = _users.Find(u => u.Email == r.Email).FirstOrDefault();

            if (user == null)
                throw HttpResponseException.NotExists("Account");

            if (!_cryptUtils.IsPasswordCorrect(r.Password, user.Password, user.Salt))
                throw HttpResponseException.IncorrectPassword();

            var token = _jwtUtils.GenerateToken(user.Id);
            return new AuthUser(user, token);
        }

        public void UpdateRoles(string userId, IUpdateAccountRolesRequestBody r)
        {
            var uid = ObjectId.Parse(userId);
            var update = Builders<Account>.Update.Set("roles", r.Roles);
            var result = _users.UpdateOne(u => u.Id == uid, update);

            if (!result.IsAcknowledged)
                throw HttpResponseException.DatabaseError("Could not assign role.");
        }

        public void UpdateIsActive(string userId, IUpdateAccountIsActiveRequestBody r)
        {
            var uid = ObjectId.Parse(userId);
            var update = Builders<Account>.Update.Set("isActive", r.IsActive);
            var result = _users.UpdateOne(u => u.Id == uid, update);

            if (!result.IsAcknowledged)
                throw HttpResponseException.DatabaseError("Could not update active state.");
        }

        public IEnumerable<ApiKey> GetApiKeys(string targetUserId)
        {
            var targetUserIdParsed = ObjectId.Parse(targetUserId);
            return _apiKeys.Find(a => a.UserId == targetUserIdParsed).ToEnumerable();
        }

        public string CreateApiKey(string targetUserId)
        {
            var targetUserIdParsed = ObjectId.Parse(targetUserId);

            if (_apiKeys.Find(a => a.UserId == targetUserIdParsed).CountDocuments() >= 3)
                throw HttpResponseException.MaxApiKeys();

            var apiKey = Regex.Replace(Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)), "[^A-Za-z0-9]", "");
            _apiKeys.InsertOne(new ApiKey(userId: targetUserIdParsed, key: apiKey, app: "ExtraReports"));
            return apiKey;
        }

        public void DeleteApiKey(string apiKey, string targetUserId)
        {
            var targetUserIdParsed = ObjectId.Parse(targetUserId);
            var result = _apiKeys.DeleteOne(a => a.Key == apiKey && a.UserId == targetUserIdParsed);

            if (!result.IsAcknowledged)
                throw HttpResponseException.DatabaseError("Could not delete API key.");
        }

        public IEnumerable<User> GetAllUsers()
        {
            return _users.Find(_ => true).ToEnumerable().Select(u => (User)u);
        }
    }
}
