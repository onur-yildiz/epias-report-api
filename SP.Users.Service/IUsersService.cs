using MongoDB.Bson;
using SP.Users.Models;
using SP.Users.Models.RequestParams;

namespace SP.Users.Service
{
    public interface IUsersService
    {
        AuthUser RefreshToken(string token);
        AuthUser Login(IUserLoginRequestBody r);
        AuthUser Register(IUserRegisterRequestBody r);
        void UpdateRoles(string userId, IUpdateAccountRolesRequestBody r);
        void UpdateIsActive(string userId, IUpdateAccountIsActiveRequestBody r);
        IEnumerable<ApiKey> GetApiKeys(string targetUserId);
        string CreateApiKey(string targetUserId);
        void DeleteApiKey(string apiKey, string targetUserId);
        public IEnumerable<User> GetAllUsers();
    }
}