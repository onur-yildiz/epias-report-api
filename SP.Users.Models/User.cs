

using System.Text.Json.Serialization;

namespace SP.Users.Models
{
    public class User : IUserBase<string>
    {

        public User(string email, string id, string name, bool isActive, bool isAdmin, string languageCode, HashSet<string> roles)
        {
            Email = email;
            Id = id;
            Name = name;
            IsActive = isActive;
            IsAdmin = isAdmin;
            LanguageCode = languageCode;
            Roles = roles;
        }

        /// <summary>
        /// Account E-mail
        /// </summary>
        [JsonPropertyName("email")]
        public string Email { get; set; }

        /// <summary>
        /// User ID
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; }

        /// <summary>
        /// Username
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; }

        /// <summary>
        /// Account's active state
        /// </summary>
        [JsonPropertyName("isActive")]
        public bool IsActive { get; set; }

        /// <summary>
        /// Account's admin state
        /// </summary>
        [JsonPropertyName("isAdmin")]
        public bool IsAdmin { get; set; }

        /// <summary>
        /// User's preferred language
        /// </summary>
        [JsonPropertyName("languageCode")]
        public string LanguageCode { get; set; }

        /// <summary>
        /// Account roles
        /// </summary>
        [JsonPropertyName("roles")]
        public HashSet<string> Roles { get; set; }
    }
}
