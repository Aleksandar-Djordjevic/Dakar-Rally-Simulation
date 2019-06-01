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

    public class Malfunction
    {
        public DateTime OccuredOn { get; set; }
        public string Severity { get; set; }
    }
}
