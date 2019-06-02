using System;
using System.Collections.Generic;

namespace DakarRallySimulation.App.GetRallyStatus
{
    public class RallyStatusInfo
    {
        public RallyStatus Status { get; set; }
        public List<Tuple<VehicleStatus, int>> NumberOfVehiclesByStatus { get; set; }
        public List<Tuple<VehicleType, int>> NumberOfVehiclesByType { get; set; }
    }
}