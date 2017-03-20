using System.Threading.Tasks;
using Jasmine.Api.Models;

namespace Jasmine.Api.Services
{
    public interface IMovementService
    {
        Task AddAsync(MovementChangedData movementData);
    }
}
