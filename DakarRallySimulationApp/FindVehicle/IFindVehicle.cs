using System;
using System.Collections.Generic;
using CSharpFunctionalExtensions;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace DakarRallySimulation.App.FindVehicle
{
    public interface IFindVehicle
    {
        Result<IEnumerable<Vehicle>> FindVehicle(string rallyId, Query query);
    }

    public class Vehicle
    {
        public string Id { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public VehicleType Type { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public VehicleStatus Status { get; set; }
        public string TeamName { get; set; }
        public string Model { get; set; }
        public DateTime ManufacturingDate { get; set; }
        public decimal DistanceFromStart { get; set; }
    }
}