using System;

namespace DakarRallySimulation.App.FindVehicle
{
    public interface IFindVehicle
    {
        void FindVehicle(string team, string model, DateTime manufacturingDate, VehicleStatus status);
    }
}