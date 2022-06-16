﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MongoDB.Bson;
using Serilog;

namespace SP.Authorization
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        public virtual string[] Roles { get; set; }
        public virtual bool AdminRestricted { get; set; }

        public AuthorizeAttribute()
        {
            this.Roles = Array.Empty<string>();
            this.AdminRestricted = false;
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
            var hasRoles = !Roles.Any() || (accountRoles != null && Roles.All(role => accountRoles.Contains(role)));
            var isActive = (bool?)context.HttpContext.Items["IsActive"];
            var isAdmin = (bool?)context.HttpContext.Items["IsAdmin"];
            if (isActive != true || (AdminRestricted && isAdmin != true) || (!hasRoles && isAdmin != true)) context.Result = new JsonResult(new { message = "Forbidden" }) { StatusCode = StatusCodes.Status403Forbidden };

            // Logging
            var userId = (ObjectId?)context.HttpContext.Items["UserId"];
            var logger = (ILogger)context.HttpContext.RequestServices.GetService(typeof(ILogger))!;
            logger.Information("{path} {@requester}", context.HttpContext.Request.Path.Value, userId);
        }
    }
}
