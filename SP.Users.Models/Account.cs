using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SP.Users.Models
{
    public class Account : IAccount
    {
        public static explicit operator User(Account acc)
        {
            return new User()
            {
                Id = acc.Id.ToString(),
                Name = acc.Name,
                Roles = acc.Roles,
                Email = acc.Email,
                IsActive = acc.IsActive,
                IsAdmin = acc.IsAdmin,
                LanguageCode = acc.LanguageCode,
            };
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
