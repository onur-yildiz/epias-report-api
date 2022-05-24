using System.Text.Json;

namespace EpiasReportHttp
{
    public class HttpHelper
    {
        static readonly HttpClient _client = new();

        static async Task<T?> ExtractBody<T>(HttpResponseMessage response)
        {
            var data = await response.Content.ReadAsStreamAsync();
            return JsonSerializer.Deserialize<T>(data);
        }

        static public async Task<T?> Fetch<T>(string url)
        {
            var response = await _client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            return await ExtractBody<T>(response);
        }
    }
}
