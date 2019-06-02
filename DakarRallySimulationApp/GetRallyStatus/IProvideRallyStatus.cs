using CSharpFunctionalExtensions;

namespace DakarRallySimulation.App.GetRallyStatus
{
    public interface IProvideRallyStatusInfo
    {
        Result<RallyStatusInfo> GetRallyStatusInfo(string rallyId);
    }
}