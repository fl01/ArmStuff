using System.Threading.Tasks;
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
        public async Task AddAsync(MovementChangedData movementData)
        {
            await Task.FromResult(1);
        }
    }
}
