using MongoDB.Bson;
using SP.EpiasReports.Models;

namespace SP.Users.Models
{
    public interface IAccount: IMongoDbEntity, IUserBase
    {
        string Password { get; set; }
        byte[] Salt { get; set; }
    }
}