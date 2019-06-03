namespace DakarRallySimulation.Domain
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