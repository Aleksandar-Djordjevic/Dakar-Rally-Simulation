using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using CSharpFunctionalExtensions;

namespace DakarRallySimulation.Domain
{
    public class Rally : IAmRally
    {
        public event EventHandler Started;
        
        public string Id { get; }
        public int Year { get; }

        public RallyStatus GetStatus()
        {
            return _state.Status;
        }
        public int Distance { get; }
        public bool IsFinished { get; private set; }
        public ImmutableDictionary<string, IAmVehicle> Vehicles => _vehicles.ToImmutableDictionary();

        private RallyState _state;
        private Dictionary<string, IAmVehicle> _vehicles;
        private HashSet<string> _vehiclesStillInRally = new HashSet<string>();

        public Rally(int year, int distance)
        {
            Id = year.ToString();
            Year = year;
            Distance = distance;
            _state = new RallyPending(this);
            _vehicles = new Dictionary<string, IAmVehicle>();
        }

        public Result AddVehicle(IAmVehicle vehicle)
        {
            return Result.Create(_state.AllowedVehicleAdding, "Adding vehicle not allowed.")
                .OnSuccess(() => Result.Create(!_vehicles.ContainsKey(vehicle.Id), ErrorMessages.VehicleAlreadyAdded))
                .OnSuccess(() =>
                {
                    _vehicles.Add(vehicle.Id, vehicle);
                    _vehiclesStillInRally.Add(vehicle.Id);
                    vehicle.FinishedRally += WhenVehicleFinishesRally;
                });
        }

        public Result RemoveVehicle(string vehicleId)
        {
            return Result.Create(_state.AllowedVehicleRemoving, "Removing vehicle not allowed.")
                .OnSuccess(() => Result.Create(_vehicles.TryGetValue(vehicleId, out var v), v, ErrorMessages.VehicleDoesNotExist))
                .OnSuccess(vehicle =>
                {
                    _vehicles.Remove(vehicle.Id);
                    _vehiclesStillInRally.Remove(vehicle.Id);
                    vehicle.FinishedRally -= WhenVehicleFinishesRally;
                });
        }

        public Result Start()
        {
            return _state.Start();
        }

        private void WhenVehicleFinishesRally(object vehicle, EventArgs e)
        {
            _state.VehicleFinishedRally(((IAmVehicle)vehicle).Id);
        }

        protected virtual void OnStarted()
        {
            Started?.Invoke(this, EventArgs.Empty);
        }

        private abstract class RallyState
        {
            public abstract RallyStatus Status { get; }
            protected readonly Rally Rally;

            public RallyState(Rally rally)
            {
                Rally = rally;
            }

            public abstract bool AllowedVehicleAdding { get; }
            public abstract bool AllowedVehicleRemoving { get; }
            public abstract Result Start();

            public abstract void VehicleFinishedRally(string vehicleId);
        }

        private class RallyPending : RallyState
        {
            public RallyPending(Rally rally) : base (rally) { }

            public override RallyStatus Status { get {return RallyStatus.Pending; }}

            public override bool AllowedVehicleAdding => true;

            public override bool AllowedVehicleRemoving => true;

            public override Result Start()
            {
                return Result.Create(Rally._vehicles.Any(), ErrorMessages.CannotStartRallyWithNoVehicles)
                    .OnSuccess(() =>
                    {
                        Rally._state = new RallyRunning(Rally);
                        Rally.OnStarted();
                        foreach (var vehicle in Rally._vehicles.Values)
                        {
                            vehicle.StartRally(Rally);
                        }
                    });
            }

            public override void VehicleFinishedRally(string vehicleId)
            {
                // log error
            }
        }

        private class RallyRunning : RallyState
        {
            public RallyRunning(Rally rally) : base (rally) { }

            public override RallyStatus Status { get {return RallyStatus.Running; }}

            public override bool AllowedVehicleAdding => false;

            public override bool AllowedVehicleRemoving => false;

            public override Result Start()
            {
                return Result.Fail("Rally is already started.");
            }

            public override void VehicleFinishedRally(string vehicleId)
            {
                Rally._vehiclesStillInRally.Remove(vehicleId);
                if (!Rally._vehiclesStillInRally.Any())
                {
                    Rally._state = new RallyFinished(Rally);
                }
            }
        }

        private class RallyFinished : RallyState
        {
            public RallyFinished(Rally rally) : base(rally)
            {
                rally.IsFinished = true;
            }

            public override RallyStatus Status { get {return RallyStatus.Finished; }}

            public override bool AllowedVehicleAdding => false;

            public override bool AllowedVehicleRemoving => false;

            public override Result Start()
            {
                return Result.Fail("Rally is already started.");
            }

            public override void VehicleFinishedRally(string vehicleId)
            {
                // log error
            }
        }
    }

}
