using SP.ExtraReports.Models;
using SP.Reports.Models.RequestParams;

namespace SP.ExtraReports.Service
{
    public interface IExtraReportsService
    {
        Task<ConsumptionStatistics> GetConsumptionStatistics(string dateString);
        Task<IEnumerable<HourlyGenerations>> GetHourlyGenerations(IDateIntervalRequestParams r);
    }
}