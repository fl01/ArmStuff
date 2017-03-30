using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Jasmine.Api.Definitions;
using Jasmine.Api.Models;

namespace Jasmine.Api.Storage
{
    public interface IMovementsStorage
    {
        Task AddAsync(MovementAction action);

        Task<IEnumerable<MovementAction>> GetActionsByPageAsync(Guid deviceId, DateTime since, int pageNum, int pageSize);

        Task<long> GetMovementActionsCountAsync(Guid deviceId);

        Task<SensorActivity> GetSensorActivityAsync(Guid deviceId, SensorType type);
    }
}
