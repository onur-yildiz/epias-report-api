using MongoDB.Bson;
using SP.User.Models;
using SP.User.Models.RequestParams;

namespace SP.User.Service
{
    public interface IUserService
    {
        bool IsAccountExisting(ObjectId id);
        void AssignRole(UpdateRoleRequestParams r);
        string Login(UserLoginRequestParams r);
        string Register(UserRegisterRequestParams r);
        void RemoveRole(UpdateRoleRequestParams r);
        void UpdateIsActive(UpdateIsActiveRequestParams r);
    }
}