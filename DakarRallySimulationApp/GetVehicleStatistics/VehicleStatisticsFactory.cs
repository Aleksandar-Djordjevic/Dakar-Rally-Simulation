using System.Collections.Generic;
using System.Linq;
using DakarRallySimulation.Domain;

namespace DakarRallySimulation.App.GetVehicleStatistics
{
    public class VehicleStatisticsFactory : ICreateVehicleStatistics
    {
        public VehicleStatistics Create(IAmVehicle vehicle)
        {
            var stat = new VehicleStatistics
            {
                Status = vehicle.Status.ToDto(),
                DistanceFromStart = vehicle.Distance,
                FinishTime = vehicle.FinishedAt,
                MalfunctionStatistics = GetMalfunctionStatistics(vehicle),
            };

            return stat;
        }

        private List<Malfunction> GetMalfunctionStatistics(IAmVehicle vehicle)
        {
            return new List<Malfunction>(vehicle.MalfunctionHistory.Select(
                malfunction => new Malfunction
                {
                    OccuredOn = malfunction.OccuredOn,
                    Damage = malfunction.IsHeavy? DamageLevel.Heavy : DamageLevel.Light
                }));
        }
    }
}
