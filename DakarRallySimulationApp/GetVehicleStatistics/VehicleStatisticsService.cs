﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using DakarRallySimulation;

namespace DakarRallySimulationApp.GetVehicleStatistics
{
    public class VehicleStatisticsService : IProvideVehicleStatistics
    {
        private readonly IAmRallyRepository _rellyRepo;

        public VehicleStatisticsService(IAmRallyRepository rellyRepo)
        {
            _rellyRepo = rellyRepo;
        }

        public Result<VehicleStatistics> GetVehicleStatistics(string rallyId, string vehicleId)
        {
            return _rellyRepo.Find(rallyId)
                .OnFailureCompensate(failure => Result.Fail<IAmRally>(ErrorMessages.RallyNotFound))
                .OnSuccess(rally =>
                    Result.Create(rally.Vehicles.TryGetValue(vehicleId, out var v), v, ErrorMessages.VehicleDoesNotExist)
                    .Map(GetStatistics));
        }

        private VehicleStatistics GetStatistics(IAmVehicle vehicle)
        {
            return new VehicleStatistics();
        }
    }
}
