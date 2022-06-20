

namespace SP.Users.Models
{
    public class User : IUserBase<string>
    {
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
