using CSharpFunctionalExtensions;

namespace DakarRallySimulation.App.RemoveVehicleFromRally
{
    public interface IRemoveVehicleFromRally
    {
        Result RemoveVehicleFromRally(string rallyId, string vehicleId);
    }
}