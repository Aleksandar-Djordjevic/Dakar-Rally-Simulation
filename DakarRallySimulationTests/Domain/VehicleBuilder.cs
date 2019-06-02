using System;
using DakarRallySimulation.Domain;
using DakarRallySimulation.Domain.Vehicle;
using DakarRallySimulation.Domain.VehicleHealthStatus;
using Moq;

namespace DakarRallySimulation.Tests.Domain
{
    public class VehicleBuilder
    {
        public Vehicle BuildProperlyWorkingVehicle()
        {
            var healtStatusProviderMock = new Mock<IProvideHealthStatus>();
            healtStatusProviderMock
                .Setup(provider => provider.GetHealtStatus())
                .Returns(HealthStatus.WorkingProperly);

            return new Vehicle(
                VehicleType.Car, "ID 1", "BMW Racing", "BMW 3M", DateTime.UtcNow, 600, TimeSpan.FromSeconds(3), 2, healtStatusProviderMock.Object);
        }

        public Vehicle BuildLightlyMalfunctioningVehicle()
        {
            var healtStatusNumber = 0;
            var healtStatusProviderMock = new Mock<IProvideHealthStatus>();
            healtStatusProviderMock
                .Setup(provider => provider.GetHealtStatus())
                .Returns(() => healtStatusNumber++ % 2 == 0 ? HealthStatus.LightMalfunction : HealthStatus.WorkingProperly);

            return new Vehicle(
                VehicleType.Car, "ID 1", "BMW Racing", "BMW 3M", DateTime.UtcNow, 600, TimeSpan.FromSeconds(3), 2, healtStatusProviderMock.Object);
        }

        public Vehicle BuildHeavilyMalfunctioningVehicle()
        {
            var healtStatusProviderMock = new Mock<IProvideHealthStatus>();
            healtStatusProviderMock
                .Setup(provider => provider.GetHealtStatus())
                .Returns(HealthStatus.HeavyMalfunction);

            return new Vehicle(
                VehicleType.Car, "ID 1", "BMW Racing", "BMW 3M", DateTime.UtcNow, 600, TimeSpan.FromSeconds(3), 2, healtStatusProviderMock.Object);
        }

        public Vehicle BuildVehicleWhichLightlyMalfunctionsInBeginning()
        {
            var healtStatusNumber = 0;
            var healtStatusProviderMock = new Mock<IProvideHealthStatus>();
            healtStatusProviderMock
                .Setup(provider => provider.GetHealtStatus())
                .Returns(() => healtStatusNumber++ < 2 ? HealthStatus.LightMalfunction : HealthStatus.WorkingProperly);

            return new Vehicle(
                VehicleType.Car, "ID 1", "BMW Racing", "BMW 3M", DateTime.UtcNow, 600, TimeSpan.FromSeconds(3), 2, healtStatusProviderMock.Object);
        }

        public Vehicle BuildLightlyMalfunctioningVehicleWithLongRapairmentDuration()
        {
            var healtStatusProviderMock = new Mock<IProvideHealthStatus>();
            healtStatusProviderMock
                .Setup(provider => provider.GetHealtStatus())
                .Returns(HealthStatus.LightMalfunction);

            return new Vehicle(
                VehicleType.Car, "ID 1", "BMW Racing", "BMW 3M", DateTime.UtcNow, 600, TimeSpan.FromSeconds(60), 1, healtStatusProviderMock.Object);
        }
    }
}
