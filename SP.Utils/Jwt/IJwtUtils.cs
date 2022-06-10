using MongoDB.Bson;

namespace SP.Utils.Jwt
{
    public interface IJwtUtils
    {
        public string GenerateToken(ObjectId userId);
        public ObjectId? ValidateToken(string token);
    }
}
