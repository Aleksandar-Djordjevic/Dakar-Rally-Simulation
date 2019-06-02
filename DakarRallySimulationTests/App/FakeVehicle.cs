using System;
using System.Collections.Generic;
using DakarRallySimulation.Domain;

namespace DakarRallySimulation.Tests.App
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
        public List<Malfunction> MalfunctionHistory { get; }
        public void StartRally(Rally rally) {}

        public override bool Equals(object obj)
        {
            var vehicle = obj as FakeVehicle;
            return vehicle != null &&
                   Id == vehicle.Id &&
                   Distance == vehicle.Distance &&
                   EqualityComparer<DateTime?>.Default.Equals(FinishedAt, vehicle.FinishedAt) &&
                   EqualityComparer<List<Malfunction>>.Default.Equals(MalfunctionHistory, vehicle.MalfunctionHistory);
        }

        public override int GetHashCode()
        {
            var hashCode = -1546983277;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Id);
            hashCode = hashCode * -1521134295 + Distance.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<DateTime?>.Default.GetHashCode(FinishedAt);
            hashCode = hashCode * -1521134295 + EqualityComparer<List<Malfunction>>.Default.GetHashCode(MalfunctionHistory);
            return hashCode;
        }
    }
}
