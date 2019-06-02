using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DakarRallySimulation.Domain;
using DakarRallySimulation.Domain.Vehicle;

namespace DakarRallySimulation.App.GetVehicleStatistics
{
    public class VehicleStatisticsFactory : ICreateVehicleStatistics
    {
        public VehicleStatistics Create(IAmVehicle vehicle)
        {
            var stat = new VehicleStatistics
            {
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
