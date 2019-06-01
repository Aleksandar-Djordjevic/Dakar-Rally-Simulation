using CSharpFunctionalExtensions;

namespace DakarRallySimulationApp
{
    public interface IRemoveVehicleFromRally
    {
        Result RemoveVehicleFromRally(string rallyId, string vehicleId);
    }
}