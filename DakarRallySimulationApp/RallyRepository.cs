using System.Collections.Generic;
using CSharpFunctionalExtensions;
using DakarRallySimulation.Domain;

namespace DakarRallySimulation.App
{
    public class RallyRepository : IAmRallyRepository
    {
        private readonly Dictionary<string, IAmRally> _rallies = new Dictionary<string, IAmRally>();

        public Result Add(IAmRally rally)
        {
            return Result.Create(!_rallies.ContainsKey(rally.Id), "Already exists.")
                .OnSuccess(() => _rallies.Add(rally.Id, rally));
        }

        public bool Exists(string rallyId)
        {
            return _rallies.ContainsKey(rallyId);
        }

        public Result<IAmRally> Find(string rallyId)
        {
            return Result.Create(_rallies.TryGetValue(rallyId, out var rally), rally, "Not found.");
        }        
    }
}