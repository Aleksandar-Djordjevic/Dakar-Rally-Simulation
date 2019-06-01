using System;

namespace DakarRallySimulation.Domain
{
    public class HealtStatusProvider : IProvideHealtStatus
    {
        private readonly int _lightMalfunctionHourlyLikelihood;
        private readonly int _heavyMalfunctionHourlyLikelihood;
        private readonly int _simulationResolutionTimeInSeconds;
        private readonly Random _random;

        public HealtStatusProvider(int lightMalfunctionHourlyLikelihood, int heavyMalfunctionHourlyLikelihood, int simulationResolutionTimeInSeconds)
        {
            _lightMalfunctionHourlyLikelihood = lightMalfunctionHourlyLikelihood;
            _heavyMalfunctionHourlyLikelihood = heavyMalfunctionHourlyLikelihood;
            _simulationResolutionTimeInSeconds = simulationResolutionTimeInSeconds;
            _random = new Random();
        }

        public HealtStatus GetHealtStatus()
        {
            var randomNumber = _random.Next(1, 100 * 3600 / _simulationResolutionTimeInSeconds);
            if (randomNumber <= _heavyMalfunctionHourlyLikelihood)
            {
                return HealtStatus.HeavyMalfunction;
            }
            if (randomNumber <= _lightMalfunctionHourlyLikelihood)
            {
                return HealtStatus.LightMalfunction;
            }

            return HealtStatus.WorkingProperly;
        } 
    }
}
