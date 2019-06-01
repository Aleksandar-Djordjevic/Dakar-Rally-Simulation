using System;
using System.Collections.Generic;
using System.Text;
using DakarRallySimulation.App;
using DakarRallySimulation.App.GetVehicleStatistics;
using Xunit;

namespace DakarRallySimulationTests.App
{
    public class VehicleStatisticsServiceShould
    {
        [Fact]
        public void FailWhenRallyDoesNotExist()
        {
            var rallyId = "2019";
            var vehicleId = "vehicle1";
            var service = new VehicleStatisticsService(CommonBuilders.SetUpRepoWithNoRally(rallyId).Object);

            var result = service.GetVehicleStatistics(rallyId, vehicleId);

            Assert.True(result.IsFailure);
            Assert.Equal(ErrorMessages.RallyNotFound, result.Error);
        }

        [Fact]
        public void FailWhenVehicleDoesNotExist()
        {
            var rallyId = "2019";
            var vehicleId = "vehicle1";
            var rallyRepo = CommonBuilders.SetUpRepoWithRally(
                rallyId,
                CommonBuilders.GetRallyThatDoesNotHaveVehicle(vehicleId));
            var service = new VehicleStatisticsService(rallyRepo);

            var result = service.GetVehicleStatistics(rallyId, vehicleId);

            Assert.True(result.IsFailure);
            Assert.Equal(ErrorMessages.VehicleDoesNotExist, result.Error);
        }

        [Fact]
        public void GetStatisticsWhenVehicleExists()
        {
            var rallyId = "2019";
            var vehicleId = "vehicle1";
            var rallyRepo = CommonBuilders.SetUpRepoWithRally(
                rallyId,
                CommonBuilders.GetRallyThatHasVehicle(vehicleId, new FakeVehicle(vehicleId)));
            var service = new VehicleStatisticsService(rallyRepo);

            var result = service.GetVehicleStatistics(rallyId, vehicleId);

            Assert.True(result.IsSuccess);
        }
    }
}
