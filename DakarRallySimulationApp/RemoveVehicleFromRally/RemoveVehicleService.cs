using System;
using System.Collections.Generic;
using System.Text;
using CSharpFunctionalExtensions;
using DakarRallySimulation;

namespace DakarRallySimulationApp.RemoveVehicleFromRally
{
    public class RemoveVehicleService : IRemoveVehicleFromRally
    {
        private readonly IAmRallyRepository _rellyRepo;

        public RemoveVehicleService(IAmRallyRepository rellyRepo)
        {
            _rellyRepo = rellyRepo;
        }

        public Result RemoveVehicleFromRally(string rallyId, string vehicleId)
        {
            return _rellyRepo.Find(rallyId)
                .OnFailureCompensate(failure => Result.Fail<IAmRally>(ErrorMessages.RallyNotFound))
                .OnSuccess(rally => rally.RemoveVehicle(vehicleId));
        }
    }
}
