using System;
using System.Collections.Generic;
using System.Text;
using CSharpFunctionalExtensions;
using DakarRallySimulation;
using DakarRallySimulationApp;
using DakarRallySimulationApp.CreateRally;
using Moq;
using Xunit;

namespace DakarRallySimulationTests
{
    public class CreateRallyServiceShould
    {
        [Fact]
        public void CreateRallyWhenItDoesNotExist()
        {
            var repoMock = new Mock<IAmRallyRepository>();
            repoMock.Setup(repo => repo.Find(It.IsAny<string>()))
                .Returns(Result.Fail<Rally>("Not found"));
            repoMock.Setup(repo => repo.Add(It.IsAny<Rally>()))
                .Returns(Result.Ok);
            var service = new CreateRallyService(repoMock.Object);

            var result = service.CreateRally(2019);

            Assert.True(result.IsSuccess);
        }

        [Fact]
        public void NotCreateRallyWhenItExists()
        {
            var repoMock = new Mock<IAmRallyRepository>();
            repoMock.Setup(repo => repo.Exists(It.IsAny<string>()))
                .Returns(true);
            var service = new CreateRallyService(repoMock.Object);

            var result = service.CreateRally(2019);

            Assert.True(result.IsFailure);
        }

    }
}
