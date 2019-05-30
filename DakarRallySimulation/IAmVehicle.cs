using System;
using System.Threading.Tasks;

namespace DakarRallySimulation
{
    public interface IAmVehicle
    {
        event EventHandler FinishedRally;
        string Id { get; }

        void StartRally(Rally rally);
    }
}