using System;
using CSharpFunctionalExtensions;

namespace DakarRallySimulation.App.AddVehicleToRally
{
    public interface IAddVehicleToRally
    {
        Result AddSportCar(string rallyId, string vehicleId, string teamName, string model, DateTime manufacturingDate);

        Result AddTerrainCar(string rallyId, string vehicleId, string teamName, string model, DateTime manufacturingDate);

        Result AddTruck(string rallyId, string vehicleId, string teamName, string model, DateTime manufacturingDate);

        Result AddSportMotorcycle(string rallyId, string vehicleId, string teamName, string model, DateTime manufacturingDate);

        Result AddCrossMotorcycle(string rallyId, string vehicleId, string teamName, string model, DateTime manufacturingDate);
    }
}