namespace DakarRallySimulation
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