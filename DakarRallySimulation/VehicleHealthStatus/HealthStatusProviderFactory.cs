namespace DakarRallySimulation.Domain
{
    public class HealthStatusProviderFactory : ICreateHealthStatusProvider
    {
        private readonly int _sportCarLightMalfunctionHourlyLikelihood;
        private readonly int _sportCarHeavyMalfunctionHourlyLikelihood;
        private readonly int _terrainCarLightMalfunctionHourlyLikelihood;
        private readonly int _terrainCarHeavyMalfunctionHourlyLikelihood;
        private readonly int _truckLightMalfunctionHourlyLikelihood;
        private readonly int _truckHeavyMalfunctionHourlyLikelihood;
        private readonly int _sportMotorcycleLightMalfunctionHourlyLikelihood;
        private readonly int _sportMotorcycleHeavyMalfunctionHourlyLikelihood;
        private readonly int _crossMotorcycleLightMalfunctionHourlyLikelihood;
        private readonly int _crossMotorcycleHeavyMalfunctionHourlyLikelihood;
        private readonly int _simulationResolutionTimeInSeconds;

        public HealthStatusProviderFactory(
            int sportCarLightMalfunctionHourlyLikelihood,
            int sportCarHeavyMalfunctionHourlyLikelihood,
            int terrainCarLightMalfunctionHourlyLikelihood,
            int terrainCarHeavyMalfunctionHourlyLikelihood,
            int truckLightMalfunctionHourlyLikelihood,
            int truckHeavyMalfunctionHourlyLikelihood,
            int sportMotorcycleLightMalfunctionHourlyLikelihood,
            int sportMotorcycleHeavyMalfunctionHourlyLikelihood,
            int crossMotorcycleLightMalfunctionHourlyLikelihood,
            int crossMotorcycleHeavyMalfunctionHourlyLikelihood,
            int simulationResolutionTimeInSeconds)
        {
            _sportCarLightMalfunctionHourlyLikelihood = sportCarLightMalfunctionHourlyLikelihood;
            _sportCarHeavyMalfunctionHourlyLikelihood = sportCarHeavyMalfunctionHourlyLikelihood;
            _terrainCarLightMalfunctionHourlyLikelihood = terrainCarLightMalfunctionHourlyLikelihood;
            _terrainCarHeavyMalfunctionHourlyLikelihood = terrainCarHeavyMalfunctionHourlyLikelihood;
            _truckLightMalfunctionHourlyLikelihood = truckLightMalfunctionHourlyLikelihood;
            _truckHeavyMalfunctionHourlyLikelihood = truckHeavyMalfunctionHourlyLikelihood;
            _sportMotorcycleLightMalfunctionHourlyLikelihood = sportMotorcycleLightMalfunctionHourlyLikelihood;
            _sportMotorcycleHeavyMalfunctionHourlyLikelihood = sportMotorcycleHeavyMalfunctionHourlyLikelihood;
            _crossMotorcycleLightMalfunctionHourlyLikelihood = crossMotorcycleLightMalfunctionHourlyLikelihood;
            _crossMotorcycleHeavyMalfunctionHourlyLikelihood = crossMotorcycleHeavyMalfunctionHourlyLikelihood;
            _simulationResolutionTimeInSeconds = simulationResolutionTimeInSeconds;
        }

        public IProvideHealthStatus BuildForSportCar()
        {
            return new HealthStatusProvider(
                _sportCarLightMalfunctionHourlyLikelihood,
                _sportCarHeavyMalfunctionHourlyLikelihood,
                _simulationResolutionTimeInSeconds);
        }

        public IProvideHealthStatus BuildForTerrainCar()
        {
            return new HealthStatusProvider(
                _terrainCarLightMalfunctionHourlyLikelihood,
                _terrainCarHeavyMalfunctionHourlyLikelihood,
                _simulationResolutionTimeInSeconds);
        }

        public IProvideHealthStatus BuildForTruck()
        {
            return new HealthStatusProvider(
                _truckLightMalfunctionHourlyLikelihood,
                _truckHeavyMalfunctionHourlyLikelihood,
                _simulationResolutionTimeInSeconds);
        }

        public IProvideHealthStatus BuildForSportMotorcycle()
        {
            return new HealthStatusProvider(
                _sportMotorcycleLightMalfunctionHourlyLikelihood,
                _sportMotorcycleHeavyMalfunctionHourlyLikelihood,
                _simulationResolutionTimeInSeconds);
        }

        public IProvideHealthStatus BuildForCrossMotorcycle()
        {
            return new HealthStatusProvider(
                _crossMotorcycleLightMalfunctionHourlyLikelihood,
                _crossMotorcycleHeavyMalfunctionHourlyLikelihood,
                _simulationResolutionTimeInSeconds);
        }
    }
}