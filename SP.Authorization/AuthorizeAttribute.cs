using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace SP.Authorization
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        private string[] roles;

        public AuthorizeAttribute()
        {
            this.roles = Array.Empty<string>();
        }

        public virtual string[] Roles
        {
            get { return roles; }
            set { roles = value; }
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            // skip authorization if action is decorated with [AllowAnonymous] attribute
            var isAllowAnonymous = context.ActionDescriptor.EndpointMetadata.OfType<AllowAnonymousAttribute>().Any();
            if (isAllowAnonymous) return;

            // check if authorized by JwtMiddleware, account is active, and has required roles
            var accountRoles = (HashSet<string>)context.HttpContext.Items["Roles"];
            var hasRoles = Roles.All(role => accountRoles.Contains(role));
            var isActive = (bool)context.HttpContext.Items["IsActive"];
            var isTokenValid = (bool)context.HttpContext.Items["IsTokenValid"];
            if (!isTokenValid) context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
            else if (!isActive || !hasRoles) context.Result = new JsonResult(new { message = "Forbidden" }) { StatusCode = StatusCodes.Status403Forbidden };
        }
    }
}
