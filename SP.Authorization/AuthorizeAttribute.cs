using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MongoDB.Bson;
using Serilog;
using SP.User.Models;

namespace SP.Authorization
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        public virtual string[] Roles { get; set; }

        public AuthorizeAttribute()
        {
            this.Roles = Array.Empty<string>();
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            // skip authorization if action is decorated with [AllowAnonymous] attribute
            var isAllowAnonymous = context.ActionDescriptor.EndpointMetadata.OfType<AllowAnonymousAttribute>().Any();
            if (isAllowAnonymous) return;

            // check if authorized by JwtMiddleware, account is active, and has required roles
            var isTokenValid = (bool?)context.HttpContext.Items["IsTokenValid"];
            if (isTokenValid != true) context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
            var accountRoles = (HashSet<string>?)context.HttpContext.Items["Roles"];
            var hasRoles = (accountRoles != null && Roles.Any()) && Roles.All(role => accountRoles.Contains(role));
            var isActive = (bool?)context.HttpContext.Items["IsActive"];
            if (isActive != true || hasRoles) context.Result = new JsonResult(new { message = "Forbidden" }) { StatusCode = StatusCodes.Status403Forbidden };

            // Logging
            var userId = (ObjectId?)context.HttpContext.Items["UserId"];
            var logger = (ILogger)context.HttpContext.RequestServices.GetService(typeof(ILogger))!;
            logger.Information("{path} {@requester}", context.HttpContext.Request.Path.Value, userId);
        }
    }
}
