using CompanySystem.Application.Interfaces;
using CompanySystem.Infrastructure.Repositories;
using CompanySystem.LoggerService;


namespace CompanySystem.Api.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureCors(this IServiceCollection services) =>
 services.AddCors(options =>
 {
     options.AddPolicy("CorsPolicy", builder =>
     builder.AllowAnyOrigin()
     .AllowAnyMethod()
     .AllowAnyHeader()
     .WithExposedHeaders("X-Pagination"));
 });
        //if we want to host our application on IIS
      public static void ConfigureIISIntegration(this IServiceCollection services) =>
            services.Configure<IISOptions>(options =>
           {
            });
        public static void ConfigureLoggerService(this IServiceCollection services) =>services.AddSingleton < ILoggerManager, LoggerManager>();

        

    }


}
