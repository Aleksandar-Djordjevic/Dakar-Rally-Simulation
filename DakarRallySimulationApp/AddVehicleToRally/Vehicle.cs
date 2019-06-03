using System;
using System.Collections.Generic;
using System.Text;

namespace DakarRallySimulation.App.AddVehicleToRally
{
    public class Vehicle
    {
        public string Id { get; set; }
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
