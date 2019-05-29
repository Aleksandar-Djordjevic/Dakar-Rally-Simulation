using System;
using System.Collections.Generic;
using System.Text;

namespace DakarRallySimulation
{
    public class VehicleBuilder : ICreateVehicle
    {
        private readonly int _sportCarMaxSpeed;
        private readonly int _terrainCarMaxSpeed;
        private readonly int _truckMaxSpeed;
        private readonly int _sportMotorcycleMaxSpeed;
        private readonly int _crossMotorcycleMaxSpeed;
        private readonly TimeSpan _carRepairmentDuration;
        private readonly TimeSpan _truckRepairmentDuration;
        private readonly TimeSpan _motorcycleRepairmentDuration;
        private readonly ICreateHealtStatusProvider _healtStatusProviderBuilder;
        private readonly int _simulationResolutionTimeInSeconds;

        public VehicleBuilder(
            int sportCarMaxSpeed,
            int terrainCarMaxSpeed,
            int truckMaxSpeed,
            int sportMotorcycleMaxSpeed,
            int crossMotorcycleMaxSpeed,
            TimeSpan carRepairmentDuration,
            TimeSpan truckRepairmentDuration,
            TimeSpan motorcycleRepairmentDuration,
            ICreateHealtStatusProvider healtStatusProviderBuilder,
            int simulationResolutionTimeInSeconds)
        {
            _sportCarMaxSpeed = sportCarMaxSpeed;
            _terrainCarMaxSpeed = terrainCarMaxSpeed;
            _truckMaxSpeed = truckMaxSpeed;
            _sportMotorcycleMaxSpeed = sportMotorcycleMaxSpeed;
            _crossMotorcycleMaxSpeed = crossMotorcycleMaxSpeed;
            _carRepairmentDuration = carRepairmentDuration;
            _truckRepairmentDuration = truckRepairmentDuration;
            _motorcycleRepairmentDuration = motorcycleRepairmentDuration;
            _healtStatusProviderBuilder = healtStatusProviderBuilder;
            _simulationResolutionTimeInSeconds = simulationResolutionTimeInSeconds;
        }

        public Vehicle BuildSportCar(string id, string teamName, string model, DateTime manufacturingDate)
        {
            return new Vehicle(
                id, 
                teamName, 
                model, 
                manufacturingDate, 
                _sportCarMaxSpeed, 
                _carRepairmentDuration,
                _simulationResolutionTimeInSeconds,
                _healtStatusProviderBuilder.BuildForSportCar());
        }

        public Vehicle BuildTerrainCar(string id, string teamName, string model, DateTime manufacturingDate)
        {
            return new Vehicle(
                id,
                teamName,
                model,
                manufacturingDate,
                _terrainCarMaxSpeed,
                _carRepairmentDuration,
                _simulationResolutionTimeInSeconds,
                _healtStatusProviderBuilder.BuildForTerrainCar());
        }

        public Vehicle BuildTruck(string id, string teamName, string model, DateTime manufacturingDate)
        {
            return new Vehicle(
                id,
                teamName,
                model,
                manufacturingDate,
                _truckMaxSpeed,
                _truckRepairmentDuration,
                _simulationResolutionTimeInSeconds,
                _healtStatusProviderBuilder.BuildForTruck());
        }

        public Vehicle BuildSportMotorcycle(string id, string teamName, string model, DateTime manufacturingDate)
        {
            return new Vehicle(
                id,
                teamName,
                model,
                manufacturingDate,
                _sportMotorcycleMaxSpeed,
                _motorcycleRepairmentDuration,
                _simulationResolutionTimeInSeconds,
                _healtStatusProviderBuilder.BuildForSportMotorcycle());
        }

        public Vehicle BuildCrossMotorcycle(string id, string teamName, string model, DateTime manufacturingDate)
        {
            return new Vehicle(
                id,
                teamName,
                model,
                manufacturingDate,
                _crossMotorcycleMaxSpeed,
                _motorcycleRepairmentDuration,
                _simulationResolutionTimeInSeconds,
                _healtStatusProviderBuilder.BuildForCrossMotorcycle());
        }
    }
}
