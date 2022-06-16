using MongoDB.Bson;

namespace SP.Users.Models
{
    public interface IAccount : IUserBase<ObjectId>
    {
        string Password { get; set; }
        byte[] Salt { get; set; }
    }
}