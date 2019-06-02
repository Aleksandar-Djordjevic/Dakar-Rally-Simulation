using System;
using System.Collections.Generic;
using System.Text;
using CSharpFunctionalExtensions;

namespace DakarRallySimulation.App.GetRallyStatus
{
    public class GetRallyStatusInfoService : IProvideRallyStatusInfo
    {
        private readonly IAmRallyRepository _rellyRepo;

        public GetRallyStatusInfoService(IAmRallyRepository rellyRepo)
        {
            _rellyRepo = rellyRepo;
        }

        public Result<RallyStatusInfo> GetRallyStatus(string rallyId)
        {
            throw new NotImplementedException();
        }
    }
}
