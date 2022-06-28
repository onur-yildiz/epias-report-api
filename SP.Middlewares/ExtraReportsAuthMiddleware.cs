

using Microsoft.AspNetCore.Http;
using MongoDB.Driver;
using SP.Exceptions;
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
                throw HttpResponseException.Forbidden("API key does not exist or none provided.");

            await _next(context);
        }
    }
}