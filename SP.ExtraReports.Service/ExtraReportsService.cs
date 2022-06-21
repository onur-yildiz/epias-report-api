using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using SP.Exceptions;
using SP.ExtraReports.Models;
using SP.Reports.Models.Api;
using SP.Reports.Models.RealTimeConsumption;
using SP.Reports.Models.RealTimeGeneration;
using SP.Reports.Models.RequestParams;
using SP.Reports.Service;
using System.Globalization;
using System.Text.Json;

namespace SP.ExtraReports.Service
{
    public class ExtraReportsService : IExtraReportsService
    {
        readonly IHttpClientFactory _httpClientFactory;
        readonly IMongoCollection<HourlyGenerationsByType> _hourlyGenerations;
        readonly IMongoCollection<ConsumptionStatistics> _consumptionStatistics;
        readonly IReportsService _reportsServiceRepository;
        readonly IApiPaths _apiPaths;

        public ExtraReportsService(IHttpClientFactory httpClientFactory, IMongoClient client, IReportsService reportsServiceRepository, IOptions<ApiPaths> options)
        {
            _httpClientFactory = httpClientFactory;
            _hourlyGenerations = client.GetDatabase("cluster0").GetCollection<HourlyGenerationsByType>("hourly-generations");
            _consumptionStatistics = client.GetDatabase("cluster0").GetCollection<ConsumptionStatistics>("consumption-statistics");
            _reportsServiceRepository = reportsServiceRepository;
            _apiPaths = options.Value;
        }

        public async Task<IEnumerable<IHourlyGenerationsByType>> GetHourlyGenerations(IDateIntervalRequestParams r)
        {
            var startDate = DateTime.Parse(r.StartDate, CultureInfo.GetCultureInfo("tr-TR"));
            var endDate = DateTime.Parse(r.EndDate, CultureInfo.GetCultureInfo("tr-TR"));

            var hourlyGenerationsByType = _hourlyGenerations.Find(h => h.Date >= startDate && h.Date < endDate.AddDays(1)).ToList();
            if (hourlyGenerationsByType.Count != ((endDate - startDate).Days + 1) * 24)
            {
                var res = await _reportsServiceRepository.GetData<HourlyGenerationContainer, HourlyGenerationResponse>(r, _apiPaths.RealTimeGeneration);

                if (res?.HourlyGenerations == null)
                    throw new HttpResponseException(StatusCodes.Status502BadGateway, new { message = "Could not get the data." });

                hourlyGenerationsByType = new List<HourlyGenerationsByType>();
                foreach (var h in res.HourlyGenerations)
                {
                    var hByType = new HourlyGenerationsByType(h);
                    hourlyGenerationsByType.Add(hByType);
                    _hourlyGenerations.UpdateOne(h => h.Date == hByType.Date,
                        Builders<HourlyGenerationsByType>.Update
                        .SetOnInsert("importExport", hByType.ImportExport)
                        .SetOnInsert("renewable", hByType.Renewable)
                        .SetOnInsert("renewableTotal", hByType.RenewableTotal)
                        .SetOnInsert("nonRenewable", hByType.NonRenewable)
                        .SetOnInsert("nonRenewableTotal", hByType.NonRenewableTotal)
                        .SetOnInsert("total", hByType.Total)
                        .SetOnInsert("date", hByType.Date)
                    , new UpdateOptions { IsUpsert = true });
                }
            }

            return hourlyGenerationsByType;
        }

        public async Task<IConsumptionStatistics> GetConsumptionStatistics(string dateString)
        {
            var date = DateTime.Parse(dateString, CultureInfo.GetCultureInfo("tr-TR"));
            var period = new Period(new DateTime(date.Year, date.Month, 1), new DateTime(date.Year, date.Month, DateTime.DaysInMonth(date.Year, date.Month), 23, 59, 59));

            var consumptionStatistics = _consumptionStatistics.Find(s => s.Period == period).FirstOrDefault();
            // Get the data from actual source if not processed before
            if (consumptionStatistics == null)
            {
                var httpClient = _httpClientFactory.CreateClient("EpiasAPI");
                var response = await httpClient.GetAsync($"consumption/real-time-consumption?startDate={period.Start:yyyy-MM-dd}&endDate={period.End:yyyy-MM-dd}");
                var data = await response.Content.ReadAsStreamAsync();
                var consumptions = JsonSerializer.Deserialize<HourlyConsumptionResponse>(data)?.Body?.HourlyConsumptions;

                if (consumptions == null)
                    throw new HttpResponseException(StatusCodes.Status502BadGateway, new { message = "Could not get the data." });

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

                _consumptionStatistics.InsertOne(consumptionStatistics);
            }

            return consumptionStatistics;
        }

    }
}