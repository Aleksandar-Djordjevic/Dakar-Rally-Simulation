using System;
using System.Threading.Tasks;
using DakarRallySimulation.Domain;
using Xunit;

namespace DakarRallySimulation.Tests.Domain
{
    public class VehicleShould
    {
        [Fact]
        public void VehicleCannotStartRallyTwice()
        {
            var rally = BuildRally(2);
            var vehicle = AVehicleBuilder.BuildProperlyWorkingVehicle();

            vehicle.StartRally(rally);
            Action startingRallySecondTime = () => vehicle.StartRally(rally);

            Assert.Throws<InvalidOperationException>(startingRallySecondTime);
        }

        [Fact]
        public async Task PassFinishLineIfWorkingProperly()
        {
            var rally = BuildRally(2);
            var vehicle = AVehicleBuilder.BuildProperlyWorkingVehicle();

            vehicle.StartRally(rally);
            await Task.Delay(TimeSpan.FromSeconds(30));
            var vehicleStatistics = vehicle.GetStatistics();

            Assert.Equal(VehicleState.Finished, vehicleStatistics.Status);
            Assert.NotNull(vehicleStatistics.FinishTime);
            Assert.True(rally.Distance <= vehicleStatistics.DistanceFromStart);
        }

        [Fact]
        public async Task PassFinishLineIfLightlyMalfunctioning()
        {
            var rally = BuildRally(2);
            var vehicle = AVehicleBuilder.BuildLightlyMalfunctioningVehicle();

            vehicle.StartRally(rally);
            await Task.Delay(TimeSpan.FromSeconds(30));
            var vehicleStatistics = vehicle.GetStatistics();

            Assert.Equal(VehicleState.Finished, vehicleStatistics.Status);
            Assert.NotNull(vehicleStatistics.FinishTime);
            Assert.True(rally.Distance <= vehicleStatistics.DistanceFromStart);
        }

        [Fact]
        public async Task NotPassFinishLineIfHeavilyMalfunctioning()
        {
            var rally = BuildRally(2);
            var vehicle = AVehicleBuilder.BuildHeavilyMalfunctioningVehicle();

            vehicle.StartRally(rally);
            await Task.Delay(TimeSpan.FromSeconds(30));
            var vehicleStatistics = vehicle.GetStatistics();

            Assert.Equal(VehicleState.Broken, vehicleStatistics.Status);
            Assert.Null(vehicleStatistics.FinishTime);
            Assert.True(rally.Distance > vehicleStatistics.DistanceFromStart);
        }

        [Fact]
        public async Task IncludeAllMalfunctioningInStatistics()
        {
            var rally = BuildRally(2);
            var vehicle = AVehicleBuilder.BuildVehicleWhichLightlyMalfunctionsInBeginning();

            vehicle.StartRally(rally);
            await Task.Delay(TimeSpan.FromSeconds(30));
            var vehicleStatistics = vehicle.GetStatistics();

            Assert.Equal(2, vehicleStatistics.Malfunctions.Count);
        }

        [Fact]
        public async Task BeAbleToCaptureRepairingStatus()
        {
            var rally = BuildRally(2);
            var vehicle = AVehicleBuilder.BuildLightlyMalfunctioningVehicleWithLongRapairmentDuration();

            vehicle.StartRally(rally);
            await Task.Delay(TimeSpan.FromSeconds(5));
            var vehicleStatistics = vehicle.GetStatistics();
            
            Assert.Equal(VehicleState.Repairing, vehicleStatistics.Status);
        }

        private Rally BuildRally(int distance)
        {
            return new Rally(2019, distance);
        }

        private VehicleBuilder AVehicleBuilder => new VehicleBuilder();
    }
}
