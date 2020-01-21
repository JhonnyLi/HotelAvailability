using HotelAvailabilityApiService.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HotelAvailabilityApiService
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Latest);

            services.AddSingleton<ISecretsService, SecretsService>();
            services.AddScoped<IIntentService, IntentService>();
            services.AddSingleton<IHttpService, HttpService>();
            services.AddScoped<IHotelService, HotelService>();
            services.AddScoped<IAvailabilityService, AvailabilityService>();
            services.AddApplicationInsightsTelemetry();
        }

        public static void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
