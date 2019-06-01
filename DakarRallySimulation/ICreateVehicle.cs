using System;

namespace DakarRallySimulation
{
    public interface ICreateVehicle
    {
        Vehicle CreateCrossMotorcycle(string id, string teamName, string model, DateTime manufacturingDate);
        Vehicle CreateSportCar(string id, string teamName, string model, DateTime manufacturingDate);
        Vehicle CreateSportMotorcycle(string id, string teamName, string model, DateTime manufacturingDate);
        Vehicle CreateTerrainCar(string id, string teamName, string model, DateTime manufacturingDate);
        Vehicle CreateTruck(string id, string teamName, string model, DateTime manufacturingDate);
    }
}