namespace DakarRallySimulationApp
{
    public interface ISimulateDakarRally : 
        ICreateRally, 
        IAddVehicleToRally, 
        IRemoveVehicleFromRally, 
        IStartRally, 
        IProvideLeaderboard,
        IProvideVehicleStatistics,
        IFindVehicle,
        IProvideRallyStatus
    {
    }
}