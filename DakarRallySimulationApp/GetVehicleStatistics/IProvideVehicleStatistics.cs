using CSharpFunctionalExtensions;
using DakarRallySimulationApp.GetVehicleStatistics;

namespace DakarRallySimulationApp
{
    public interface IProvideVehicleStatistics
    {
        Result<VehicleStatistics> GetVehicleStatistics(string rallyId, string vehicleId);
    }
}