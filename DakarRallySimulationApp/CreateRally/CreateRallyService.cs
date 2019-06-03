using CSharpFunctionalExtensions;
using DakarRallySimulation.Domain;

namespace DakarRallySimulation.App.CreateRally
{
    public class CreateRallyService : ICreateRally
    {
        private readonly IAmRallyRepository _rallyRepository;

        public CreateRallyService(IAmRallyRepository rallyRepository)
        {
            _rallyRepository = rallyRepository;
        }

        public Result CreateRally(int year)
        {
            return Result.Create(!_rallyRepository.Exists(year.ToString()), "Rally for this year already exists.")
                .OnSuccess(() => _rallyRepository.Add(new Rally(year, 10000)));
        }
    }
}
