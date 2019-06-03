using CSharpFunctionalExtensions;
using DakarRallySimulation.Domain;

namespace DakarRallySimulation.App.GetVehicleStatistics
{
    public class VehicleStatisticsService : IProvideVehicleStatistics
    {
        private readonly IAmRallyRepository _rellyRepo;
        private readonly ICreateVehicleStatistics _vehicleStatisticsFactory;

        public VehicleStatisticsService(IAmRallyRepository rellyRepo, ICreateVehicleStatistics vehicleStatisticsFactory)
        {
            _rellyRepo = rellyRepo;
            _vehicleStatisticsFactory = vehicleStatisticsFactory;
        }

        public Result<VehicleStatistics> GetVehicleStatistics(string rallyId, string vehicleId)
        {
            return _rellyRepo.Find(rallyId)
                .OnFailureCompensate(failure => Result.Fail<IAmRally>(ErrorMessages.RallyNotFound))
                .OnSuccess(rally =>
                    Result.Create(rally.Vehicles.TryGetValue(vehicleId, out var v), v, ErrorMessages.VehicleDoesNotExist)
                    .Map(GetStatistics));
        }

        private VehicleStatistics GetStatistics(IAmVehicle vehicle)
        {
            return _vehicleStatisticsFactory.Create(vehicle);
        }
    }
}
