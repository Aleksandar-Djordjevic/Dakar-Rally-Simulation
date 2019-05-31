using System;

namespace DakarRallySimulationApp
{
    public interface IFindVehicle
    {
        void FindVehicle(string team, string model, DateTime manufacturingDate, VehicleStatus status);
    }
}