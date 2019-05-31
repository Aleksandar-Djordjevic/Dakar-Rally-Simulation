namespace DakarRallySimulationApp
{
    public interface IProvideLeaderboard
    {
        void GetLeaderboard();
        void GetLeaderboard(VehicleType type);
    }
}