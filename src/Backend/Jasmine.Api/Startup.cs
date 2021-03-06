﻿using Jasmine.Api.Services;
using Jasmine.Api.Services.Watcher;
using Jasmine.Api.Storage;
using Jasmine.Api.WS;
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
                .AddTransient<IDeviceService, DeviceService>()
                .AddSingleton<IMovementsStorage, MongoMovementsStorage>()
                .AddTransient<IMovementService, MovementService>()
                .AddSingleton<IWebSocketHandler, NotificationsMessageHandler>()
                .AddSingleton<IStatusWatcherService, StatusWatcherService>()
                .AddSingleton<INotificationsService, WSNotificationsService>()
                .AddSingleton<IMongoDatabase>(svcProvider =>
                {
                    var settingsService = svcProvider.GetService<ISettingsService>();
                    IMongoClient client = new MongoClient(settingsService.GetConnectionString());
                    return client.GetDatabase(settingsService.GetMovementsDbName());
                });

            services.AddCors(options =>
                options.AddPolicy("AllowAllOrigins",
                    builder => { builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod(); }
                ));

            services.AddMvc();
            services.AddWebSocketManager();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();
            app.UseCors("AllowAllOrigins");

            app.UseMvc();

            app.UseWebSockets();
            app.MapWebSocketManager("/notifications", app.ApplicationServices.GetService<IWebSocketHandler>());
            app.UseStatusWatcher();
        }
    }
}
