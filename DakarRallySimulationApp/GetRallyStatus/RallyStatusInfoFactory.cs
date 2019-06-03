using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DakarRallySimulation.Domain;

namespace DakarRallySimulation.App.GetRallyStatus
{
    public class RallyStatusInfoFactory : ICreateRallyStatusInfo
    {
        public RallyStatusInfo Create(IAmRally rally)
        {

            return new RallyStatusInfo
            {
                Status = GetStatus(rally),
                NumberOfVehiclesByStatus = GetNumberOfVehiclesByStatus(rally),
                NumberOfVehiclesByType = GetNumberOfVehiclesByType(rally)
            };
        }

        private static List<VehicleCountByStatus> GetNumberOfVehiclesByStatus(IAmRally rally)
        {
            return rally.Vehicles.Values
                .GroupBy(vehicle => vehicle.Status)
                .Select(group => 
                    new VehicleCountByStatus(group.Key.ToDto(), group.Count()))
                .ToList();
        }

        private static List<VehicleCountByType> GetNumberOfVehiclesByType(IAmRally rally)
        {
            return rally.Vehicles.Values
                .GroupBy(vehicle => vehicle.Type)
                .Select(group => 
                    new VehicleCountByType(group.Key.ToDto(), group.Count()))
                .ToList();
        }

        private RallyStatus GetStatus(IAmRally rally)
        {
            return rally.GetStatus().ToDto();
        }
    }
}
