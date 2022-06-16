namespace SP.Users.Models
{
    public interface IUserBase<T>
    {
        string Email { get; set; }
        T Id { get; set; }
        string Name { get; set; }
        bool IsActive { get; set; }
        bool IsAdmin { get; set; }
        string LanguageCode { get; set; }
        HashSet<string> Roles { get; set; }
        HashSet<string> ApiKeys { get; set; }
    }
}
