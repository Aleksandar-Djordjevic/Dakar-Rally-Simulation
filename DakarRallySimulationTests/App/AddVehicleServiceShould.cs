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
            
            var service = new AddVehicleService(SetUpRepoWithNoRally().Object, new FakeVehicleBuilder());

            var result = service.AddSportCar("rally1", "vehicle1", "BMW Racing", "BMW 3M", DateTime.UtcNow);

            Assert.True(result.IsFailure);
            Assert.Equal(ErrorMessages.RallyNotFound, result.Error);
        }

        [Fact]
        public void ReturnOkWhenRallyExistsAndAcceptsVehicle()
        {
            var vehicleFactory = new FakeVehicleBuilder();
            var rallyRepo = SetUpRepoWithRally(GetRallyThatAcceptsVehicles());
            var service = new AddVehicleService(rallyRepo, vehicleFactory);

            var result = service.AddSportCar("2019", "vehicle1", "BMW Racing", "BMW 3M", DateTime.UtcNow);

            Assert.True(vehicleFactory.VehicleRequested);
            Assert.True(result.IsSuccess);
        }

        [Fact]
        public void ReturnFailWhenRallyExistsAndDoesNotAcceptVehicle()
        {
            var forReason = "Rally has already started.";
            var vehicleFactory = new FakeVehicleBuilder();
            var rallyRepo = SetUpRepoWithRally(GetRallyThatDoesNotAcceptsVehicles(forReason));
            var service = new AddVehicleService(rallyRepo, vehicleFactory);

            var result = service.AddSportCar("2019", "vehicle1", "BMW Racing", "BMW 3M", DateTime.UtcNow);

            Assert.True(vehicleFactory.VehicleRequested);
            Assert.True(result.IsFailure);
            Assert.Equal(forReason, result.Error);
        }

        private Mock<IAmRallyRepository> SetUpRepoWithNoRally()
        {
            var rallyRepoMock = new Mock<IAmRallyRepository>();
            rallyRepoMock
                .Setup(repo => repo.Find(It.IsAny<string>()))
                .Returns(Result.Fail<IAmRally>("Not Found"));

            return rallyRepoMock;
        }

        private Mock<IAmRallyRepository> SetUpRepoWithRally(int year)
        {
            var rallyRepoMock = new Mock<IAmRallyRepository>();
            rallyRepoMock
                .Setup(repo => repo.Find(year.ToString()))
                .Returns(Result.Ok<IAmRally>(new Rally(year, 100)));

            return rallyRepoMock;
        }

        private IAmRallyRepository SetUpRepoWithRally(IAmRally rally)
        {
            var rallyRepoMock = new Mock<IAmRallyRepository>();
            rallyRepoMock
                .Setup(repo => repo.Find(It.IsAny<string>()))
                .Returns(Result.Ok<IAmRally>(rally));
            return rallyRepoMock.Object;
        }

        private IAmRally GetRallyThatAcceptsVehicles()
        {
            var rallyMock = new Mock<IAmRally>();
            rallyMock.Setup(rally => rally.AddVehicle(It.IsAny<IAmVehicle>()))
                .Returns(Result.Ok);
            return rallyMock.Object;
        }

        private IAmRally GetRallyThatDoesNotAcceptsVehicles(string reason)
        {
            var rallyMock = new Mock<IAmRally>();
            rallyMock.Setup(rally => rally.AddVehicle(It.IsAny<IAmVehicle>()))
                .Returns(Result.Fail(reason));
            return rallyMock.Object;
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
            public FakeVehicle()
            {
                
            }
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
