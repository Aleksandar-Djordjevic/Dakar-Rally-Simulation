using CSharpFunctionalExtensions;

namespace DakarRallySimulation.App.GetLeaderboard
{
    public interface IProvideLeaderboard
    {
        Result<Leaderboard> GetLeaderboard(string rallyId);
        Result<Leaderboard> GetLeaderboard(string rallyId, VehicleType type);
    }
}