using System;
using System.Threading.Tasks;

namespace DakarRallySimulation
{
    public interface IAmVehicle
    {
        event EventHandler<string> VehicleFinishedRally;
        string Id { get; }

        void StartRally(Rally rally);
    }
}