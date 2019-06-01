using System;
using System.Collections.Generic;
using System.Text;
using CSharpFunctionalExtensions;
using DakarRallySimulation;
using DakarRallySimulation.App.AddVehicleToRally;
using DakarRallySimulation.Domain;
using Moq;
using Xunit;
using ErrorMessages = DakarRallySimulation.App.ErrorMessages;

namespace DakarRallySimulationTests.App
{
    public partial class AddVehicleServiceShould
    {
        [Fact]
        public void FailWhenRallyDoesNotExist()
        {
            var rallyId = "rally1";
            var vehicleId = "vehicle1";
            var service = new AddVehicleService(CommonBuilders.SetUpRepoWithNoRally(rallyId).Object, new FakeVehicleBuilder());

            var result = service.AddSportCar(rallyId, vehicleId, "BMW Racing", "BMW 3M", DateTime.UtcNow);

            Assert.True(result.IsFailure);
            Assert.Equal(ErrorMessages.RallyNotFound, result.Error);
        }

        [Fact]
        public void ReturnOkWhenRallyExistsAndAcceptsVehicle()
        {
            var rallyId = "2019";
            var vehicleId = "vehicle1";
            var vehicleFactory = new FakeVehicleBuilder();
            var rallyRepo = CommonBuilders.SetUpRepoWithRally(
                rallyId,
                CommonBuilders.GetRallyThatAcceptsVehicle(vehicleId));
            var service = new AddVehicleService(rallyRepo, vehicleFactory);

            var result = service.AddSportCar(rallyId, vehicleId, "BMW Racing", "BMW 3M", DateTime.UtcNow);

            Assert.True(vehicleFactory.VehicleRequested);
            Assert.True(result.IsSuccess);
        }

        [Fact]
        public void ReturnFailWhenRallyExistsAndDoesNotAcceptVehicle()
        {
            var rallyId = "rally1";
            var vehicleId = "vehicle1";
            var forReason = "Rally has already started.";
            var vehicleFactory = new FakeVehicleBuilder();
            var rallyRepo = CommonBuilders.SetUpRepoWithRally(
                rallyId,
                CommonBuilders.GetRallyThatDoesNotAcceptsVehicle(vehicleId, forReason));
            var service = new AddVehicleService(rallyRepo, vehicleFactory);

            var result = service.AddSportCar(rallyId, vehicleId, "BMW Racing", "BMW 3M", DateTime.UtcNow);

            Assert.True(vehicleFactory.VehicleRequested);
            Assert.True(result.IsFailure);
            Assert.Equal(forReason, result.Error);
        }

        private class FakeVehicleBuilder : ICreateVehicle
        {
            public bool VehicleRequested = false;
            public IAmVehicle CreateCrossMotorcycle(string id, string teamName, string model, DateTime manufacturingDate)
            {
                VehicleRequested = true;
                return new FakeVehicle(id);
            }

            public IAmVehicle CreateSportCar(string id, string teamName, string model, DateTime manufacturingDate)
            {
                VehicleRequested = true;
                return new FakeVehicle(id);
            }

            public IAmVehicle CreateSportMotorcycle(string id, string teamName, string model, DateTime manufacturingDate)
            {
                VehicleRequested = true;
                return new FakeVehicle(id);
            }

            public IAmVehicle CreateTerrainCar(string id, string teamName, string model, DateTime manufacturingDate)
            {
                VehicleRequested = true;
                return new FakeVehicle(id);
            }

            public IAmVehicle CreateTruck(string id, string teamName, string model, DateTime manufacturingDate)
            {
                VehicleRequested = true;
                return new FakeVehicle(id);
            }
        }
    }
}
