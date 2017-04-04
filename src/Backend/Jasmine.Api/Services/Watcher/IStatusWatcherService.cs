using System;

namespace Jasmine.Api.Services
{
    public interface IStatusWatcherService
    {
        void Watch(Guid deviceId);
    }
}
