using Microsoft.AspNetCore.Http;
using MongoDB.Bson;
using MongoDB.Driver;
using SP.Exceptions;
using SP.Users.Models;
using SP.Users.Models.RequestParams;
using SP.Utils.Cryptography;
using SP.Utils.Jwt;

namespace SP.Users.Service
{
    public class UsersService : IUsersService
    {
        readonly IMongoCollection<Account> _users;
        readonly IJwtUtils _jwtUtils;
        readonly ICryptographyUtils _cryptUtils;

        public UsersService(IMongoClient client, IJwtUtils jwtUtils, ICryptographyUtils cryptUtils)
        {
            this._users = client.GetDatabase("cluster0").GetCollection<Account>("users");
            _jwtUtils = jwtUtils;
            _cryptUtils = cryptUtils;
        }

        public Account? GetAccountById(ObjectId id)
        {
            var user = _users.Find(u => u.Id == id).FirstOrDefault();
            if (user == null) return null;
            return user;
        }

        public AuthUser RefreshToken(string token)
        {
            var userId = _jwtUtils.ValidateToken(token);
            if (userId == null)
                throw new HttpResponseException(statusCode: StatusCodes.Status400BadRequest, new { message = "Be sure to provide an elligible token." });

            var account = GetAccountById((ObjectId)userId);
            if (account == null)
                throw new HttpResponseException(statusCode: StatusCodes.Status404NotFound, new { message = "Account does not exist." });

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

        public AuthUser Register(UserRegisterRequestBody r)
        {
            if (IsAccountExisting(r.Email))
                throw new HttpResponseException(statusCode: StatusCodes.Status409Conflict, new { message = "Account already exists." });
            var (hashedPassword, salt) = _cryptUtils.Encrypt(r.Password);
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

            _users.InsertOne(user);
            var token = _jwtUtils.GenerateToken(user.Id);
            return new AuthUser(user, token);
        }

        public AuthUser Login(UserLoginRequestBody r)
        {
            var user = _users.Find(u => u.Email == r.Email).FirstOrDefault();

            if (user == null)
                throw new HttpResponseException(StatusCodes.Status404NotFound, new { message = "Account does not exist." });

            if (!_cryptUtils.IsPasswordCorrect(r.Password, user.Password, user.Salt))
                throw new HttpResponseException(statusCode: 401, new { message = "Incorrect password." });

            var token = _jwtUtils.GenerateToken(user.Id);
            return new AuthUser(user, token);
        }

        public void UpdateRoles(string userId, UpdateAccountRolesRequestBody r)
        {
            var uid = ObjectId.Parse(userId);
            var update = Builders<Account>.Update.Set("roles", r.Roles);
            var result = _users.UpdateOne(u => u.Id == uid, update);

            if (!result.IsAcknowledged)
                throw new HttpResponseException(StatusCodes.Status502BadGateway, new { message = "Could not assign role." });
        }

        public void UpdateIsActive(string userId, UpdateAccountIsActiveRequestBody r)
        {
            var uid = ObjectId.Parse(userId);
            var update = Builders<Account>.Update.Set("isActive", r.IsActive);
            var result = _users.UpdateOne(u => u.Id == uid, update);

            if (!result.IsAcknowledged)
                throw new HttpResponseException(StatusCodes.Status502BadGateway, new { message = "Could not update active state." });
        }

        public IEnumerable<User> GetAllUsers()
        {
            return _users.Find(_ => true).ToEnumerable().Select(u => (User)u);
        }
    }
}
