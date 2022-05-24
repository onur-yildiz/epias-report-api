using System.Text.Json;

namespace SP.Reports.Service
{
    public class ReportsResponseUtils
    {
        public static async Task<T?> ExtractContent<T>(HttpResponseMessage response)
        {
            var data = await response.Content.ReadAsStreamAsync();
            return JsonSerializer.Deserialize<T>(data);
        }
    }
}
