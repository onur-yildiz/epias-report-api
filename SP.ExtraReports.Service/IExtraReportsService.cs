using SP.ExtraReports.Models;
using SP.Reports.Models.RequestParams;

namespace SP.ExtraReports.Service
{
    public interface IExtraReportsService
    {
        Task<ConsumptionStatistics> GetConsumptionStatistics(string dateString);
        Task<IEnumerable<HourlyGenerationsByType>> GetHourlyGenerations(IDateIntervalRequestParams r);
    }
}