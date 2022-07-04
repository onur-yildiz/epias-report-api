using MongoDB.Driver;
using SP.DAL.Interfaces;
using SP.Reports.Models.ReportListing;

namespace SP.DAL
{
    public class ReportRepository : GenericRepository<Report>, IReportRepository
    {
        public ReportRepository(IMongoDatabase database) : base(database, "reports")
        {
        }
    }
}
