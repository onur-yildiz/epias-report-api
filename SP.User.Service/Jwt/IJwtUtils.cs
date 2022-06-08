using MongoDB.Bson;
using SP.User.Models;

namespace SP.User.Service.Jwt
{
    public interface IJwtUtils
    {
        public string GenerateToken(ObjectId userId);
        public ObjectId? ValidateToken(string token);
    }
}
