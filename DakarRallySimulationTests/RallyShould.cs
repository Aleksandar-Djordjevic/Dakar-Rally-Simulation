using System;
using DakarRallySimulation;
using Xunit;

namespace DakarRallySimulationTests
{
    public class RallyShould
    {
        [Fact]
        public void SucceedToAddNewVehicleWhenRallyHasNotStarted()
        {
            var rally = new Rally(2019, 2);
            var vehicle = AVehicleBuilder.BuildProperlyWorkingVehicle();

            var operationResult = rally.AddVehicle(vehicle);

            Assert.True(operationResult.IsSuccess);
            Assert.True(rally.Vehicles.Count == 1);
        }

        [Fact]
        public void FailToAddAlreadyAddedVehicleWhenRallyHasNotStarted()
        {
            var rally = new Rally(2019, 2);
            var vehicle = AVehicleBuilder.BuildProperlyWorkingVehicle();

            rally.AddVehicle(vehicle);
            var operationResult = rally.AddVehicle(vehicle);

            Assert.False(operationResult.IsSuccess);
        }

        [Fact]
        public void SucceedToRemoveAddedVehicleWhenRallyHasNotStarted()
        {
            var rally = new Rally(2019, 2);
            var vehicle = AVehicleBuilder.BuildProperlyWorkingVehicle();

            rally.AddVehicle(vehicle);
            var operationResult = rally.RemoveVehicle(vehicle.Id);

            Assert.True(operationResult.IsSuccess);
            Assert.True(rally.Vehicles.Count == 0);
        }

        [Fact]
        public void FailToRemoveUnexistingVehicleWhenRallyHasNotStarted()
        {
            var rally = new Rally(2019, 2);
            var operationResult = rally.RemoveVehicle("id 1");

            Assert.False(operationResult.IsSuccess);
        }

        [Fact]
        public void SucceedToStartWhenItHasNotBeenStarted()
        {
            var rally = new Rally(2019, 2);

            var operationResult = rally.Start();

            Assert.True(operationResult.IsSuccess);
        }

        [Fact]
        public void FailToStartWhenItHasBeenStarted()
        {
            var rally = new Rally(2019, 2);

            rally.Start();
            var operationResult = rally.Start();

            Assert.False(operationResult.IsSuccess);
        }

        [Fact]
        public void Finish()
        {
            var rally = new Rally(2019, 2);
            var vehicle = AVehicleBuilder.BuildProperlyWorkingVehicle();
            rally.AddVehicle(vehicle);

            var operationResult = rally.Start();

            Assert.True(operationResult.IsSuccess);
        }

        private VehicleBuilder AVehicleBuilder => new VehicleBuilder();
    }
}
