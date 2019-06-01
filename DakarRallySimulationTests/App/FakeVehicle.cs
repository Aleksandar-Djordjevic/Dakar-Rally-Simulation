using System;
using DakarRallySimulation;
using DakarRallySimulation.Domain;

namespace DakarRallySimulationTests.App
{
    
    public class FakeVehicle : IAmVehicle
    {
        public FakeVehicle(string vehicleId)
        {
            Id = vehicleId;
        }
        public int CompareTo(object obj)
        {
            return 1;
        }

        public event EventHandler FinishedRally;
        public event EventHandler Moved;
        public string Id { get; }
        public decimal Distance { get; }
        public DateTime? FinishedAt { get; }
        public void StartRally(Rally rally)
        {}
    }
}
