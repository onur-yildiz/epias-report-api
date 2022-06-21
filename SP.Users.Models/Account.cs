using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SP.Users.Models
{
    public class Account : IAccount
    {
        public Account(ObjectId id, string name, string email, string password, HashSet<string> roles, HashSet<string> apiKeys, bool isActive, bool isAdmin, string languageCode, byte[] salt)
        {
            Id = id;
            Name = name;
            Email = email;
            Password = password;
            Roles = roles;
            IsActive = isActive;
            IsAdmin = isAdmin;
            LanguageCode = languageCode;
            Salt = salt;
        }

        public static explicit operator User(Account acc)
        {
            return new User
            (
                email: acc.Email,
                id: acc.Id.ToString(),
                name: acc.Name,
                roles: acc.Roles,
                isActive: acc.IsActive,
                isAdmin: acc.IsAdmin,
                languageCode: acc.LanguageCode,
                apiKeys: new HashSet<string>()
            );
        }

        [BsonId]
        public ObjectId Id { get; set; }

        [BsonRequired]
        [BsonElement("name")]
        public string Name { get; set; }

        [BsonRequired]
        [BsonElement("email")]
        public string Email { get; set; }

        [BsonRequired]
        [BsonElement("password")]
        public string Password { get; set; }

        [BsonRequired]
        [BsonElement("roles")]
        public HashSet<string> Roles { get; set; }

        [BsonRequired]
        [BsonElement("isActive")]
        public bool IsActive { get; set; }

        [BsonRequired]
        [BsonElement("isAdmin")]
        public bool IsAdmin { get; set; }

        [BsonRequired]
        [BsonElement("languageCode")]
        public string LanguageCode { get; set; }

        [BsonRequired]
        [BsonElement("salt")]
        public Byte[] Salt { get; set; }
    }
}
