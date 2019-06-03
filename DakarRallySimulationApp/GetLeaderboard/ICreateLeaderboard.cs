using System.Collections.Generic;

namespace DakarRallySimulation.App.GetLeaderboard
{
    public interface ICreateLeaderboard
    {
        Leaderboard Create(List<Vehicle> vehicles);
    }
}