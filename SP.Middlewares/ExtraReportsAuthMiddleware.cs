

using Microsoft.AspNetCore.Http;
using MongoDB.Driver;
using SP.Reports.Models.ReportListing;
using SP.Roles.Service;
using SP.Users.Models;

namespace SP.Middlewares
{
    public class ExtraReportsAuthMiddleware
    {
        private readonly RequestDelegate _next;

        public ExtraReportsAuthMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, IMongoClient mongo)
        {
            var apiKey = context.Request.Headers["x-api-key"].FirstOrDefault();

            if (apiKey == null || !mongo.GetDatabase("cluster0").GetCollection<ApiKey>("api-keys").Find(a => a.Key == apiKey).Any())
            {
                context.Response.StatusCode = StatusCodes.Status403Forbidden;
                return;
            }

            await _next(context);
        }
    }
}