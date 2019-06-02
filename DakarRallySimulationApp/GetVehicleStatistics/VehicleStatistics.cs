using System;
using System.Collections.Generic;

namespace DakarRallySimulation.App.GetVehicleStatistics
{
    public class VehicleStatistics
    {
        public VehicleStatus Status { get; set; }
        public decimal DistanceFromStart { get; set; }
        public List<Malfunction> MalfunctionStatistics { get; set; }
        public DateTime? FinishTime { get; set; }
    }

    public enum VehicleStatus
    {
        Running,
        Repairing,
        Broken,
        FinishedRally
    }

    public class Malfunction
    {
        public DateTime OccuredOn { get; set; }
        public DamageLevel Damage { get; set; }
    }

    public enum DamageLevel
    {
        Heavy,
        Light
    }
}
