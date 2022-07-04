using MongoDB.Driver;
using SP.Reports.Models.ReportListing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.DAL.Interfaces
{
    public interface IReportRepository : IGenericRepository<Report>
    {
    }
}
