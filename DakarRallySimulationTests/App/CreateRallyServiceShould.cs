using CSharpFunctionalExtensions;
using DakarRallySimulation.App;
using DakarRallySimulation.App.CreateRally;
using DakarRallySimulation.Domain;
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
                .Returns(Result.Fail<IAmRally>("Not found"));
            repoMock.Setup(repo => repo.Add(It.IsAny<IAmRally>()))
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
