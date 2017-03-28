using System;
using System.Threading.Tasks;
using Jasmine.Api.Definitions;
using Jasmine.Api.Models;

namespace Jasmine.Api.Services
{
    public interface IMovementService
    {
        Task AddMovementAsync(MovementChangedData movementData);

        Task<MovementHistory> GetHistoryForDeviceAsync(Guid deviceId, int page, int limit);

        Task<SensorActivity> GetSensorStatusAsync(Guid deviceId, SensorType type);
    }
}
