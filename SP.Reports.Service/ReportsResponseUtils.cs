using SP.Reports.Models;
using System.Text;
using System.Text.Json;

namespace SP.Reports.Service
{
    public static class ReportsResponseUtils
    {
        public static async Task<TBody?> ExtractBody<TBody, TContainer>(HttpResponseMessage response) where TBody: class where TContainer: IResponseBase<TBody>
        {
            var data = await response.Content.ReadAsStreamAsync();
            return JsonSerializer.Deserialize<TContainer>(data)?.Body;
        }

        public static string GenerateQueryFromObject (object r)
        {
            var query = new StringBuilder("?");
            if (r != null)
            {
                var props = r.GetType().GetProperties();
                foreach (var prop in props)
                {
                    var key = Char.ToLower(prop.Name[0]) + prop.Name[1..];
                    var value = prop.GetValue(r, null);
                    query.Append($"{key}={value}&");
                }
            }
            else query.Clear();
            return query.ToString();
        }
    }
}
