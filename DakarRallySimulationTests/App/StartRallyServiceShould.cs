using DakarRallySimulation.App;
using DakarRallySimulation.App.StartRally;
using Xunit;

namespace DakarRallySimulation.Tests.App
{
    public class StartRallyServiceShould
    {
        [Fact]
        public void FailWhenRallyDoesNotExist()
        {
            var rallyId = "rally1";
            var service = new StartRallyService(CommonBuilders.SetUpRepoWithNoRally(rallyId).Object);

            var result = service.StartRally(rallyId);

            Assert.True(result.IsFailure);
            Assert.Equal(ErrorMessages.RallyNotFound, result.Error);
        }

        [Fact]
        public void ReturnOkWhenRallyExistsAndSuccessfullyStarts()
        {
            var rallyId = "rally1";
            var rallyRepo = CommonBuilders.SetUpRepoWithRally(
                rallyId,
                CommonBuilders.GetRallyThatSuccessfullyStarts());
            var service = new StartRallyService(rallyRepo);

            var result = service.StartRally(rallyId);

            Assert.True(result.IsSuccess);
        }

        [Fact]
        public void ReturnFailWhenRallyExistsAndUnsuccessfullyStarts()
        {
            var rallyId = "rally1";
            var forReason = "some reason";
            var rallyRepo = CommonBuilders.SetUpRepoWithRally(
                rallyId,
                CommonBuilders.GetRallyThatUnsuccessfullyStarts(forReason));
            var service = new StartRallyService(rallyRepo);

            var result = service.StartRally(rallyId);

            Assert.True(result.IsFailure);
            Assert.Equal(forReason, result.Error);
        }
    }


}
