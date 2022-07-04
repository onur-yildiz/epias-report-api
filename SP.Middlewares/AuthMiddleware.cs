

using Microsoft.AspNetCore.Http;
using MongoDB.Bson;
using SP.DAL.Interfaces;
using SP.Exceptions;
using SP.Utils.Jwt;

namespace SP.Middlewares
{
    public class AuthMiddleware
    {
        private readonly RequestDelegate _next;

        public AuthMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, IUserRepository userRepository, IJwtUtils jwtUtils)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            if (token != null)
            {
                var userId = jwtUtils.ValidateToken(token);
                if (userId != null)
                {
                    var account = userRepository.GetById((ObjectId)userId);
                    if (account == null)
                        throw HttpResponseException.InvalidToken("Account does not exist");
                    
                    context.Items["IsTokenValid"] = true;
                    context.Items["UserId"] = account.Id;
                    context.Items["Roles"] = account.Roles;
                    context.Items["IsActive"] = account.IsActive;
                    context.Items["IsAdmin"] = account.IsAdmin;
                }
            }

            await _next(context);
        }
    }
}
