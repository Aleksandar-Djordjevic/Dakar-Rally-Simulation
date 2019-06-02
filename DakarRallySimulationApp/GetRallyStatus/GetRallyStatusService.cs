using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using DakarRallySimulation.Domain;

namespace DakarRallySimulation.App.GetRallyStatus
{
    public class GetRallyStatusInfoService : IProvideRallyStatusInfo
    {
        private readonly IAmRallyRepository _rellyRepo;
        private readonly ICreateRallyStatusInfo _rallyStatusInfoFactory;

        public GetRallyStatusInfoService(IAmRallyRepository rellyRepo, ICreateRallyStatusInfo rallyStatusInfoFactory)
        {
            _rellyRepo = rellyRepo;
            _rallyStatusInfoFactory = rallyStatusInfoFactory;
        }

        public Result<RallyStatusInfo> GetRallyStatusInfo(string rallyId)
        {
            return _rellyRepo.Find(rallyId)
                .OnFailureCompensate(failure => Result.Fail<IAmRally>(ErrorMessages.RallyNotFound))
                .OnSuccess(rally => Result.Ok(_rallyStatusInfoFactory.Create(rally)));
        }
    }
}
