using System;
using DakarRallySimulation.Domain;
using Moq;

namespace DakarRallySimulation.Tests.Domain
{
    public class VehicleBuilder
    {
        public Vehicle BuildProperlyWorkingVehicle()
        {
            var healtStatusProviderMock = new Mock<IProvideHealtStatus>();
            healtStatusProviderMock
                .Setup(provider => provider.GetHealtStatus())
                .Returns(HealtStatus.WorkingProperly);

            return new Vehicle(
                "ID 1", "BMW Racing", "BMW 3M", DateTime.UtcNow, 600, TimeSpan.FromSeconds(3), 2, healtStatusProviderMock.Object);
        }

        public Vehicle BuildLightlyMalfunctioningVehicle()
        {
            var healtStatusNumber = 0;
            var healtStatusProviderMock = new Mock<IProvideHealtStatus>();
            healtStatusProviderMock
                .Setup(provider => provider.GetHealtStatus())
                .Returns(() => healtStatusNumber++ % 2 == 0 ? HealtStatus.LightMalfunction : HealtStatus.WorkingProperly);

            return new Vehicle(
                "ID 1", "BMW Racing", "BMW 3M", DateTime.UtcNow, 600, TimeSpan.FromSeconds(3), 2, healtStatusProviderMock.Object);
        }

        public Vehicle BuildHeavilyMalfunctioningVehicle()
        {
            var healtStatusProviderMock = new Mock<IProvideHealtStatus>();
            healtStatusProviderMock
                .Setup(provider => provider.GetHealtStatus())
                .Returns(HealtStatus.HeavyMalfunction);

            return new Vehicle(
                "ID 1", "BMW Racing", "BMW 3M", DateTime.UtcNow, 600, TimeSpan.FromSeconds(3), 2, healtStatusProviderMock.Object);
        }

        public Vehicle BuildVehicleWhichLightlyMalfunctionsInBeginning()
        {
            var healtStatusNumber = 0;
            var healtStatusProviderMock = new Mock<IProvideHealtStatus>();
            healtStatusProviderMock
                .Setup(provider => provider.GetHealtStatus())
                .Returns(() => healtStatusNumber++ < 2 ? HealtStatus.LightMalfunction : HealtStatus.WorkingProperly);

            return new Vehicle(
                "ID 1", "BMW Racing", "BMW 3M", DateTime.UtcNow, 600, TimeSpan.FromSeconds(3), 2, healtStatusProviderMock.Object);
        }

        public Vehicle BuildLightlyMalfunctioningVehicleWithLongRapairmentDuration()
        {
            var healtStatusProviderMock = new Mock<IProvideHealtStatus>();
            healtStatusProviderMock
                .Setup(provider => provider.GetHealtStatus())
                .Returns(HealtStatus.LightMalfunction);

            return new Vehicle(
                "ID 1", "BMW Racing", "BMW 3M", DateTime.UtcNow, 600, TimeSpan.FromSeconds(60), 1, healtStatusProviderMock.Object);
        }
    }
}
