using System;

namespace DakarRallySimulation
{
    public interface ICreateVehicle
    {
        IAmVehicle CreateCrossMotorcycle(string id, string teamName, string model, DateTime manufacturingDate);
        IAmVehicle CreateSportCar(string id, string teamName, string model, DateTime manufacturingDate);
        IAmVehicle CreateSportMotorcycle(string id, string teamName, string model, DateTime manufacturingDate);
        IAmVehicle CreateTerrainCar(string id, string teamName, string model, DateTime manufacturingDate);
        IAmVehicle CreateTruck(string id, string teamName, string model, DateTime manufacturingDate);
    }
}