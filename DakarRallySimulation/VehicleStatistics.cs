using System;
using System.Collections.Generic;
using DakarRallySimulation.Domain.Vehicle;

namespace DakarRallySimulation.Domain
{
    public class VehicleStatistics
    {
        public VehicleStatus Status { get; internal set; }
        public decimal DistanceFromStart { get; internal set; }
        public DateTime? FinishTime { get; internal set; }
        public List<Malfunction> Malfunctions { get; internal set; }
    }
}