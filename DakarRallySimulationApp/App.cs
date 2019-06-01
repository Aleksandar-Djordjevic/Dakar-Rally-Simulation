using System;
using System.Threading.Tasks;
using System.Xml.Schema;
using CSharpFunctionalExtensions;

namespace DakarRallySimulationApp
{
    public class App : ISimulateDakarRally
    {
        private readonly ICreateRally _createRallyService;
        private readonly IAddVehicleToRally _addVehicleToRallyService;

        public App(ICreateRally createRallyService, IAddVehicleToRally addVehicleToRallyService)
        {
            _createRallyService = createRallyService;
            _addVehicleToRallyService = addVehicleToRallyService;
        }

        public Result CreateRally(int year)
        {
            return _createRallyService.CreateRally(year);
        }

        public Result AddSportCar(string rallyId, string vehicleId, string teamName, string model, DateTime manufacturingDate)
        {
            return _addVehicleToRallyService.AddSportCar(rallyId, vehicleId, teamName, model, manufacturingDate);
        }

        public Result AddTerrainCar(string rallyId, string vehicleId, string teamName, string model, DateTime manufacturingDate)
        {
            return _addVehicleToRallyService.AddTerrainCar(rallyId, vehicleId, teamName, model, manufacturingDate);
        }

        public Result AddTruck(string rallyId, string vehicleId, string teamName, string model, DateTime manufacturingDate)
        {
            return _addVehicleToRallyService.AddTruck(rallyId, vehicleId, teamName, model, manufacturingDate);
        }

        public Result AddSportMotorcycle(string rallyId, string vehicleId, string teamName, string model, DateTime manufacturingDate)
        {
            return _addVehicleToRallyService.AddSportMotorcycle(rallyId, vehicleId, teamName, model, manufacturingDate);
        }

        public Result AddCrossMotorcycle(string rallyId, string vehicleId, string teamName, string model, DateTime manufacturingDate)
        {
            return _addVehicleToRallyService.AddCrossMotorcycle(rallyId, vehicleId, teamName, model, manufacturingDate);
        }

        public Result RemoveVehicleFromRally(string rallyId, string vehicleId)
        {
            throw new NotImplementedException();
        }

        public void StartRally(string rallyId)
        {
            throw new NotImplementedException();
        }

        public void GetLeaderboard()
        {
            throw new NotImplementedException();
        }

        public void GetLeaderboard(VehicleType type)
        {
            throw new NotImplementedException();
        }

        public void GetVehicleStatistics(string vehicleId)
        {
            throw new NotImplementedException();
        }

        public void FindVehicle(string team, string model, DateTime manufacturingDate, VehicleStatus status)
        {
            throw new NotImplementedException();
        }

        public void GetRallyStatus(string rallyId)
        {
            throw new NotImplementedException();
        }
    }
}
