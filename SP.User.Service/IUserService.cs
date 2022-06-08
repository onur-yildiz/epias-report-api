using MongoDB.Bson;
using SP.User.Models;
using SP.User.Models.RequestParams;

namespace SP.User.Service
{
    public interface IUserService
    {
        Account? GetAccountById(ObjectId id);
        AuthUserData GetUserDataByToken(string token);
        bool IsAccountExisting(string email);
        void AssignRole(UpdateRoleRequestParams r);
        AuthUserData Login(UserLoginRequestParams r);
        AuthUserData Register(UserRegisterRequestParams r);
        void RemoveRole(UpdateRoleRequestParams r);
        void UpdateIsActive(UpdateIsActiveRequestParams r);
    }
}