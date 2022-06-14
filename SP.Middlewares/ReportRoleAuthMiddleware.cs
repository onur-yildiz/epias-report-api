

using Microsoft.AspNetCore.Http;
using MongoDB.Driver;
using SP.Reports.Models.ReportListing;
using SP.Settings.Service;

namespace SP.Middlewares
{
    public class ReportRoleAuthMiddleware
    {
        private readonly RequestDelegate _next;

        public ReportRoleAuthMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, ISettingsService settingsService, IMongoClient mongo)
        {
            var pathSections = context.Request.Path.Value[1..].Split('/');
            if (pathSections.Length == 2 && pathSections[0] == "reports")
            {
                var userRoles = (HashSet<string>?)context.Items["Roles"];
                var isUserActive = (bool?)context.Items["IsActive"];
                var isAdmin = (bool?)context.Items["IsAdmin"];


                var reports = mongo.GetDatabase("cluster0").GetCollection<Report>("reports");
                var report = reports.Find(r => r.Endpoint == pathSections[1]).FirstOrDefault();

                if (report != null)
                {
                    var noRoles =  report.Roles.Count > 0 && (userRoles == null || !report.Roles.Any(r => userRoles.Contains(r)));
                    if (!report.IsActive || isAdmin != true && noRoles || isUserActive != true && report.Roles.Count > 0)
                    {
                        context.Response.StatusCode = StatusCodes.Status403Forbidden;
                        return;
                    }
                }
            }

            await _next(context);
        }
    }
}