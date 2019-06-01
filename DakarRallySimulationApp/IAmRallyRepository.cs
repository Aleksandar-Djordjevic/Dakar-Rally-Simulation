using CSharpFunctionalExtensions;
using DakarRallySimulation.Domain;

namespace DakarRallySimulation.App
{
    public interface IAmRallyRepository
    {
        Result Add(IAmRally rally);
        bool Exists(string rallyId);
        Result<IAmRally> Find(string rallyId);
    }
}