using MongoDB.Bson;
using MongoDB.Driver;
using SP.DAL.Interfaces;
using SP.Users.Models;

namespace SP.DAL
{
    public class UserRepository : GenericRepository<Account>, IUserRepository
    {
        public UserRepository(IMongoDatabase database) : base(database, "users")
        {
        }

        public virtual Account? GetByEmail(string email)
        {
            return Collection.Find(u => u.Email == email).FirstOrDefault();
        }
    }
}
