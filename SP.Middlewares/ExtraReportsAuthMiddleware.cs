

using Microsoft.AspNetCore.Http;
using MongoDB.Driver;
using SP.Exceptions;
using SP.Users.Service;

namespace SP.Middlewares
{
    public class ExtraReportsAuthMiddleware
    {
        private readonly RequestDelegate _next;

        public ExtraReportsAuthMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, IUsersService usersService)
        {
            var apiKey = context.Request.Headers["x-api-key"].FirstOrDefault();

            if (apiKey == null || !usersService.CheckIfApiKeyExists(apiKey))
                throw HttpResponseException.Forbidden("API key does not exist or none provided.");

            await _next(context);
        }
    }
}