using CSharpFunctionalExtensions;

namespace DakarRallySimulation.App.StartRally
{
    public interface IStartRally
    {
        Result StartRally(string rallyId);
    }
}