namespace SP.Users.Models
{
    public interface IApiKey
    {
        string App { get; set; }
        string Key { get; set; }
    }
}