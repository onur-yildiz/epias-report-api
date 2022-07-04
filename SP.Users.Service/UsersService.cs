using MongoDB.Bson;
using MongoDB.Driver;
using SP.DAL.Interfaces;
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
        readonly IUserRepository _userRepository;
        readonly IApiKeyRepository _apiKeyRepository;
        readonly IJwtUtils _jwtUtils;
        readonly ICryptographyUtils _cryptUtils;

        public UsersService(IUserRepository userRepository, IApiKeyRepository apiKeyRepository, IJwtUtils jwtUtils, ICryptographyUtils cryptUtils)
        {
            _userRepository = userRepository;
            _apiKeyRepository = apiKeyRepository;
            _jwtUtils = jwtUtils;
            _cryptUtils = cryptUtils;
        }

        public AuthUser RefreshToken(string token)
        {
            var uid = _jwtUtils.ValidateToken(token);

            if (uid == null)
                throw HttpResponseException.InvalidToken();

            var account = _userRepository.GetById((ObjectId)uid);

            if (account == null)
                throw HttpResponseException.NotExists("Account");

            var refreshedToken = _jwtUtils.GenerateToken(account.Id);
            return new AuthUser(account, refreshedToken);
        }

        public AuthUser Register(IUserRegisterRequestBody r)
        {
            if (_userRepository.GetByEmail(r.Email) != null)
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
                roles: new HashSet<string>()
            );

            _userRepository.AddOne(user);
            var token = _jwtUtils.GenerateToken(user.Id);
            return new AuthUser(user, token);
        }

        public AuthUser Login(IUserLoginRequestBody r)
        {
            var user = _userRepository.GetByEmail(r.Email);

            if (user == null)
                throw HttpResponseException.NotExists("Account");

            if (!_cryptUtils.IsPasswordCorrect(r.Password, user.Password, user.Salt))
                throw HttpResponseException.IncorrectPassword();

            var token = _jwtUtils.GenerateToken(user.Id);
            return new AuthUser(user, token);
        }

        public void UpdateRoles(string userId, IUpdateAccountRolesRequestBody r)
        {
            var account = _userRepository.UpdateOne_Set(userId, "roles", r.Roles);

            if (account == null)
                throw HttpResponseException.DatabaseError();
        }

        public void UpdateIsActive(string userId, IUpdateAccountIsActiveRequestBody r)
        {
            var account = _userRepository.UpdateOne_Set(userId, "isActive", r.IsActive);

            if (account == null)
                throw HttpResponseException.DatabaseError();
        }

        public IEnumerable<ApiKey> GetApiKeys(string targetUserId)
        {
            var targetUserIdParsed = ObjectId.Parse(targetUserId);
            return _apiKeyRepository.Get(a => a.UserId == targetUserIdParsed);
        }

        public string CreateApiKey(string targetUserId)
        {
            var targetUserIdParsed = ObjectId.Parse(targetUserId);
            if (_apiKeyRepository.Get(a => a.UserId == targetUserIdParsed).Count() >= 3)
                throw HttpResponseException.MaxApiKeys();

            var apiKey = Regex.Replace(Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)), "[^A-Za-z0-9]", "");
            _apiKeyRepository.AddOne(new ApiKey(targetUserId, apiKey, "ExtraReports"));
            return apiKey;
        }

        public void DeleteApiKey(string apiKey, string targetUserId)
        {
            var targetUserIdParsed = ObjectId.Parse(targetUserId);
            var removedApiKey = _apiKeyRepository.RemoveOne(a => a.Key == apiKey && a.UserId == targetUserIdParsed);

            if (removedApiKey == null)
                throw HttpResponseException.DatabaseError();
        }

        public IEnumerable<User> GetAllUsers()
        {
            return _userRepository.Get().Select(u => (User)u);
        }
    }
}
