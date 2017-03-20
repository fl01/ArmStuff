using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Jasmine.Api.Models;

namespace Jasmine.Api.Storage
{
    public interface IMovementsStorage
    {
        Task AddAsync(MovementAction action, CancellationToken token);

        Task<IEnumerable<MovementAction>> GetActionsByPageAsync(int pageNum, int pageSize);
        
        Task<long> GetMovementActionsCountAsync();
    }
}
