using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace DakarRallySimulation.App.GetVehicleStatistics
{
    public class VehicleStatistics
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public VehicleStatus Status { get; set; }
        public decimal DistanceFromStart { get; set; }
        public List<Malfunction> MalfunctionStatistics { get; set; }
        public DateTime? FinishTime { get; set; }
    }

    public class Malfunction
    {
        public DateTime OccuredOn { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public DamageLevel Damage { get; set; }
    }

    public enum DamageLevel
    {
        Heavy,
        Light
    }
}
