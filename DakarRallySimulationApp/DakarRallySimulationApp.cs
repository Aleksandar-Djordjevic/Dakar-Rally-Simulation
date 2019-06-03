using System;
using System.Collections.Generic;
using CSharpFunctionalExtensions;
using DakarRallySimulation.App.AddVehicleToRally;
using DakarRallySimulation.App.CreateRally;
using DakarRallySimulation.App.FindVehicle;
using DakarRallySimulation.App.GetLeaderboard;
using DakarRallySimulation.App.GetRallyStatus;
using DakarRallySimulation.App.GetVehicleStatistics;
using DakarRallySimulation.App.RemoveVehicleFromRally;
using DakarRallySimulation.App.StartRally;
using AddingVehicle = DakarRallySimulation.App.AddVehicleToRally.Vehicle;
using FoundVehicle = DakarRallySimulation.App.FindVehicle.Vehicle;

namespace DakarRallySimulation.App
{
    public class DakarRallySimulationApp : ISimulateDakarRally
    {
        private readonly ICreateRally _createRallyService;
        private readonly IAddVehicleToRally _addVehicleToRallyService;
        private readonly IRemoveVehicleFromRally _removeVehicleFromRallyService;
        private readonly IStartRally _startRallyService;
        private readonly IProvideVehicleStatistics _vehicleStatisticsService;
        private readonly IProvideRallyStatusInfo _getRallyStatusInfoService;
        private readonly IProvideLeaderboard _leaderboardService;
        private readonly IFindVehicle _findVehicleService;

        public DakarRallySimulationApp(ICreateRally createRallyService, IAddVehicleToRally addVehicleToRallyService, IRemoveVehicleFromRally removeVehicleFromRallyService, IStartRally startRallyService, IProvideVehicleStatistics vehicleStatisticsService, IProvideRallyStatusInfo getRallyStatusInfoService, IProvideLeaderboard leaderboardService, IFindVehicle findVehicleService)
        {
            _createRallyService = createRallyService;
            _addVehicleToRallyService = addVehicleToRallyService;
            _removeVehicleFromRallyService = removeVehicleFromRallyService;
            _startRallyService = startRallyService;
            _vehicleStatisticsService = vehicleStatisticsService;
            _getRallyStatusInfoService = getRallyStatusInfoService;
            _leaderboardService = leaderboardService;
            _findVehicleService = findVehicleService;
        }

        public Result CreateRally(int year)
        {
            return _createRallyService.CreateRally(year);
        }

        public Result AddVehicle(string rallId, AddingVehicle vehicle)
        {
            return _addVehicleToRallyService.AddVehicle(rallId, vehicle);
        }

        public Result RemoveVehicleFromRally(string rallyId, string vehicleId)
        {
            return _removeVehicleFromRallyService.RemoveVehicleFromRally(rallyId, vehicleId);
        }

        public Result StartRally(string rallyId)
        {
            return _startRallyService.StartRally(rallyId);
        }

        public Result<Leaderboard> GetLeaderboard(string rallyId)
        {
            return _leaderboardService.GetLeaderboard(rallyId);
        }

        public Result<Leaderboard> GetLeaderboard(string rallyId, VehicleType type)
        {
            return _leaderboardService.GetLeaderboard(rallyId, type);
        }

        public Result<VehicleStatistics> GetVehicleStatistics(string rallyId, string vehicleId)
        {
            return _vehicleStatisticsService.GetVehicleStatistics(rallyId, vehicleId);
        }

        public Result<IEnumerable<FoundVehicle>> FindVehicle(string rallyId, Query query)
        {
            return _findVehicleService.FindVehicle(rallyId, query);
        }

        public Result<RallyStatusInfo> GetRallyStatusInfo(string rallyId)
        {
            return _getRallyStatusInfoService.GetRallyStatusInfo(rallyId);
        }
    }
}
