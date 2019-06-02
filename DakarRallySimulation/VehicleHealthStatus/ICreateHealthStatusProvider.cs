namespace DakarRallySimulation.Domain.VehicleHealthStatus
{
    public interface ICreateHealthStatusProvider
    {
        IProvideHealthStatus BuildForCrossMotorcycle();
        IProvideHealthStatus BuildForSportCar();
        IProvideHealthStatus BuildForSportMotorcycle();
        IProvideHealthStatus BuildForTerrainCar();
        IProvideHealthStatus BuildForTruck();
    }
}