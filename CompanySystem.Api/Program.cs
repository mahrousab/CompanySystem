using AspNetCoreRateLimit;
using CompanySystem.Api.Extensions;
using CompanySystem.Application.DTOS;
using CompanySystem.Application.Interfaces;
using CompanySystem.Application.services;
using CompanySystem.Infrastructure.Data;
using CompanySystem.Infrastructure.Repositories;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NLog;

Console.WriteLine("STARTING API");

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.ConfigureIISIntegration();
builder.Services.ConfigureLoggerService();

builder.Services.AddScoped<IRepositoryManager, RepositoryManager>();

builder.Services.AddScoped<IDataShaper<EmployeeDto>, DataShaper<EmployeeDto>>();
builder.Services.ConfigureVersioning();

builder.Services.ConfigureResponseCaching();

builder.Services
    .AddControllers(options =>
    {
        options.RespectBrowserAcceptHeader = true;
        options.ReturnHttpNotAcceptable = true;
        options.CacheProfiles.Add("120SecondsDuration", new CacheProfile { Duration = 120 });
    })
    .AddNewtonsoftJson()
    .AddXmlDataContractSerializerFormatters();

builder.Services.AddDbContext<CompanySystemDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
    ));

builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddOpenApi();
builder.Services.AddMemoryCache();
builder.Services.ConfigureRateLimitingOptions();
builder.Services.AddHttpContextAccessor();
builder.Services.AddAuthentication();
builder.Services.ConfigureIdentity();
builder.Services.ConfigureJwt(builder.Configuration);
builder.Services.AddJwtConfiguration(builder.Configuration);
builder.Services.AddScoped<IServiceManager, ServiceManager>();
var app = builder.Build();

LogManager.Setup()
    .LoadConfigurationFromFile("nlog.config");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseIpRateLimiting();
app.UseCors("CorsPolicy");
app.UseResponseCaching();

app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.All
});
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
