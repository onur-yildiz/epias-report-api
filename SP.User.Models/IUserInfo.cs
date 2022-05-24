using MongoDB.Bson;

namespace SP.User.Models
{
    public interface IUserInfo
    {
        string Email { get; set; }
        ObjectId Id { get; set; }
        string Name { get; set; }
    }
}