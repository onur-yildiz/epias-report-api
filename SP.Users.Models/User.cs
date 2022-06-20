

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

        public string Email { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public bool IsAdmin { get; set; }
        public string LanguageCode { get; set; }
        public HashSet<string> Roles { get; set; }
        public HashSet<string> ApiKeys { get; set; }
    }
}
