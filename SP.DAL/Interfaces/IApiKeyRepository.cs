using MongoDB.Bson;
using SP.Users.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.DAL.Interfaces
{
    public interface IApiKeyRepository : IGenericRepository<ApiKey>
    {
    }
}
