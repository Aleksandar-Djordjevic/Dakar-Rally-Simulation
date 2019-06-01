using CSharpFunctionalExtensions;
using DakarRallySimulation;

namespace DakarRallySimulationApp
{
    public interface IAmRallyRepository
    {
        Result Add(Rally rally);
        bool Exists(string rallyId);
        Result<Rally> Find(string rallyId);
    }
}