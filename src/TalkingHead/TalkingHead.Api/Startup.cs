using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using TalkingHead.Api.Http;
using TalkingHead.Api.Services;
using TalkingHead.Api.Services.TTS;
using TalkingHead.Api.WS;

namespace TalkingHead.Api
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
                .AddSingleton<IWebSocketHandler, NotificationsMessageHandler>()
                .AddSingleton<IConfigurationRoot>(Configuration);

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

            app.UseMvc();

            app.UseCors("AllowAllOrigins");
            app.UseWebSockets();
            app.MapWebSocketManager("/notifications", app.ApplicationServices.GetService<IWebSocketHandler>());
        }
    }
}
