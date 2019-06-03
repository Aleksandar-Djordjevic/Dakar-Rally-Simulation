using System.Collections.Generic;

namespace DakarRallySimulation.App.GetLeaderboard
{
    public class LeaderboardFactory : ICreateLeaderboard
    {
        public Leaderboard Create(List<Vehicle> vehicles)
        {
            vehicles.Sort();
            return new Leaderboard
            {
                Vehicles = vehicles
            };
        }
    }
}