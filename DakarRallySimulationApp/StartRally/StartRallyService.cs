using System;
using System.Collections.Generic;
using System.Text;
using CSharpFunctionalExtensions;
using DakarRallySimulation;

namespace DakarRallySimulationApp.StartRally
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
