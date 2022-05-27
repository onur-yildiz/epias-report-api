using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using SP.User.Service.Jwt;

namespace SP.User.Service.Middlewares
{
    public class AuthMiddleware
    {
        private readonly RequestDelegate _next;

        public AuthMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, IUserService userService, IJwtUtils jwtUtils)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            if (token != null)
            {
                var userId = jwtUtils.ValidateToken(token);
                if (userId != null)
                {
                    var account = userService.GetAccountById((ObjectId)userId);
                    if (account != null)
                    {
                        context.Items["IsTokenValid"] = true;
                        context.Items["Roles"] = account.Roles;
                        context.Items["IsActive"] = account.IsActive;
                    }
                }
            }

            await _next(context);
        }
    }
}
