using System;
using System.ComponentModel;
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

        public Result AddVehicle(string rallyId, Vehicle vehicle)
        {
            switch (vehicle.Type)
            {
                case VehicleType.SportCar:
                    return AddVehicle(rallyId, vehicle.Id, vehicle.TeamName, vehicle.Model, vehicle.ManufacturingDate, 
                        _vehicleFactory.CreateSportCar);
                case VehicleType.TerrainCar:
                    return AddVehicle(rallyId, vehicle.Id, vehicle.TeamName, vehicle.Model, vehicle.ManufacturingDate, 
                        _vehicleFactory.CreateTerrainCar);
                case VehicleType.Truck:
                    return AddVehicle(rallyId, vehicle.Id, vehicle.TeamName, vehicle.Model, vehicle.ManufacturingDate, 
                        _vehicleFactory.CreateTruck);
                case VehicleType.SportMotorcycle:
                    return AddVehicle(rallyId, vehicle.Id, vehicle.TeamName, vehicle.Model, vehicle.ManufacturingDate, 
                        _vehicleFactory.CreateSportMotorcycle);
                case VehicleType.CrossMotorcycle:
                    return AddVehicle(rallyId, vehicle.Id, vehicle.TeamName, vehicle.Model, vehicle.ManufacturingDate, 
                        _vehicleFactory.CreateCrossMotorcycle);
                default:
                    throw new InvalidEnumArgumentException("Vehicle type not supported.");
            }
        }

        private Result AddVehicle(string rallyId, string vehicleId, string teamName, string model, DateTime manufacturingDate, Func<string, string, string, DateTime, IAmVehicle> createVehicle)
        {
            return _rellyRepo.Find(rallyId)
                .OnFailureCompensate(failure => Result.Fail<IAmRally>(ErrorMessages.RallyNotFound))
                .OnSuccess(rally => rally.AddVehicle(createVehicle(vehicleId, teamName, model, manufacturingDate)));
        }
    }
}
