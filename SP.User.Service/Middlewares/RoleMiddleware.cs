using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using SP.User.Service.Jwt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.User.Service.Middlewares
{
    public class RoleMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly JwtSettings _jwtSettings;

        public RoleMiddleware(RequestDelegate next, IOptions<JwtSettings> jwtSettings)
        {
            _next = next;
            _jwtSettings = jwtSettings.Value;
        }

        public async Task Invoke(HttpContext context, IUserService userService, IJwtUtils jwtUtils)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            if (token != null)
            {
                var userId = jwtUtils.ValidateToken(token);
                if (userId != null)
                {
                    //context.Items["User"] = userService.GetById((ObjectId)userId);
                }
            }

            await _next(context);
        }
    }
}
