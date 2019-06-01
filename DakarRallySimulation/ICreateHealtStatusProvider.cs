namespace DakarRallySimulation.Domain
{
    public interface ICreateHealtStatusProvider
    {
        IProvideHealtStatus BuildForCrossMotorcycle();
        IProvideHealtStatus BuildForSportCar();
        IProvideHealtStatus BuildForSportMotorcycle();
        IProvideHealtStatus BuildForTerrainCar();
        IProvideHealtStatus BuildForTruck();
    }
}