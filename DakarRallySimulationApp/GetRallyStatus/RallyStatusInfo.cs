using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace DakarRallySimulation.App.GetRallyStatus
{
    public class RallyStatusInfo
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public RallyStatus Status { get; set; }
        public List<VehicleCountByStatus> NumberOfVehiclesByStatus { get; set; }
        public List<VehicleCountByType> NumberOfVehiclesByType { get; set; }
    }

    public enum RallyStatus
    {
        Pending,
        Running,
        Finished
    }

    public struct VehicleCountByStatus
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public VehicleStatus VehicleStatus { get; set; }
        public int Count { get; set; }

        public VehicleCountByStatus(VehicleStatus vehicleStatus, int count)
        {
            VehicleStatus = vehicleStatus;
            Count = count;
        }
    }

    public struct VehicleCountByType
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public VehicleType VehicleType { get; set; }
        public int Count { get; set; }

        public VehicleCountByType(VehicleType vehicleType, int count)
        {
            VehicleType = vehicleType;
            Count = count;
        }
    }
}