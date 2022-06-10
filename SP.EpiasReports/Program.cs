using MongoDB.Driver;
using Serilog;
using SP.Exceptions;
using SP.Middlewares;
using SP.Reports.Models.Api;
using SP.Reports.Service;
using SP.Settings.Service;
using SP.User.Service;
using SP.Utils.Jwt;

var root = Directory.GetCurrentDirectory();
var dotenv = Path.Combine(root, ".env");
if (File.Exists(dotenv))
{
    foreach(var line in File.ReadAllLines(dotenv))
    {
        var parts = line.Split("=", StringSplitOptions.RemoveEmptyEntries);
        if (parts.Length != 2) continue;
        Environment.SetEnvironmentVariable(parts[0], parts[1]);
    }
}

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddCors();
builder.Services.AddControllers(options =>
{
    options.Filters.Add<HttpResponseExceptionFilter>();
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//builder.Services.AddHttpClient();
builder.Services.AddHttpClient("EpiasAPI", httpClient =>
{
    httpClient.BaseAddress = new Uri(Environment.GetEnvironmentVariable("EPIAS_API_BASE_URL") ?? builder.Configuration.GetValue<string>("Api:BaseUrl"));
});

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
builder.Services.AddSingleton<IMongoClient>(_ => new MongoClient(mongoDbConnString));
builder.Services.AddTransient<IReportsService, ReportsService>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<ISettingsService, SettingsService>();
builder.Services.AddTransient<IJwtUtils, JwtUtils>();

builder.Services.Configure<ApiPaths>(builder.Configuration.GetSection("Api").GetSection("Paths"));
builder.Services.AddOptions();

var app = builder.Build();

app.UseCors(builder =>
    builder.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader()
);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseExceptionHandler("/error-development");
}
else
{
    app.UseExceptionHandler("/error");
}

app.UseMiddleware<AuthMiddleware>();
app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();
app.Run();
