namespace DakarRallySimulation.Domain
{
    public class HealtStatusProviderFactory : ICreateHealtStatusProvider
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

        public HealtStatusProviderFactory(
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

        public IProvideHealtStatus BuildForSportCar()
        {
            return new HealtStatusProvider(
                _sportCarLightMalfunctionHourlyLikelihood,
                _sportCarHeavyMalfunctionHourlyLikelihood,
                _simulationResolutionTimeInSeconds);
        }

        public IProvideHealtStatus BuildForTerrainCar()
        {
            return new HealtStatusProvider(
                _terrainCarLightMalfunctionHourlyLikelihood,
                _terrainCarHeavyMalfunctionHourlyLikelihood,
                _simulationResolutionTimeInSeconds);
        }

        public IProvideHealtStatus BuildForTruck()
        {
            return new HealtStatusProvider(
                _truckLightMalfunctionHourlyLikelihood,
                _truckHeavyMalfunctionHourlyLikelihood,
                _simulationResolutionTimeInSeconds);
        }

        public IProvideHealtStatus BuildForSportMotorcycle()
        {
            return new HealtStatusProvider(
                _sportMotorcycleLightMalfunctionHourlyLikelihood,
                _sportMotorcycleHeavyMalfunctionHourlyLikelihood,
                _simulationResolutionTimeInSeconds);
        }

        public IProvideHealtStatus BuildForCrossMotorcycle()
        {
            return new HealtStatusProvider(
                _crossMotorcycleLightMalfunctionHourlyLikelihood,
                _crossMotorcycleHeavyMalfunctionHourlyLikelihood,
                _simulationResolutionTimeInSeconds);
        }
    }
}