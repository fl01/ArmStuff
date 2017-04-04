using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Jasmine.Api.Services.Watcher
{
    public static class StatusWatcherExtensions
    {
        public static void UseStatusWatcher(this IApplicationBuilder app)
        {
            var service = app.ApplicationServices.GetService<IStatusWatcherService>();
            Task.Factory.StartNew(() =>
            {
                service.Watch(Definitions.Devices.RUWC.ID);
            },
            TaskCreationOptions.LongRunning);
        }
    }
}
