using Microsoft.AspNetCore.Diagnostics;
using MongoDB.Driver;
using Serilog;
using SP.AppConfig.Service;
using SP.DAL;
using SP.DAL.Interfaces;
using SP.EpiasReports.Models;
using SP.EpiasReports.Swagger;
using SP.Exceptions;
using SP.ExtraReports.Service;
using SP.Middlewares;
using SP.Reports.Models.Api;
using SP.Reports.Service;
using SP.Roles.Service;
using SP.Users.Service;
using SP.Utils.Cryptography;
using SP.Utils.Jwt;
using System.Reflection;

var root = Directory.GetCurrentDirectory();

#region .env
var dotenv = Path.Combine(root, ".env");
if (File.Exists(dotenv))
{
    foreach (var line in File.ReadAllLines(dotenv))
    {
        var parts = line.Split("=", StringSplitOptions.RemoveEmptyEntries);
        if (parts.Length != 2) continue;
        Environment.SetEnvironmentVariable(parts[0], parts[1]);
    }
}
#endregion

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

#region Swagger Config
builder.Services.AddSwaggerGen(c =>
{
    c.OperationFilter<SwaggerHeaderFilter>();
    var currentAssembly = Assembly.GetExecutingAssembly();
    var xmlDocs = currentAssembly.GetReferencedAssemblies()
    .Union(new AssemblyName[] { currentAssembly.GetName() })
    .Select(a => Path.Combine(Path.GetDirectoryName(currentAssembly.Location) ?? "", $"{a.Name}.xml"))
    .Where(f => File.Exists(f)).ToArray();

    Array.ForEach(xmlDocs, (d) =>
    {
        c.IncludeXmlComments(d);
    });
});
#endregion

#region HttpClients
builder.Services.AddHttpClient("EpiasAPI", httpClient =>
{
    httpClient.BaseAddress = new Uri(Environment.GetEnvironmentVariable("EPIAS_API_BASE_URL") ?? builder.Configuration.GetValue<string>("Api:BaseUrl"));
});
#endregion

#region DI

    #region Singletons
    var mongoDbConnString = builder.Configuration.GetValue<string>("MongoDbConnectionString");
    builder.Services.AddSingleton(_ =>
    {
        var mongoClient = new MongoClient(mongoDbConnString);
        var loggingDb = mongoClient.GetDatabase("cluster0");
        var collectionName = builder.Environment.IsDevelopment() ? "logs-development" : "logs";
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.MongoDB(loggingDb, collectionName: collectionName, period: TimeSpan.FromSeconds(1))
            .CreateLogger();

        Serilog.Debugging.SelfLog.Enable(output => Console.WriteLine(output));
        Log.Information("Starting up");
        return Log.Logger;
    });
    builder.Services.AddSingleton(_ => new MongoClient(mongoDbConnString).GetDatabase(Environment.GetEnvironmentVariable("DB_NAME")));
    #endregion

    #region DB Repositories
    builder.Services.AddTransient<IUserRepository, UserRepository>();
    builder.Services.AddTransient<IApiKeyRepository, ApiKeyRepository>();
    builder.Services.AddTransient<IRoleRepository, RoleRepository>();
    builder.Services.AddTransient<IReportRepository, ReportRepository>();
    builder.Services.AddTransient<IReportFolderRepository, ReportFolderRepository>();
    builder.Services.AddTransient<IHourlyGenerationsRepository, HourlyGenerationsRepository>();
    builder.Services.AddTransient<IConsumptionStatisticsRepository, ConsumptionStatisticsRepository>();
    #endregion

    #region Services
    builder.Services.AddTransient<IReportsService, ReportsService>();
    builder.Services.AddTransient<IExtraReportsService, ExtraReportsService>();
    builder.Services.AddTransient<IUsersService, UsersService>();
    builder.Services.AddTransient<IRolesService, RolesService>();
    builder.Services.AddTransient<IAppConfigService, AppConfigService>();
    #endregion

    #region Utils
    builder.Services.AddTransient<IJwtUtils, JwtUtils>();
    builder.Services.AddTransient<ICryptographyUtils, CryptographyUtils>();
    #endregion

#endregion

#region Configure Options
builder.Services.Configure<ApiPaths>(builder.Configuration.GetSection("Api").GetSection("Paths"));
builder.Services.AddOptions();
#endregion

var app = builder.Build();

#region CORS
app.UseCors(builder =>
    builder.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader()
);
#endregion

#region ExceptionHandler
if (app.Environment.IsDevelopment()) app.UseExceptionHandler(builder =>
{
    builder.Run(async context =>
    {
        var exceptionHandlerFeature = context.Features.Get<IExceptionHandlerFeature>();
        var logger = (Serilog.ILogger)context.RequestServices.GetService(typeof(Serilog.ILogger))!;

        logger.Error("{@error}", exceptionHandlerFeature?.Error);

        var statusCode = exceptionHandlerFeature?.Error is HttpResponseException error ? error.ResponseCode : 500;
        context.Response.StatusCode = statusCode;
        await context.Response.WriteAsJsonAsync(
            new ApiResponse<string>(
                statusCode,
                exceptionHandlerFeature?.Error.Message ?? "",
                exceptionHandlerFeature?.Error.ToString()
            )
        );
    });
});
else app.UseExceptionHandler(builder =>
{
    builder.Run(async context =>
    {
        var exceptionHandlerFeature = context.Features.Get<IExceptionHandlerFeature>();
        var logger = (Serilog.ILogger)context.RequestServices.GetService(typeof(Serilog.ILogger))!;

        logger.Error("{@error}", exceptionHandlerFeature?.Error);

        var statusCode = exceptionHandlerFeature?.Error is HttpResponseException error ? error.ResponseCode : 500;
        context.Response.StatusCode = statusCode;
        await context.Response.WriteAsJsonAsync(
            new ApiResponse<object>(
                statusCode,
                exceptionHandlerFeature?.Error.Message ?? ""
            )
        );
    });
});
#endregion

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseMiddleware<AuthMiddleware>();
app.UseWhen(context => context.Request.Path.Value?.ToLower().StartsWith("/extrareports") == true, app => app.UseMiddleware<ExtraReportsAuthMiddleware>());
app.UseWhen(context => context.Request.Path.Value?.ToLower().StartsWith("/reports") == true, app => app.UseMiddleware<ReportAuthMiddleware>());

app.MapControllers();
app.Run();
