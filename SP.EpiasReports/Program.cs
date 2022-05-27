using MongoDB.Driver;
using SP.Reports.Service;
using SP.Reports.Models.Api;
using SP.User.Service;
using SP.User.Service.Jwt;
using SP.User.Service.Middlewares;
using SP.Exceptions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

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

builder.Services.AddSingleton<IMongoClient>(_ => new MongoClient(builder.Configuration.GetValue<string>("MongoDbConnectionString")));
builder.Services.AddTransient<IReportsService, ReportsService>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IJwtUtils, JwtUtils>();

builder.Services.Configure<ApiPaths>(builder.Configuration.GetSection("Api").GetSection("Paths"));
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));
builder.Services.AddOptions();

var app = builder.Build();

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
