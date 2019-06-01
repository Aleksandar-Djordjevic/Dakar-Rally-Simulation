using System.Collections.Generic;
using CSharpFunctionalExtensions;
using DakarRallySimulation;

namespace DakarRallySimulationApp
{
    public class RallyRepository : IAmRallyRepository
    {
        private readonly Dictionary<string, Rally> _rallies = new Dictionary<string, Rally>();

        public Result Add(Rally rally)
        {
            return Result.Create(!_rallies.ContainsKey(rally.Id), "Already exists.")
                .OnSuccess(() => _rallies.Add(rally.Id, rally));
        }

        public bool Exists(string rallyId)
        {
            return _rallies.ContainsKey(rallyId);
        }

        public Result<Rally> Find(string rallyId)
        {
            return Result.Create(_rallies.TryGetValue(rallyId, out var rally), rally, "Not found.");
        }        
    }
}