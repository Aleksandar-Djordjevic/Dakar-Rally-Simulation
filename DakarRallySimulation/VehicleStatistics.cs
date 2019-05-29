﻿using System;
using System.Collections.Generic;

namespace DakarRallySimulation
{
    public class VehicleStatistics
    {
        public VehicleState Status { get; internal set; }
        public decimal DistanceFromStart { get; internal set; }
        public DateTime? FinishTime { get; internal set; }
        public List<Malfunction> Malfunctions { get; internal set; }
    }
}