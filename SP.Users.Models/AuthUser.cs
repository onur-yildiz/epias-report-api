using System.Text.Json.Serialization;

namespace SP.Users.Models
{
    public class AuthUser : IAuthUser
    {
        public AuthUser(IAccount account, string token)
        {
            Email = account.Email;
            Id = account.Id.ToString();
            Name = account.Name;
            IsActive = account.IsActive;
            IsAdmin = account.IsAdmin;
            LanguageCode = account.LanguageCode;
            Roles = account.Roles;
            Token = token;
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
        [JsonPropertyName("token")]
        public string Token { get; set; }
    }
}
