using System;
using System.Linq;
using System.Text;
using CSharpFunctionalExtensions;

using DakarRallySimulation.Domain;

namespace DakarRallySimulation.App.GetLeaderboard
{
    public class LeaderboardService : IProvideLeaderboard
    {
        private readonly IAmRallyRepository _rellyRepo;
        private readonly ICreateLeaderboard _leaderboardFactory;

        public LeaderboardService(IAmRallyRepository rellyRepo, ICreateLeaderboard leaderboardFactory)
        {
            _rellyRepo = rellyRepo;
            _leaderboardFactory = leaderboardFactory;
        }

        public Result<Leaderboard> GetLeaderboard(string rallyId)
        {
            return _rellyRepo.Find(rallyId)
                .OnFailureCompensate(failure => Result.Fail<IAmRally>(ErrorMessages.RallyNotFound))
                .OnSuccess(rally => Result.Ok(_leaderboardFactory.Create(
                    rally.Vehicles.Values.ToDto())));
        }

        public Result<Leaderboard> GetLeaderboard(string rallyId, VehicleType type)
        {
            return _rellyRepo.Find(rallyId)
                .OnFailureCompensate(failure => Result.Fail<IAmRally>(ErrorMessages.RallyNotFound))
                .OnSuccess(rally => Result.Ok(_leaderboardFactory.Create(
                    rally.Vehicles.Values.ToDto().Where(vehicle => vehicle.Type == type).ToList())));
        }
    }
}
