using SP.ExtraReports.Models;
using SP.Reports.Models.RequestParams;

namespace SP.ExtraReports.Service
{
    public interface IExtraReportsService
    {
        Task<IConsumptionStatistics> GetConsumptionStatistics(string dateString);
        Task<IEnumerable<HourlyGenerationsByType>> GetHourlyGenerations(DateIntervalRequestParams r);
    }
}