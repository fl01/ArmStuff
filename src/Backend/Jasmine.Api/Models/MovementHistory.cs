using System.Collections.Generic;
using System.Linq;

namespace Jasmine.Api.Models
{
    public class MovementHistory
    {
        public long Total { get; set; }

        public IEnumerable<MovementAction> Actions { get; set; } = Enumerable.Empty<MovementAction>();

        public MovementHistory()
        {
        }

        public MovementHistory(long total, IEnumerable<MovementAction> actions)
        {
            Total = total;
            Actions = actions;
        }
    }
}
