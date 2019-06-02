using System;
using System.Collections.Generic;

namespace DakarRallySimulation.Domain
{
    public interface IAmVehicle : IComparable
    {
        event EventHandler FinishedRally;
        event EventHandler Moved;
        string Id { get; }
        decimal Distance { get; }
        DateTime? FinishedAt { get; }
        List<Malfunction> MalfunctionHistory { get; }
        void StartRally(Rally rally);
    }
}