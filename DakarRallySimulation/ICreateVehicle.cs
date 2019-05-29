using System;

namespace DakarRallySimulation
{
    public interface ICreateVehicle
    {
        Vehicle BuildCrossMotorcycle(string id, string teamName, string model, DateTime manufacturingDate);
        Vehicle BuildSportCar(string id, string teamName, string model, DateTime manufacturingDate);
        Vehicle BuildSportMotorcycle(string id, string teamName, string model, DateTime manufacturingDate);
        Vehicle BuildTerrainCar(string id, string teamName, string model, DateTime manufacturingDate);
        Vehicle BuildTruck(string id, string teamName, string model, DateTime manufacturingDate);
    }
}