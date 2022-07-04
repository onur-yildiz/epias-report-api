using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using SP.DAL.Interfaces;
using SP.Exceptions;
using SP.ExtraReports.Models;
using SP.Reports.Models.Api;
using SP.Reports.Models.RealTimeConsumption;
using SP.Reports.Models.RealTimeGeneration;
using SP.Reports.Models.RequestParams;
using SP.Reports.Service;
using System.Text.Json;

namespace SP.ExtraReports.Service
{
    public class ExtraReportsService : IExtraReportsService
    {
        readonly IHttpClientFactory _httpClientFactory;
        readonly IHourlyGenerationsRepository _hourlyGenerationsRepository;
        readonly IConsumptionStatisticsRepository _consumptionStatisticsRepository;
        readonly IReportsService _reportsService;
        readonly IApiPaths _apiPaths;

        public ExtraReportsService(IHttpClientFactory httpClientFactory,
            IHourlyGenerationsRepository hourlyGenerationsRepository,
            IConsumptionStatisticsRepository consumptionStatisticsRepository,
            IReportsService reportsService,
            IOptions<ApiPaths> options)
        {
            _httpClientFactory = httpClientFactory;
            _hourlyGenerationsRepository = hourlyGenerationsRepository;
            _consumptionStatisticsRepository = consumptionStatisticsRepository;
            _reportsService = reportsService;
            _apiPaths = options.Value;
        }

        public async Task<IEnumerable<HourlyGenerations>> GetHourlyGenerations(IDateIntervalRequestParams r)
        {
            var startDate = DateTime.ParseExact(r.StartDate + " +3", "yyyy-MM-dd z", default).ToUniversalTime(); // utc+3
            var endDate = DateTime.ParseExact(r.EndDate + " +3", "yyyy-MM-dd z", default).AddDays(1).ToUniversalTime(); // utc+3

            if (startDate > endDate)
                throw new HttpResponseException(StatusCodes.Status400BadRequest, "Start date cannot be after the end date");
            if (endDate > DateTime.Today.AddDays(1))
                throw new HttpResponseException(StatusCodes.Status400BadRequest, "End date cannot be in the future");

            var hourlyGenerationsByType = _hourlyGenerationsRepository.Get(h => h.Date >= startDate && h.Date < endDate).ToList();
            if (hourlyGenerationsByType.Count != (endDate - startDate).Days * 24)
            {
                var res = await _reportsService.GetData<HourlyGenerationContainer, HourlyGenerationResponse>(r, _apiPaths.RealTimeGeneration);

                if (res?.HourlyGenerations == null)
                    throw HttpResponseException.DatabaseError();

                hourlyGenerationsByType = new List<HourlyGenerations>();
                foreach (var h in res.HourlyGenerations)
                {
                    var hByType = new HourlyGenerations(h);
                    hourlyGenerationsByType.Add(hByType);
                    _hourlyGenerationsRepository.ReplaceOne_Upsert(h => h.Date == hByType.Date, hByType);
                }
            }

            return hourlyGenerationsByType;
        }

        public async Task<ConsumptionStatistics> GetConsumptionStatistics(string dateString)
        {
            var date = DateTime.Parse(dateString);
            var startNoTZ = new DateTime(date.Year, date.Month, 1);
            var endNoTZ = new DateTime(date.Year, date.Month, DateTime.DaysInMonth(date.Year, date.Month));
            var period = new Period(
                DateTime.ParseExact($"{startNoTZ:yyyy-MM-dd} +3", $"yyyy-MM-dd z", default), // utc+3
                DateTime.ParseExact($"{endNoTZ:yyyy-MM-dd} +3", $"yyyy-MM-dd z", default) // utc+3
                    .AddDays(1) // add 1 day to include last day
                    .AddHours(-1) //decrement 1 hour to exclude next month's first hour
            );

            var consumptionStatistics = _consumptionStatisticsRepository.Get(s => s.Period == period).FirstOrDefault();
            // Get the data from actual source if not processed before
            if (consumptionStatistics == null)
            {
                var httpClient = _httpClientFactory.CreateClient("EpiasAPI");
                var response = await httpClient.GetAsync($"consumption/real-time-consumption?startDate={startNoTZ:yyyy-MM-dd}&endDate={endNoTZ:yyyy-MM-dd}");
                var data = await response.Content.ReadAsStreamAsync();
                var consumptions = JsonSerializer.Deserialize<HourlyConsumptionResponse>(data)?.Body?.HourlyConsumptions;

                if (consumptions == null)
                    throw new HttpResponseException(StatusCodes.Status502BadGateway, "Could not get the data.");

                var dailyStats = from c in consumptions
                                 group c by c.Date != null ? DateTime.Parse(c.Date).Day : -1
                                 into daily
                                 select (
                                     daily.First().Date,
                                     Max: daily.MaxBy(d => d.Consumption),
                                     Sum: daily.Aggregate(0.0, (total, curr) => total + curr.Consumption ?? 0)
                                    );

                var mostConsumedHours = new MostConsumedPeriod[dailyStats.Count()];
                var dailyConsumptions = new MostConsumedPeriod[dailyStats.Count()];
                foreach (var (Date, Max, Sum, index) in dailyStats.Select((s, i) => (s.Date, s.Max, s.Sum, i)))
                {
                    mostConsumedHours[index] = new MostConsumedPeriod(
                            Max.Date ?? "",
                            Max.Consumption ?? -1,
                            Max.Consumption / Sum ?? -1
                        );
                    dailyConsumptions[index] = new MostConsumedPeriod(
                            Date,
                            Sum,
                            Sum / dailyStats.Aggregate(0.0, (total, curr) => total + curr.Sum)
                        );
                }

                consumptionStatistics = new ConsumptionStatistics(
                    period,
                    mostConsumedHours,
                    mostConsumedDays: dailyConsumptions.OrderByDescending(d => d.Consumption).Take(5)
                );

                _consumptionStatisticsRepository.AddOne(consumptionStatistics);
            }

            return consumptionStatistics;
        }

    }
}