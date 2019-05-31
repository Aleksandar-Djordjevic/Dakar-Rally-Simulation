using System.Collections.Generic;
using System.Xml.Schema;
using DakarRallySimulation;

namespace DakarRallySimulationApp
{
    public class App// : ISimulateDakarRally
    {
        private readonly IAmRallyRepository rallyRepo;
    }

    internal interface IAmRallyRepository
    {
    }

    public class RallyRepository : IAmRallyRepository
    {
        private readonly Dictionary<string, Rally> _rallies;
    }
}
