

using Microsoft.AspNetCore.Http;
using MongoDB.Driver;
using SP.Exceptions;
using SP.Reports.Models.ReportListing;

namespace SP.Middlewares
{
    public class ReportAuthMiddleware
    {
        private readonly RequestDelegate _next;

        public ReportAuthMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, IMongoClient mongo)
        {
            var pathSections = context.Request.Path.Value[1..].Split('/');
            if (pathSections.Length == 2)
            {
                var reports = mongo.GetDatabase("cluster0").GetCollection<Report>("reports");
                var report = reports.Find(r => r.Key == pathSections[1]).FirstOrDefault();

                if (report == null)
                    throw HttpResponseException.NotExists("Report");

                var userRoles = (HashSet<string>?)context.Items["Roles"];
                var isUserActive = (bool?)context.Items["IsActive"];
                var isAdmin = (bool?)context.Items["IsAdmin"];

                var noRoles = report.Roles.Count > 0 && (userRoles == null || !report.Roles.Any(r => userRoles.Contains(r)));
                if (!report.IsActive || isAdmin != true && noRoles || isUserActive != true && report.Roles.Count > 0)
                    throw HttpResponseException.Forbidden();
            }
            await _next(context);
        }
    }
}