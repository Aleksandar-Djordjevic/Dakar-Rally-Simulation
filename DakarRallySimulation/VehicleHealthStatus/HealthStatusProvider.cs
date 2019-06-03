using System;

namespace DakarRallySimulation.Domain
{
    public class HealthStatusProvider : IProvideHealthStatus
    {
        private readonly int _lightMalfunctionHourlyLikelihood;
        private readonly int _heavyMalfunctionHourlyLikelihood;
        private readonly int _simulationResolutionTimeInSeconds;
        private readonly Random _random;

        public HealthStatusProvider(int lightMalfunctionHourlyLikelihood, int heavyMalfunctionHourlyLikelihood, int simulationResolutionTimeInSeconds)
        {
            _lightMalfunctionHourlyLikelihood = lightMalfunctionHourlyLikelihood;
            _heavyMalfunctionHourlyLikelihood = heavyMalfunctionHourlyLikelihood;
            _simulationResolutionTimeInSeconds = simulationResolutionTimeInSeconds;
            _random = new Random();
        }

        public HealthStatus GetHealtStatus()
        {
            var randomNumber = _random.Next(1, 100 * 3600 / _simulationResolutionTimeInSeconds);
            if (randomNumber <= _heavyMalfunctionHourlyLikelihood)
            {
                return HealthStatus.HeavyMalfunction;
            }
            if (randomNumber <= _lightMalfunctionHourlyLikelihood)
            {
                return HealthStatus.LightMalfunction;
            }

            return HealthStatus.WorkingProperly;
        } 
    }
}
