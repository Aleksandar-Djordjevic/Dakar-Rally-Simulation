using DakarRallySimulation.Domain;

namespace DakarRallySimulation.App.GetRallyStatus
{
    public interface ICreateRallyStatusInfo
    {
        RallyStatusInfo Create(IAmRally rally);
    }
}