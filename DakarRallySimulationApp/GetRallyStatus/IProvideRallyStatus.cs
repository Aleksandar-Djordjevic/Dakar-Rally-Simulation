using CSharpFunctionalExtensions;

namespace DakarRallySimulation.App.GetRallyStatus
{
    public interface IProvideRallyStatusInfo
    {
        Result<RallyStatusInfo> GetRallyStatus(string rallyId);
    }
}