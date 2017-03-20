using Jasmine.Api.Services;
using Jasmine.Api.Storage;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;

namespace Jasmine.Api
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddSingleton<ISettingsService, SettingsService>()
                .AddSingleton<IConfigurationRoot>(Configuration)
                .AddSingleton<IMovementsStorage, MongoMovementsStorage>()
                .AddTransient<IMovementService, MovementService>()
                .AddSingleton<IMongoDatabase>(svcProvider =>
                {
                    var settingsService = svcProvider.GetService<ISettingsService>();
                    IMongoClient client = new MongoClient(settingsService.GetConnectionString());
                    return client.GetDatabase(settingsService.GetMovementsDbName());
                });

            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseMvc();
        }
    }
}
