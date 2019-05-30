using System;
using System.Threading.Tasks;

namespace DakarRallySimulation
{
    public interface IAmVehicle
    {
        event EventHandler<string> FinishedRally;
        string Id { get; }

        void StartRally(Rally rally);
    }
}