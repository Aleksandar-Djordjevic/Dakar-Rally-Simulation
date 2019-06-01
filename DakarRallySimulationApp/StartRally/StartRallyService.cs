using CSharpFunctionalExtensions;
using DakarRallySimulation.Domain;

namespace DakarRallySimulation.App.StartRally
{
    public class StartRallyService : IStartRally
    {
        private readonly IAmRallyRepository _rellyRepo;

        public StartRallyService(IAmRallyRepository rellyRepo)
        {
            _rellyRepo = rellyRepo;
        }

        public Result StartRally(string rallyId)
        {
            return _rellyRepo.Find(rallyId)
                .OnFailureCompensate(failure => Result.Fail<IAmRally>(ErrorMessages.RallyNotFound))
                .OnSuccess(rally => rally.Start());
        }
    }
}
