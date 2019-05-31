using System;

namespace DakarRallySimulationApp
{
    public interface IAddVehicleToRally
    {
        void AddSportCar(string rallyId, string model, DateTime manufacturingDate);
        void AddTerrainCar(string rallyId, string model, DateTime manufacturingDate);
        void AddTruck(string rallyId, string model, DateTime manufacturingDate);
        void AddSportMotorCycle(string rallyId, string model, DateTime manufacturingDate);
        void AddCrossMotorCycle(string rallyId, string model, DateTime manufacturingDate);
    }
}