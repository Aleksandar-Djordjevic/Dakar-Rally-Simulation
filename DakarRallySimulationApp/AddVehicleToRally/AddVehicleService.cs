using System;
using CSharpFunctionalExtensions;
using DakarRallySimulation.Domain;

namespace DakarRallySimulation.App.AddVehicleToRally
{
    public class AddVehicleService : IAddVehicleToRally
    {
        private readonly IAmRallyRepository _rellyRepo;
        private readonly ICreateVehicle _vehicleFactory;

        public AddVehicleService(IAmRallyRepository rellyRepo, ICreateVehicle vehicleFactory)
        {
            _rellyRepo = rellyRepo;
            _vehicleFactory = vehicleFactory;
        }

        public Result AddSportCar(string rallyId, string vehicleId, string teamName, string model, DateTime manufacturingDate)
        {
            return AddVehicle(rallyId, vehicleId, teamName, model, manufacturingDate, _vehicleFactory.CreateSportCar);
        }

        public Result AddTerrainCar(string rallyId, string vehicleId, string teamName, string model, DateTime manufacturingDate)
        {
            return AddVehicle(rallyId, vehicleId, teamName, model, manufacturingDate, _vehicleFactory.CreateTerrainCar);
        }

        public Result AddTruck(string rallyId, string vehicleId, string teamName, string model, DateTime manufacturingDate)
        {
            return AddVehicle(rallyId, vehicleId, teamName, model, manufacturingDate, _vehicleFactory.CreateTruck);
        }

        public Result AddSportMotorcycle(string rallyId, string vehicleId, string teamName, string model, DateTime manufacturingDate)
        {
            return AddVehicle(rallyId, vehicleId, teamName, model, manufacturingDate, _vehicleFactory.CreateSportMotorcycle);
        }

        public Result AddCrossMotorcycle(string rallyId, string vehicleId, string teamName, string model, DateTime manufacturingDate)
        {
            return AddVehicle(rallyId, vehicleId, teamName, model, manufacturingDate, _vehicleFactory.CreateCrossMotorcycle);
        }

        private Result AddVehicle(string rallyId, string vehicleId, string teamName, string model, DateTime manufacturingDate, Func<string, string, string, DateTime, IAmVehicle> createVehicle)
        {
            return _rellyRepo.Find(rallyId)
                .OnFailureCompensate(failure => Result.Fail<IAmRally>(ErrorMessages.RallyNotFound))
                .OnSuccess(rally => rally.AddVehicle(createVehicle(vehicleId, teamName, model, manufacturingDate)));
        }
    }
}
