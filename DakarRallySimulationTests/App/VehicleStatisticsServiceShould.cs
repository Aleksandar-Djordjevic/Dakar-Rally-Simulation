using System.Collections.Generic;
using DakarRallySimulation.App.GetVehicleStatistics;
using DakarRallySimulation.Domain.Vehicle;
using Xunit;
using ErrorMessages = DakarRallySimulation.App.ErrorMessages;
using Malfunction = DakarRallySimulation.App.GetVehicleStatistics.Malfunction;
using VehicleStatistics = DakarRallySimulation.App.GetVehicleStatistics.VehicleStatistics;

namespace DakarRallySimulation.Tests.App
{
    public class VehicleStatisticsServiceShould
    {
        [Fact]
        public void FailWhenRallyDoesNotExist()
        {
            var rallyId = "2019";
            var vehicleId = "vehicle1";
            var service = new VehicleStatisticsService(
                CommonBuilders.SetUpRepoWithNoRally(rallyId).Object,
                new FakeVehicleStatisticsFactory(GetExpectedStats()));

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
            var service = new VehicleStatisticsService(rallyRepo, new FakeVehicleStatisticsFactory(GetExpectedStats()));

            var result = service.GetVehicleStatistics(rallyId, vehicleId);

            Assert.True(result.IsFailure);
            Assert.Equal(ErrorMessages.VehicleDoesNotExist, result.Error);
        }

        [Fact]
        public void GetStatisticsWhenVehicleExists()
        {
            var rallyId = "2019";
            var vehicleId = "vehicle1";
            var fakeVehicle = new FakeVehicle(vehicleId);
            var expectedStats = GetExpectedStats();
            var rallyRepo = CommonBuilders.SetUpRepoWithRally(
                rallyId,
                CommonBuilders.GetRallyThatHasVehicle(vehicleId, fakeVehicle));
            var service = new VehicleStatisticsService(rallyRepo, new FakeVehicleStatisticsFactory(expectedStats));

            var result = service.GetVehicleStatistics(rallyId, vehicleId);

            Assert.True(result.IsSuccess);
            Assert.Equal(expectedStats, result.Value);
        }

        private VehicleStatistics GetExpectedStats()
        {
            return new VehicleStatistics
            {
                DistanceFromStart = 5,
                MalfunctionStatistics = new List<Malfunction>()
            };
        }

        private class FakeVehicleStatisticsFactory : ICreateVehicleStatistics
        {
            private readonly VehicleStatistics _stats;
            public FakeVehicleStatisticsFactory(VehicleStatistics stats)
            {
                _stats = stats;
            }

            public VehicleStatistics Create(IAmVehicle vehicle)
            {
                return _stats;
            }
        }
    }
}
