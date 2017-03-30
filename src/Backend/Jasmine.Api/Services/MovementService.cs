using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Jasmine.Api.Definitions;
using Jasmine.Api.Models;
using Jasmine.Api.Storage;

namespace Jasmine.Api.Services
{
    public class MovementService : IMovementService
    {
        private readonly IMovementsStorage _storage;

        public MovementService(IMovementsStorage storage)
        {
            _storage = storage;
        }

        public async Task AddMovementAsync(MovementChangedData movementData)
        {
            var action = new MovementAction()
            {
                DeviceId = movementData.DeviceId,
                EntryDate = movementData.EntryDate,
                Sensor = movementData.Sensor,
                Value = movementData.Value
            };

            await _storage.AddAsync(action);
        }

        public async Task<MovementHistory> GetHistoryForDeviceAsync(Guid deviceId, DateTime? since, int page, int limit)
        {
            IEnumerable<MovementAction> actions = await _storage.GetActionsByPageAsync(deviceId, since ?? default(DateTime), page, limit);
            long total = await _storage.GetMovementActionsCountAsync(deviceId);

            return new MovementHistory(total, actions);
        }

        public async Task<SensorActivity> GetSensorStatusAsync(Guid deviceId, SensorType type)
        {
            return await _storage.GetSensorActivityAsync(deviceId, type);
        }
    }
}
