using System;

namespace DakarRallySimulation.Domain
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