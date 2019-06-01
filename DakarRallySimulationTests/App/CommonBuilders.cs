using System;
using System.Collections.Generic;
using System.Text;
using CSharpFunctionalExtensions;
using DakarRallySimulation;
using DakarRallySimulationApp;
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

        public static IAmRally GetRallyThatDoesNotRemoveVehicle(string vehicleId, string reason)
        {
            var rallyMock = new Mock<IAmRally>();
            rallyMock.Setup(rally => rally.RemoveVehicle(vehicleId))
                .Returns(Result.Fail(reason));
            return rallyMock.Object;
        }

        public static IAmRally GetRallyThatAcceptsVehicles(string vehicleId)
        {
            var rallyMock = new Mock<IAmRally>();
            rallyMock.Setup(rally => rally.AddVehicle(It.Is<IAmVehicle>(vehicle => vehicle.Id == vehicleId)))
                .Returns(Result.Ok);
            return rallyMock.Object;
        }

        public static IAmRally GetRallyThatDoesNotAcceptsVehicles(string vehicleId, string reason)
        {
            var rallyMock = new Mock<IAmRally>();
            rallyMock.Setup(rally => rally.AddVehicle(It.Is<IAmVehicle>(vehicle => vehicle.Id == vehicleId)))
                .Returns(Result.Fail(reason));
            return rallyMock.Object;
        }


    }
}
