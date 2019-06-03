using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace DakarRallySimulation.App.AddVehicleToRally
{
    public class Vehicle
    {
        public string Id { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public VehicleType Type { get; set; }
        public string TeamName { get; set; }
        public string Model { get; set; }
        public DateTime ManufacturingDate { get; set; }
    }

    public enum VehicleType
    {
        SportCar,
        TerrainCar,
        Truck,
        SportMotorcycle,
        CrossMotorcycle
    }
}
