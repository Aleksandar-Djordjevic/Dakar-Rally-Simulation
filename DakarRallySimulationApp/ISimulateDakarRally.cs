using DakarRallySimulation.App.AddVehicleToRally;
using DakarRallySimulation.App.CreateRally;
using DakarRallySimulation.App.FindVehicle;
using DakarRallySimulation.App.GetLeaderboard;
using DakarRallySimulation.App.GetRallyStatus;
using DakarRallySimulation.App.GetVehicleStatistics;
using DakarRallySimulation.App.RemoveVehicleFromRally;
using DakarRallySimulation.App.StartRally;

namespace DakarRallySimulation.App
{
    public interface ISimulateDakarRally : 
        ICreateRally, 
        IAddVehicleToRally, 
        IRemoveVehicleFromRally, 
        IStartRally, 
        IProvideLeaderboard,
        IProvideVehicleStatistics,
        IFindVehicle,
        IProvideRallyStatusInfo
    {
    }
}