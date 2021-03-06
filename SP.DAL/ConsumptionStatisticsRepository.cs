using MongoDB.Driver;
using SP.DAL.Interfaces;
using SP.ExtraReports.Models;

namespace SP.DAL
{
    public class ConsumptionStatisticsRepository: GenericRepository<ConsumptionStatistics>, IConsumptionStatisticsRepository
    {
        public ConsumptionStatisticsRepository(IMongoDatabase database) : base(database, "consumption-statistics")
        {
        }
    }
}
