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

        IAccount ValidateAccount(string token, ObjectId? targetUserId = null)
        {
            var uid = _jwtUtils.ValidateToken(token);
            if (uid == null)
                throw new HttpResponseException(statusCode: StatusCodes.Status400BadRequest, new { message = "Be sure to provide an elligible token." });

            var account = GetAccountById((ObjectId)uid);
            if (account == null)
                throw new HttpResponseException(statusCode: StatusCodes.Status404NotFound, new { message = "Account does not exist." });

            if (targetUserId != null && targetUserId != account.Id && !account.IsAdmin)
                throw new HttpResponseException(statusCode: StatusCodes.Status403Forbidden, new { message = "Account mismatch." });

            return account;
        }

        public IAccount? GetAccountById(ObjectId id)
        {
            var user = _users.Find(u => u.Id == id).FirstOrDefault();
            if (user == null) return null;
            return user;
        }

        public IAuthUser RefreshToken(string token)
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

        public IAuthUser Register(IUserRegisterRequestBody r)
        {
            if (IsAccountExisting(r.Email))
                throw new HttpResponseException(statusCode: StatusCodes.Status409Conflict, new { message = "Account already exists." });
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

        public IAuthUser Login(IUserLoginRequestBody r)
        {
            var user = _users.Find(u => u.Email == r.Email).FirstOrDefault();

            if (user == null)
                throw new HttpResponseException(StatusCodes.Status404NotFound, new { message = "Account does not exist." });

            if (!_cryptUtils.IsPasswordCorrect(r.Password, user.Password, user.Salt))
                throw new HttpResponseException(statusCode: 401, new { message = "Incorrect password." });

            var token = _jwtUtils.GenerateToken(user.Id);
            return new AuthUser(user, token);
        }

        public void UpdateRoles(string userId, IUpdateAccountRolesRequestBody r)
        {
            var uid = ObjectId.Parse(userId);
            var update = Builders<Account>.Update.Set("roles", r.Roles);
            var result = _users.UpdateOne(u => u.Id == uid, update);

            if (!result.IsAcknowledged)
                throw new HttpResponseException(StatusCodes.Status502BadGateway, new { message = "Could not assign role." });
        }

        public void UpdateIsActive(string userId, IUpdateAccountIsActiveRequestBody r)
        {
            var uid = ObjectId.Parse(userId);
            var update = Builders<Account>.Update.Set("isActive", r.IsActive);
            var result = _users.UpdateOne(u => u.Id == uid, update);

            if (!result.IsAcknowledged)
                throw new HttpResponseException(StatusCodes.Status502BadGateway, new { message = "Could not update active state." });
        }

        public IEnumerable<IApiKey> GetApiKeys(string token, string targetUserId)
        {
            var targetUserIdParsed = ObjectId.Parse(targetUserId);
            ValidateAccount(token, targetUserIdParsed);
            return _apiKeys.Find(a => a.UserId == targetUserIdParsed).ToEnumerable();
        }

        public string CreateApiKey(string token, string targetUserId)
        {
            var targetUserIdParsed = ObjectId.Parse(targetUserId);
            var account = ValidateAccount(token, targetUserIdParsed);

            if (_apiKeys.Find(a => a.UserId == account.Id).CountDocuments() >= 3)
                throw new HttpResponseException(statusCode: StatusCodes.Status406NotAcceptable, new { message = "Max allowed API keys reached." });

            var apiKey = Regex.Replace(Convert.ToBase64String(RandomNumberGenerator.GetBytes(128)), "[^A-Za-z0-9]", "");
            _apiKeys.InsertOne(new ApiKey(userId: targetUserIdParsed, key: apiKey, app: "ExtraReports"));
            return apiKey;
        }

        public void DeleteApiKey(string apiKey, string token, string targetUserId)
        {
            var targetUserIdParsed = ObjectId.Parse(targetUserId);
            ValidateAccount(token, targetUserIdParsed);

            var result = _apiKeys.DeleteOne(a => a.Key == apiKey && a.UserId == targetUserIdParsed);
            if (!result.IsAcknowledged)
                throw new HttpResponseException(StatusCodes.Status502BadGateway, new { message = "Could not delete API key." });
        }

        public IEnumerable<IUserBase<string>> GetAllUsers()
        {
            return _users.Find(_ => true).ToEnumerable().Select(u => (User)u);
        }
    }
}
