using System.Collections.Generic;
using System.Linq;
using DakarRallySimulation.Domain.Vehicle;

namespace DakarRallySimulation.App.GetLeaderboard
{
    public static class MappingExtensions
    {
        public static Vehicle ToDto(this IAmVehicle vehicle)
        {
            return new Vehicle
            {
                Id = vehicle.Id,
                Type = vehicle.Type.ToDto(),
                DistanceFromStart = vehicle.Distance,
            };
        }

        public static List<Vehicle> ToDto(this IEnumerable<IAmVehicle> vehicles)
        {
            return vehicles.Select(ToDto).ToList();
        }
    }
}
