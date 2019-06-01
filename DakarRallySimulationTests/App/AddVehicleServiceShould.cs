using System;
using System.Collections.Generic;
using System.Text;
using CSharpFunctionalExtensions;
using DakarRallySimulation;
using DakarRallySimulationApp;
using DakarRallySimulationApp.AddVehicleToRally;
using Moq;
using Xunit;

namespace DakarRallySimulationTests.App
{
    public class AddVehicleServiceShould
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
                CommonBuilders.GetRallyThatAcceptsVehicles(vehicleId));
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
                CommonBuilders.GetRallyThatDoesNotAcceptsVehicles(vehicleId, forReason));
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

        private class FakeVehicle : IAmVehicle
        {
            public FakeVehicle(string vehicleId)
            {
                Id = vehicleId;
            }
            public int CompareTo(object obj)
            {
                return 1;
            }

            public event EventHandler FinishedRally;
            public event EventHandler Moved;
            public string Id { get; }
            public decimal Distance { get; }
            public DateTime? FinishedAt { get; }
            public void StartRally(Rally rally)
            {}
        }
    }
}
