using MongoDB.Bson;
using SP.Users.Models;
using SP.Users.Models.RequestParams;

namespace SP.Users.Service
{
    public interface IUsersService
    {
        Account? GetAccountById(ObjectId id);
        AuthUser RefreshToken(string token);
        bool IsAccountExisting(string email);
        AuthUser Login(UserLoginRequestBody r);
        AuthUser Register(UserRegisterRequestBody r);
        void UpdateRoles(string userId, UpdateAccountRolesRequestBody r);
        void UpdateIsActive(string userId, UpdateAccountIsActiveRequestBody r);
        string CreateApiKey(string token);
        void DeleteApiKey(string apiKey, string token);
        public IEnumerable<User> GetAllUsers();
    }
}