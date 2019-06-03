using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DakarRallySimulation.Domain;

namespace DakarRallySimulation.App.FindVehicle
{
    public static class MappingExtensions
    {
        public static Vehicle ToDto(this IAmVehicle vehicle)
        {
            return new Vehicle
            {
                Id = vehicle.Id,
                Type = vehicle.Type.ToDto(),
                Status = vehicle.Status.ToDto(),
                TeamName = vehicle.TeamName,
                Model = vehicle.Model,
                ManufacturingDate = vehicle.ManufacturingDate,
                DistanceFromStart = vehicle.Distance
            };
        }

        public static List<Vehicle> ToDto(this IEnumerable<IAmVehicle> vehicles)
        {
            return vehicles.Select(ToDto).ToList();
        }
    }
}
