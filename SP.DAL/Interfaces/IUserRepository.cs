using MongoDB.Bson;
using MongoDB.Driver;
using SP.Users.Models;

namespace SP.DAL.Interfaces
{
    public interface IUserRepository : IGenericRepository<Account>
    {
        Account? GetByEmail(string email);
    }
}