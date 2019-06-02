using System;
using DakarRallySimulation.App.GetVehicleStatistics;

namespace DakarRallySimulation.App.FindVehicle
{
    public interface IFindVehicle
    {
        void FindVehicle(string team, string model, DateTime manufacturingDate, GetVehicleStatistics.VehicleStatus status);
    }
}