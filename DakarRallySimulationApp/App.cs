using System;
using CSharpFunctionalExtensions;
using DakarRallySimulation.App.AddVehicleToRally;
using DakarRallySimulation.App.CreateRally;
using DakarRallySimulation.App.GetLeaderboard;
using DakarRallySimulation.App.GetVehicleStatistics;
using DakarRallySimulation.App.RemoveVehicleFromRally;
using DakarRallySimulation.App.StartRally;

namespace DakarRallySimulation.App
{
    public class App : ISimulateDakarRally
    {
        private readonly ICreateRally _createRallyService;
        private readonly IAddVehicleToRally _addVehicleToRallyService;
        private readonly IRemoveVehicleFromRally _removeVehicleFromRallyService;
        private readonly IStartRally _startRallyService;
        private readonly IProvideVehicleStatistics _vehicleStatisticsService;

        public App(ICreateRally createRallyService, IAddVehicleToRally addVehicleToRallyService, IRemoveVehicleFromRally removeVehicleFromRallyService, IStartRally startRallyService)
        {
            _createRallyService = createRallyService;
            _addVehicleToRallyService = addVehicleToRallyService;
            _removeVehicleFromRallyService = removeVehicleFromRallyService;
            _startRallyService = startRallyService;
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
            return _removeVehicleFromRallyService.RemoveVehicleFromRally(rallyId, vehicleId);
        }

        public Result StartRally(string rallyId)
        {
            return _startRallyService.StartRally(rallyId);
        }

        public void GetLeaderboard()
        {
            throw new NotImplementedException();
        }

        public void GetLeaderboard(VehicleType type)
        {
            throw new NotImplementedException();
        }

        public Result<VehicleStatistics> GetVehicleStatistics(string rallyId, string vehicleId)
        {
            return _vehicleStatisticsService.GetVehicleStatistics(rallyId, vehicleId);
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
