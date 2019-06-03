using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using DakarRallySimulation.App;
using DakarRallySimulation.App.GetLeaderboard;
using DakarRallySimulation.App.GetRallyStatus;
using DakarRallySimulation.App.GetVehicleStatistics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Vehicle = DakarRallySimulation.App.AddVehicleToRally.Vehicle;

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
            var result = _rallySimulationApp.CreateRally(year)
                .OnSuccess<ActionResult>(() => StatusCode(201))
                .OnFailureCompensate(func: error => GetActionResult(error));
            return result.Value;
        }

        [HttpPost("{rallyId}/vehicles/")]
        public ActionResult AddVehicle(string rallyId, [FromBody] Vehicle vehicle)
        {
            var result = _rallySimulationApp.AddSportCar(rallyId, vehicle.Id, vehicle.TeamName, vehicle.Model, vehicle.ManufacturingDate)
                .OnSuccess<ActionResult>(() => StatusCode(201))
                .OnFailureCompensate(func: error => GetActionResult(error));
            return result.Value;
        }

        [HttpDelete("{rallyId}/vehicles/{vehicleId}")]
        public ActionResult RemoveVehicle(string rallyId, string vehicleId)
        {
            var result = _rallySimulationApp.RemoveVehicleFromRally(rallyId, vehicleId)
                .OnSuccess<ActionResult>(() => StatusCode(200))
                .OnFailureCompensate(func: error => GetActionResult(error));
            return result.Value;
        }

        [HttpPost("{rallyId}/start")]
        public ActionResult StartRally(string rallyId)
        {
            var result = _rallySimulationApp.StartRally(rallyId)
                .OnSuccess<ActionResult>(() => StatusCode(200))
                .OnFailureCompensate(func: error => GetActionResult(error));
            return result.Value;
        }

        [HttpGet("{rallyId}/leaderboard")]
        public ActionResult GetLeaderboard(string rallyId)
        {
            var result = _rallySimulationApp.GetLeaderboard(rallyId)
                .OnSuccess<Leaderboard, ActionResult>(leaderboard => new ObjectResult(leaderboard))
                .OnFailureCompensate(func: error => GetActionResult(error));
            return result.Value;
        }

        [HttpGet("{rallyId}/leaderboard/{type}")]
        public ActionResult GetLeaderboard(string rallyId, VehicleType type)
        {
            var result = _rallySimulationApp.GetLeaderboard(rallyId, type)
                .OnSuccess<Leaderboard, ActionResult>(leaderboard => new ObjectResult(leaderboard))
                .OnFailureCompensate(func: error => GetActionResult(error));
            return result.Value;
        }

        [HttpGet("{rallyId}/vehicles/{vehicleId}/statistics")]
        public ActionResult<VehicleStatistics> GetVehicleStatistics(string rallyId, string vehicleId)
        {
            var result =_rallySimulationApp.GetVehicleStatistics(rallyId, vehicleId)
                .OnSuccess<VehicleStatistics, ActionResult>(stats => new ObjectResult(stats))
                .OnFailureCompensate(func: error => GetActionResult(error));
            return result.Value;
        }

        [HttpGet("{rallyId}/status")]
        public ActionResult<RallyStatusInfo> GetRallyStatusInfo(string rallyId)
        {
            var result = _rallySimulationApp.GetRallyStatusInfo(rallyId)
                .OnSuccess<RallyStatusInfo, ActionResult>(stats => new ObjectResult(stats))
                .OnFailureCompensate(func: error => GetActionResult(error));
            return result.Value;
        }

        private Result<ActionResult> GetActionResult(string errorMessage)
        {
            return Result.Ok<ActionResult>(StatusCode(GetStatusCodeFromErrorMessage(errorMessage)));
        }
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