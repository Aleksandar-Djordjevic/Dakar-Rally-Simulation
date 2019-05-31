using System;
using System.Threading.Tasks;

namespace DakarRallySimulation
{
    public interface IAmVehicle : IComparable
    {
        event EventHandler FinishedRally;
        event EventHandler Moved;
        string Id { get; }
        decimal Distance { get; }
        DateTime? FinishedAt { get; }
        void StartRally(Rally rally);
    }
}