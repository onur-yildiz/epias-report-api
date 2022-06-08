using MongoDB.Bson;

namespace SP.User.Models
{
    public interface IAccount : IUserSettings
    {
        string Email { get; set; }
        ObjectId Id { get; set; }
        string Name { get; set; }
        string Password { get; set; }
        byte[] Salt { get; set; }
    }
}