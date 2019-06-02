using DakarRallySimulation.Domain;

namespace DakarRallySimulation.App.GetVehicleStatistics
{
    public interface ICreateVehicleStatistics
    {
        VehicleStatistics Create(IAmVehicle vehicle);
    }
}