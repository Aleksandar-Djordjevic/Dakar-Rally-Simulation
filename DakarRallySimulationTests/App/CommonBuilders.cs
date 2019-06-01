using System;
using System.Collections.Generic;
using System.Text;
using CSharpFunctionalExtensions;
using DakarRallySimulation;
using DakarRallySimulation.App;
using DakarRallySimulation.Domain;
using Moq;

namespace DakarRallySimulationTests.App
{
    public class CommonBuilders
    {
        public static Mock<IAmRallyRepository> SetUpRepoWithNoRally(string rallyId)
        {
            var rallyRepoMock = new Mock<IAmRallyRepository>();
            rallyRepoMock
                .Setup(repo => repo.Find(rallyId))
                .Returns(Result.Fail<IAmRally>("Not Found"));

            return rallyRepoMock;
        }

        public static IAmRallyRepository SetUpRepoWithRally(string rallyId, IAmRally rally)
        {
            var rallyRepoMock = new Mock<IAmRallyRepository>();
            rallyRepoMock
                .Setup(repo => repo.Find(rallyId))
                .Returns(Result.Ok<IAmRally>(rally));
            return rallyRepoMock.Object;
        }

        public static IAmRally GetRallyThatRemovesVehicle(string vehicleId)
        {
            var rallyMock = new Mock<IAmRally>();
            rallyMock.Setup(rally => rally.RemoveVehicle(vehicleId))
                .Returns(Result.Ok);
            return rallyMock.Object;
        }

        public static IAmRally GetRallyThatDoesNotRemoveVehicle(string vehicleId, string forReason)
        {
            var rallyMock = new Mock<IAmRally>();
            rallyMock.Setup(rally => rally.RemoveVehicle(vehicleId))
                .Returns(Result.Fail(forReason));
            return rallyMock.Object;
        }

        public static IAmRally GetRallyThatAcceptsVehicle(string vehicleId)
        {
            var rallyMock = new Mock<IAmRally>();
            rallyMock.Setup(rally => rally.AddVehicle(It.Is<IAmVehicle>(vehicle => vehicle.Id == vehicleId)))
                .Returns(Result.Ok);
            return rallyMock.Object;
        }

        public static IAmRally GetRallyThatDoesNotAcceptsVehicle(string vehicleId, string forReason)
        {
            var rallyMock = new Mock<IAmRally>();
            rallyMock.Setup(rally => rally.AddVehicle(It.Is<IAmVehicle>(vehicle => vehicle.Id == vehicleId)))
                .Returns(Result.Fail(forReason));
            return rallyMock.Object;
        }


        public static IAmRally GetRallyThatSuccessfullyStarts()
        {
            var rallyMock = new Mock<IAmRally>();
            rallyMock.Setup(rally => rally.Start())
                .Returns(Result.Ok());
            return rallyMock.Object;
        }

        public static IAmRally GetRallyThatUnsuccessfullyStarts(string forReason)
        {
            var rallyMock = new Mock<IAmRally>();
            rallyMock.Setup(rally => rally.Start())
                .Returns(Result.Fail(forReason));
            return rallyMock.Object;
        }

        public static IAmRally GetRallyThatDoesNotHaveVehicle(string vehicleId)
        {
            var rallyMock = new Mock<IAmRally>();
            rallyMock.Setup(rally => rally.Vehicles)
                .Returns(new Dictionary<string, IAmVehicle>());
            return rallyMock.Object;
        }

        public static IAmRally GetRallyThatHasVehicle(string vehicleId, FakeVehicle vehicle)
        {
            var vehicles = new Dictionary<string, IAmVehicle>();
            vehicles.Add(vehicle.Id, vehicle);
            var rallyMock = new Mock<IAmRally>();
            rallyMock.Setup(rally => rally.Vehicles)
                .Returns(vehicles);
            return rallyMock.Object;
        }
    }
}
