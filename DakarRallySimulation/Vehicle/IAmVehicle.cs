using System;
using System.Collections.Immutable;

namespace DakarRallySimulation.Domain
{
    public interface IAmVehicle
    {
        event EventHandler FinishedRally;
        string Id { get; }
        VehicleType Type { get; }
        decimal Distance { get; }
        VehicleStatus Status { get; }
        DateTime? FinishedAt { get; }
        ImmutableList<Malfunction> MalfunctionHistory { get; }
        void StartRally(Rally rally);
    }
}