using System;
using System.Threading.Tasks;
using DakarRallySimulation;
using DakarRallySimulation.Domain;
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
        public void FailToStartWhenItHasNotBeenStartedYetButDoesNotHaveVehicles()
        {
            var rally = new Rally(2019, 2);

            var operationResult = rally.Start();

            Assert.True(operationResult.IsFailure);
            Assert.Equal(ErrorMessages.CannotStartRallyWithNoVehicles, operationResult.Error);
        }

        [Fact]
        public void SucceedToStartWhenItHasNotBeenStartedYetAndHasVehicles()
        {
            var rally = new Rally(2019, 2);
            var vehicle = new VehicleThatImmediatelyFinishes("Id1");
            rally.AddVehicle(vehicle);

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
        public void FinishWhenAllVehiclesFinish()
        {
            var rally = new Rally(2019, 2);
            var vehicle = new VehicleThatImmediatelyFinishes("Id1");
            rally.AddVehicle(vehicle);

            rally.Start();
            
            Assert.True(rally.IsFinished);
        }

        [Fact]
        public void NotFinishWhenSomeVehicleNeverFinishes()
        {
            var rally = new Rally(2019, 2);
            rally.AddVehicle(new VehicleThatImmediatelyFinishes("Id1"));
            rally.AddVehicle(new VehicleThatNeverFinishes("Id2"));

            rally.Start();

            Assert.False(rally.IsFinished);
        }

        private VehicleBuilder AVehicleBuilder => new VehicleBuilder();

        private abstract class VehicleStub : IAmVehicle
        {
            public event EventHandler FinishedRally;
            public event EventHandler Moved;
            public string Id { get; }
            public decimal Distance { get; }
            public DateTime? FinishedAt { get; }

            public VehicleStub(string id)
            {
                Id = id;
            }

            public abstract void StartRally(Rally rally);

            protected void FinishRally()
            {
                FinishedRally?.Invoke(this, EventArgs.Empty);
            }

            public int CompareTo(object obj)
            {
                throw new NotImplementedException();
            }
        }

        private class VehicleThatImmediatelyFinishes : VehicleStub
        {
            public VehicleThatImmediatelyFinishes(string id) : base(id) {}

            public override void StartRally(Rally rally)
            {
                FinishRally();
            }
        }

        private class VehicleThatNeverFinishes : VehicleStub
        {
            public VehicleThatNeverFinishes(string id) : base(id) { }

            public override void StartRally(Rally rally) {}
        }
    }
}
