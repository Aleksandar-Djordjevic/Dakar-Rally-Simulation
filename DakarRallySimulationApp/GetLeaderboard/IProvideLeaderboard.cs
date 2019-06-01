namespace DakarRallySimulation.App.GetLeaderboard
{
    public interface IProvideLeaderboard
    {
        void GetLeaderboard();
        void GetLeaderboard(VehicleType type);
    }
}