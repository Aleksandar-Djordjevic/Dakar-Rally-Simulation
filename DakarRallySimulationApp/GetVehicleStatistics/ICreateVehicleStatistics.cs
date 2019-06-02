using DakarRallySimulation.Domain;
using DakarRallySimulation.Domain.Vehicle;

namespace DakarRallySimulation.App.GetVehicleStatistics
{
    public interface ICreateVehicleStatistics
    {
        VehicleStatistics Create(IAmVehicle vehicle);
    }
}