

using Microsoft.AspNetCore.Http;
using MongoDB.Driver;
using SP.DAL.Interfaces;
using SP.Exceptions;

namespace SP.Middlewares
{
    public class ExtraReportsAuthMiddleware
    {
        private readonly RequestDelegate _next;

        public ExtraReportsAuthMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, IApiKeyRepository apiKeyRepository)
        {
            var apiKey = context.Request.Headers["x-api-key"].FirstOrDefault();

            if (apiKey == null || !apiKeyRepository.Get(a => a.Key == apiKey).Any())
                throw HttpResponseException.Forbidden("API key does not exist or none provided.");

            await _next(context);
        }
    }
}