using CSharpFunctionalExtensions;

namespace DakarRallySimulation.App.GetVehicleStatistics
{
    public interface IProvideVehicleStatistics
    {
        Result<VehicleStatistics> GetVehicleStatistics(string rallyId, string vehicleId);
    }
}