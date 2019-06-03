using System;
using DakarRallySimulation.App.AddVehicleToRally;
using DakarRallySimulation.Domain;
using Moq;
using Xunit;
using ErrorMessages = DakarRallySimulation.App.ErrorMessages;
using Vehicle = DakarRallySimulation.App.AddVehicleToRally.Vehicle;
using VehicleType = DakarRallySimulation.App.AddVehicleToRally.VehicleType;

namespace DakarRallySimulation.Tests.App
{
    public partial class AddVehicleServiceShould
    {
        [Fact]
        public void FailWhenRallyDoesNotExist()
        {
            var rallyId = "rally1";
            var vehicle = new Vehicle
            {
                Id = "v1",
                Type = VehicleType.SportCar
            };
            var vehicleFactoryMock = GetVehicleFactoryWhichReturns(vehicle.Id);
            var service = new AddVehicleService(CommonBuilders.SetUpRepoWithNoRally(rallyId).Object, vehicleFactoryMock.Object);

            var result = service.AddVehicle(rallyId, vehicle);

            Assert.True(result.IsFailure);
            Assert.Equal(ErrorMessages.RallyNotFound, result.Error);
        }

        [Fact]
        public void ReturnOkWhenRallyExistsAndAcceptsVehicle()
        {
            var rallyId = "2019";
            var vehicle = new Vehicle
            {
                Id = "v1",
                Type = VehicleType.SportCar
            };
            var vehicleFactoryMock = GetVehicleFactoryWhichReturns(vehicle.Id);
            var rallyRepo = CommonBuilders.SetUpRepoWithRally(
                rallyId, CommonBuilders.GetRallyThatAcceptsVehicle(vehicle.Id));
            var service = new AddVehicleService(rallyRepo, vehicleFactoryMock.Object);

            var result = service.AddVehicle(rallyId, vehicle);

            Assert.True(result.IsSuccess);
            vehicleFactoryMock.Verify(factory => factory.CreateSportCar(vehicle.Id, It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DateTime>()));
        }

        [Fact]
        public void CallApproAppropriateMethodOnFactoryDependingOnVehicleType()
        {
            var rallyId = "2019";
            var vehicle = new Vehicle
            {
                Id = "v1",
                Type = VehicleType.SportCar
            };
            var vehicleFactoryMock = GetVehicleFactoryWhichReturns(vehicle.Id);
            var rallyRepo = CommonBuilders.SetUpRepoWithRally(
                rallyId, CommonBuilders.GetRallyThatAcceptsVehicle(vehicle.Id));
            var service = new AddVehicleService(rallyRepo, vehicleFactoryMock.Object);

            var result = service.AddVehicle(rallyId, vehicle);

            vehicleFactoryMock.Verify(factory => factory.CreateSportCar(vehicle.Id, It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DateTime>()));
        }

        [Fact]
        public void ReturnFailWhenRallyExistsAndDoesNotAcceptVehicle()
        {
            var forReason = "Rally has already started.";
            var rallyId = "2019";
            var vehicle = new Vehicle
            {
                Id = "v1",
                Type = VehicleType.SportCar
            };
            var vehicleFactoryMock = GetVehicleFactoryWhichReturns(vehicle.Id);
            var rallyRepo = CommonBuilders.SetUpRepoWithRally(
                rallyId, CommonBuilders.GetRallyThatDoesNotAcceptsVehicle(vehicle.Id, forReason));
            var service = new AddVehicleService(rallyRepo, vehicleFactoryMock.Object);

            var result = service.AddVehicle(rallyId, vehicle);

            Assert.False(result.IsSuccess);
            vehicleFactoryMock.Verify(factory => factory.CreateSportCar(vehicle.Id, It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DateTime>()));
            Assert.Equal(forReason, result.Error);
        }

        private Mock<ICreateVehicle> GetVehicleFactoryWhichReturns(string vehicleId)
        {
            var vehicle = new FakeVehicle(vehicleId);

            var mock = new Mock<ICreateVehicle>();
            mock.Setup(factory => factory.CreateSportCar(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DateTime>()))
                .Returns(vehicle);
            mock.Setup(factory => factory.CreateTerrainCar(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DateTime>()))
                .Returns(vehicle);
            mock.Setup(factory => factory.CreateTruck(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DateTime>()))
                .Returns(vehicle);
            mock.Setup(factory => factory.CreateSportMotorcycle(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DateTime>()))
                .Returns(vehicle);
            mock.Setup(factory => factory.CreateCrossMotorcycle(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DateTime>()))
                .Returns(vehicle);

            return mock;
        }
    }
}
