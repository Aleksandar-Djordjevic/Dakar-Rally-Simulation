using System;
using CSharpFunctionalExtensions;

namespace DakarRallySimulationApp
{
    public interface IAddVehicleToRally
    {
        Result AddSportCar(string rallyId, string model, DateTime manufacturingDate);
        Result AddTerrainCar(string rallyId, string model, DateTime manufacturingDate);
        Result AddTruck(string rallyId, string model, DateTime manufacturingDate);
        Result AddSportMotorCycle(string rallyId, string model, DateTime manufacturingDate);
        Result AddCrossMotorCycle(string rallyId, string model, DateTime manufacturingDate);
    }
}