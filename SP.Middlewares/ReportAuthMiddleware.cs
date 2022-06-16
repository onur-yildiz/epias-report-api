

using Microsoft.AspNetCore.Http;
using MongoDB.Driver;
using SP.Reports.Models.ReportListing;
using SP.Roles.Service;

namespace SP.Middlewares
{
    public class ReportAuthMiddleware
    {
        private readonly RequestDelegate _next;

        public ReportAuthMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, IRolesService settingsService, IMongoClient mongo)
        {
            var pathSections = context.Request.Path.Value[1..].Split('/');
            if (pathSections.Length == 2)
            {
                var reports = mongo.GetDatabase("cluster0").GetCollection<Report>("reports");
                var report = reports.Find(r => r.Key == pathSections[1]).FirstOrDefault();

                if (report == null)
                {
                    context.Response.StatusCode = StatusCodes.Status404NotFound;
                    return;
                }

                var userRoles = (HashSet<string>?)context.Items["Roles"];
                var isUserActive = (bool?)context.Items["IsActive"];
                var isAdmin = (bool?)context.Items["IsAdmin"];

                var noRoles = report.Roles.Count > 0 && (userRoles == null || !report.Roles.Any(r => userRoles.Contains(r)));
                if (!report.IsActive || isAdmin != true && noRoles || isUserActive != true && report.Roles.Count > 0)
                {
                    context.Response.StatusCode = StatusCodes.Status403Forbidden;
                    return;
                }
            }
            await _next(context);
        }
    }
}