

using Microsoft.AspNetCore.Http;
using MongoDB.Driver;
using SP.Exceptions;
using SP.Reports.Models.ReportListing;
using SP.Reports.Service;

namespace SP.Middlewares
{
    public class ReportAuthMiddleware
    {
        private readonly RequestDelegate _next;

        public ReportAuthMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, IReportsService reportsService)
        {
            var pathSections = context.Request.Path.Value[1..].Split('/');
            if (pathSections.Length == 2)
            {
                var report = reportsService.GetReportByKey(pathSections[1]);
                var userRoles = (HashSet<string>?)context.Items["Roles"];
                var isUserActive = (bool?)context.Items["IsActive"];
                var isAdmin = (bool?)context.Items["IsAdmin"];

                var noRoles = report.Roles.Any() && (userRoles == null || !report.Roles.Any(r => userRoles.Contains(r)));
                if (!report.IsActive || isAdmin != true && noRoles || isUserActive != true && report.Roles.Any())
                    throw HttpResponseException.Forbidden();
            }
            await _next(context);
        }
    }
}