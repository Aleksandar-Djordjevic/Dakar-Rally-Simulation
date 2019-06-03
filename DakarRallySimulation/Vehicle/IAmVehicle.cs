using System;
using System.Collections.Generic;

namespace DakarRallySimulation.Domain.Vehicle
{
    public interface IAmVehicle
    {
        event EventHandler FinishedRally;
        event EventHandler Moved;
        string Id { get; }
        VehicleType Type { get; }
        decimal Distance { get; }
        VehicleStatus Status { get; }
        DateTime? FinishedAt { get; }
        List<Malfunction> MalfunctionHistory { get; }
        void StartRally(Rally rally);
    }
}