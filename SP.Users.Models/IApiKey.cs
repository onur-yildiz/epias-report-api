using MongoDB.Bson;

namespace SP.Users.Models
{
    public interface IApiKey
    {
        ObjectId UserId { get; set; }
        string App { get; set; }
        string Key { get; set; }
    }
}