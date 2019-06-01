using System;
using System.Collections.Generic;
using System.Text;
using DakarRallySimulation;
using DakarRallySimulationApp;
using Xunit;

namespace DakarRallySimulationTests
{
    public class RallyRepositoryShould
    {
        private int _finishLineDistance = 1000;

        [Fact]
        public void AddRallyWhenItDoesNotExist()
        {
            var repo = new RallyRepository();

            var result = repo.Add(new Rally(2019, _finishLineDistance));

            Assert.True(result.IsSuccess);
        }

        [Fact]
        public void NotAddRallWhenItExists()
        {
            var repo = new RallyRepository();

            repo.Add(new Rally(2019, _finishLineDistance));
            var result = repo.Add(new Rally(2019, _finishLineDistance));

            Assert.True(result.IsFailure);
        }

        [Fact]
        public void FindRallyWhenItExists()
        {
            var repo = new RallyRepository();
            repo.Add(new Rally(2019, _finishLineDistance));

            var result = repo.Find("2019");

            Assert.True(result.IsSuccess);
        }

        [Fact]
        public void NotFindRallyWhenItDoesNotExist()
        {
            var repo = new RallyRepository();

            var result = repo.Find("2019");

            Assert.True(result.IsFailure);
        }

    }
}
