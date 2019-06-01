using CSharpFunctionalExtensions;
using DakarRallySimulation;

namespace DakarRallySimulationApp
{
    public interface IAmRallyRepository
    {
        Result Add(IAmRally rally);
        bool Exists(string rallyId);
        Result<IAmRally> Find(string rallyId);
    }
}