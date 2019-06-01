using System;
using System.Collections.Generic;
using System.Text;
using CSharpFunctionalExtensions;
using DakarRallySimulation;

namespace DakarRallySimulationApp.CreateRally
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

            return _rallyRepository.Find(year.ToString())
                .OnSuccess(rally => Result.Fail("Rally for this year already exists."))
                .OnFailure(() => _rallyRepository.Add(new Rally(year, 10000)))
                // on ne vrati rez od add vec ovaj prvisdfds
                ;
        }
    }
}
