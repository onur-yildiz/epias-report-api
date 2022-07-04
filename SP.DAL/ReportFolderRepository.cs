using MongoDB.Driver;
using SP.DAL.Interfaces;
using SP.Reports.Models.ReportListing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.DAL
{
    public class ReportFolderRepository : GenericRepository<ReportFolder>, IReportFolderRepository
    {
        public ReportFolderRepository(IMongoDatabase database) : base(database, "report-folders")
        {
        }
    }
}
