using MongoDB.Driver;
using SP.DAL.Interfaces;
using SP.Roles.Models;

namespace SP.DAL
{
    public class RoleRepository : GenericRepository<Role>, IRoleRepository
    {
        public RoleRepository(IMongoDatabase database) : base(database, "roles")
        {
        }
    }

}
