using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using DakarRallySimulation.Domain;

namespace DakarRallySimulation.Tests.App
{
    
    public class FakeVehicle : IAmVehicle
    {
        public FakeVehicle(string vehicleId)
        {
            Id = vehicleId;
        }

        public event EventHandler FinishedRally;
        
        public string Id { get; }
        public VehicleType Type { get; }
        public string TeamName { get; }
        public string Model { get; }
        public DateTime ManufacturingDate { get; }
        public decimal Distance { get; }
        public VehicleStatus Status { get; }
        public DateTime? FinishedAt { get; }
        public ImmutableList<Malfunction> MalfunctionHistory { get; }
        public void StartRally(Rally rally) {}
    }
}
