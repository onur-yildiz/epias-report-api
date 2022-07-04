using MongoDB.Driver;
using SP.DAL.Interfaces;
using SP.ExtraReports.Models;

namespace SP.DAL
{
    public class HourlyGenerationsRepository : GenericRepository<HourlyGenerationsEntity>, IHourlyGenerationsRepository
    {
        public HourlyGenerationsRepository(IMongoDatabase database) : base(database, "hourly-generations")
        {
        }
    }
}
