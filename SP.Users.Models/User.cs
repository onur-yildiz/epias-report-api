

using System.Text.Json.Serialization;

namespace SP.Users.Models
{
    public class User : IUserBase<string>
    {

        public User(string email, string id, string name, bool isActive, bool isAdmin, string languageCode, HashSet<string> roles, HashSet<string> apiKeys)
        {
            Email = email;
            Id = id;
            Name = name;
            IsActive = isActive;
            IsAdmin = isAdmin;
            LanguageCode = languageCode;
            Roles = roles;
            ApiKeys = apiKeys;
        }

        [JsonPropertyName("email")]
        public string Email { get; set; }
        [JsonPropertyName("id")]
        public string Id { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("isActive")]
        public bool IsActive { get; set; }
        [JsonPropertyName("isAdmin")]
        public bool IsAdmin { get; set; }
        [JsonPropertyName("languageCode")]
        public string LanguageCode { get; set; }
        [JsonPropertyName("roles")]
        public HashSet<string> Roles { get; set; }
        [JsonPropertyName("apiKeys")]
        public HashSet<string> ApiKeys { get; set; }
    }
}
