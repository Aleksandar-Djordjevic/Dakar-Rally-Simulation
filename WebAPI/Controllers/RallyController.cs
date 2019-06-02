using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using DakarRallySimulation.App;
using DakarRallySimulation.App.GetVehicleStatistics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RallyController : ControllerBase
    {
        private readonly ISimulateDakarRally _rallySimulationApp;

        public RallyController(ISimulateDakarRally rallySimulationApp)
        {
            _rallySimulationApp = rallySimulationApp;
        }

        [HttpPost("{year}")]
        public ActionResult CreateRally(int year)
        {
            int status = 0;
            _rallySimulationApp.CreateRally(year)
                .OnSuccess(() => status = 201)
                .OnFailure(e => status = GetStatusCodeFromErrorMessage(e));
            return StatusCode(status);
        }

        [HttpDelete("{rallyId}/vehicle/{vehicleId}")]
        public ActionResult RemoveVehicle(string rallyId, string vehicleId)
        {
            int status = 0;
            _rallySimulationApp.RemoveVehicleFromRally(rallyId, vehicleId)
                .OnSuccess(() => status = 200)
                .OnFailure(e => status = GetStatusCodeFromErrorMessage(e));
            return StatusCode(status);
        }

        [HttpPost("{rallyId}/start")]
        public ActionResult StartRally(string rallyId)
        {
            int status = 0;
            _rallySimulationApp.StartRally(rallyId)
                .OnSuccess(() => status = 200)
                .OnFailure(e => status = GetStatusCodeFromErrorMessage(e));
            return StatusCode(status);
        }

        //[HttpGet("{rallyId}/vehicle/{vehicleId}/statistics")]
        //public ActionResult<VehicleStatistics> GetVehicleStatistics(string rallyId, string vehicleId)
        //{
        //    int status = 0;
        //    _rallySimulationApp.GetVehicleStatistics(rallyId, vehicleId)
        //        .OnSuccess(() => status = 201)
        //        .OnFailure(e => status = GetStatusCodeFromErrorMessage(e));
        //    return StatusCode(status);
        //}

        private int GetStatusCodeFromErrorMessage(string errorMessage)
        {
            if (errorMessage == ErrorMessages.RallyNotFound ||
                errorMessage == ErrorMessages.VehicleDoesNotExist)
                return 404;
            if (errorMessage == ErrorMessages.VehicleAlreadyAdded ||
                errorMessage == ErrorMessages.CannotStartRallyWithNoVehicles)
                return 400;
            return 500;
        }

    }
}