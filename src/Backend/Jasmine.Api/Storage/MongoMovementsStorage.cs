using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
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

        public async Task AddAsync(MovementAction action, CancellationToken token)
        {
            await _movementCollection.InsertOneAsync(action, null);
        }

        public async Task<IEnumerable<MovementAction>> GetActionsByPageAsync(int pageNum, int pageSize)
        {
            IAsyncCursor<MovementAction> all = await _movementCollection.FindAsync(_ => true);
            List<MovementAction> allAsList = await all.ToListAsync();

            return allAsList.Skip(pageNum * pageSize).Take(pageSize);
        }

        public async Task<long> GetMovementActionsCountAsync()
        {
            return await _movementCollection.CountAsync(_ => true);
        }
    }
}
