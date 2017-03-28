using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Jasmine.Api.Definitions;
using Jasmine.Api.Models;
using Jasmine.Api.Services;
using MongoDB.Driver;

namespace Jasmine.Api.Storage
{
    public class MongoMovementsStorage : IMovementsStorage
    {
        private readonly ISettingsService _settingsService;
        private readonly IMongoCollection<MovementAction> _movementCollection;

        public MongoMovementsStorage(ISettingsService settingsService, IMongoDatabase database)
        {
            _settingsService = settingsService;
            _movementCollection = database.GetCollection<MovementAction>(settingsService.GetMovementsDbName());
        }

        public async Task AddAsync(MovementAction action)
        {
            await _movementCollection.InsertOneAsync(action);
        }

        public async Task<IEnumerable<MovementAction>> GetActionsByPageAsync(Guid deviceId, int pageNum, int pageSize)
        {
            IAsyncCursor<MovementAction> all = await _movementCollection.FindAsync(d => d.DeviceId.Equals(deviceId));
            List<MovementAction> allAsList = await all.ToListAsync();

            var page = allAsList.Skip(pageNum * pageSize);
            if (pageSize == 0)
            {
                return page;
            }

            return page.Take(pageSize);
        }

        public async Task<long> GetMovementActionsCountAsync(Guid deviceId)
        {
            return await _movementCollection.CountAsync(d => d.DeviceId.Equals(deviceId));
        }

        public async Task<SensorActivity> GetSensorActivityAsync(Guid deviceId, SensorType type)
        {
            var matched = await _movementCollection.FindAsync(f => f.DeviceId.Equals(deviceId) && f.Sensor == type);
            List<MovementAction> asList = await matched.ToListAsync();
            MovementAction last = asList.LastOrDefault();

            if (last == null)
            {
                return null;
            }

            return new SensorActivity()
            {
                Value = last.Value,
                Type = last.Sensor,
                EntryDate = last.EntryDate
            };
        }
    }
}
