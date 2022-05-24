using MongoDB.Bson;
using SP.User.Models;

namespace SP.User.Service.Jwt
{
    public interface IJwtUtils
    {
        public string GenerateToken(IUserInfo user);
        public ObjectId? ValidateToken(string token);
    }
}
