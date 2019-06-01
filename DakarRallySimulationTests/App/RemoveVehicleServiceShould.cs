using System;
using System.Collections.Generic;
using System.Text;
using CSharpFunctionalExtensions;
using DakarRallySimulation;
using DakarRallySimulationApp;
using DakarRallySimulationApp.AddVehicleToRally;
using DakarRallySimulationApp.RemoveVehicleFromRally;
using Moq;
using Xunit;

namespace DakarRallySimulationTests.App
{
    public class RemoveVehicleServiceShould
    {
        [Fact]
        public void FailWhenRallyDoesNotExist()
        {
            var rallyId = "rally1";
            var service = new RemoveVehicleService(CommonBuilders.SetUpRepoWithNoRally(rallyId).Object);

            var result = service.RemoveVehicleFromRally("rally1", "vehicle1");

            Assert.True(result.IsFailure);
            Assert.Equal(ErrorMessages.RallyNotFound, result.Error);
        }

        [Fact]
        public void ReturnOkWhenRallyExistsAndRemovesVehicle()
        {
            var rallyId = "rally1";
            var vehicleId = "vehicle1";

            var rallyRepo = CommonBuilders.SetUpRepoWithRally(
                rallyId,
                CommonBuilders.GetRallyThatRemovesVehicle(vehicleId));
            var service = new RemoveVehicleService(rallyRepo);

            var result = service.RemoveVehicleFromRally(rallyId, vehicleId);

            Assert.True(result.IsSuccess);
        }

        [Fact]
        public void ReturnFailWhenRallyExistsAndDoesNotRemoveVehicle()
        {
            var rallyId = "rally1";
            var vehicleId = "vehicle1";
            var forReason = "Rally has already started.";
            var rallyRepo = CommonBuilders.SetUpRepoWithRally(
                rallyId,
                CommonBuilders.GetRallyThatDoesNotRemoveVehicle(vehicleId, forReason));
            var service = new RemoveVehicleService(rallyRepo);

            var result = service.RemoveVehicleFromRally(rallyId, vehicleId);

            Assert.True(result.IsFailure);
            Assert.Equal(forReason, result.Error);
        }

        
    }
}
