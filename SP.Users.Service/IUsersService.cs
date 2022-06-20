using MongoDB.Bson;
using SP.Users.Models;
using SP.Users.Models.RequestParams;

namespace SP.Users.Service
{
    public interface IUsersService
    {
        IAccount? GetAccountById(ObjectId id);
        IAuthUser RefreshToken(string token);
        bool IsAccountExisting(string email);
        IAuthUser Login(IUserLoginRequestBody r);
        IAuthUser Register(IUserRegisterRequestBody r);
        void UpdateRoles(string userId, IUpdateAccountRolesRequestBody r);
        void UpdateIsActive(string userId, IUpdateAccountIsActiveRequestBody r);
        string CreateApiKey(string token);
        void DeleteApiKey(string apiKey, string token);
        public IEnumerable<IUserBase<string>> GetAllUsers();
    }
}