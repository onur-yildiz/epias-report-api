namespace SP.User.Models
{
    public interface IUserSettings
    {
        bool IsActive { get; set; }
        string LanguageCode { get; set; }
        string[] Roles { get; set; }
    }
}