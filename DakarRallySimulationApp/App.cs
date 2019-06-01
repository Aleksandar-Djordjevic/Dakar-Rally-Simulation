using System;
using System.Threading.Tasks;
using System.Xml.Schema;
using CSharpFunctionalExtensions;

namespace DakarRallySimulationApp
{
    public class App : ISimulateDakarRally
    {
        private readonly ICreateRally _createRallyService;

        public App(ICreateRally createRallyService)
        {
            _createRallyService = createRallyService;
        }


        public Result CreateRally(int year)
        {
            return _createRallyService.CreateRally(year);
        }

        public void AddSportCar(string rallyId, string model, DateTime manufacturingDate)
        {
            throw new NotImplementedException();
        }

        public void AddTerrainCar(string rallyId, string model, DateTime manufacturingDate)
        {
            throw new NotImplementedException();
        }

        public void AddTruck(string rallyId, string model, DateTime manufacturingDate)
        {
            throw new NotImplementedException();
        }

        public void AddSportMotorCycle(string rallyId, string model, DateTime manufacturingDate)
        {
            throw new NotImplementedException();
        }

        public void AddCrossMotorCycle(string rallyId, string model, DateTime manufacturingDate)
        {
            throw new NotImplementedException();
        }

        public void RemoveVehicleFromRally(string rallyId, string vehicleId)
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
