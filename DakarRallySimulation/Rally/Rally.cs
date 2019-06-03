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
            return _state.AddVehicle(vehicle);
        }

        public Result RemoveVehicle(string vehicleId)
        {
            return _state.RemoveVehicle(vehicleId);
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

            public abstract Result AddVehicle(IAmVehicle vehicle);
            public abstract Result RemoveVehicle(string vehicleId);
            public abstract Result Start();

            public abstract void VehicleFinishedRally(string vehicleId);
        }

        private class RallyPending : RallyState
        {
            public RallyPending(Rally rally) : base (rally) { }

            public override RallyStatus Status { get {return RallyStatus.Pending; }}

            public override Result AddVehicle(IAmVehicle vehicle)
            {
                return Result.Create(!Rally._vehicles.ContainsKey(vehicle.Id), ErrorMessages.VehicleAlreadyAdded)
                    .OnSuccess(() =>
                    {
                        Rally._vehicles.Add(vehicle.Id, vehicle);
                        Rally._vehiclesStillInRally.Add(vehicle.Id);
                        vehicle.FinishedRally += Rally.WhenVehicleFinishesRally;
                    });
            }

            public override Result RemoveVehicle(string vehicleId)
            {
                return Result.Create(
                        Rally._vehicles.TryGetValue(vehicleId, out var v), v, ErrorMessages.VehicleDoesNotExist)
                    .OnSuccess(vehicle =>
                    {
                        Rally._vehicles.Remove(vehicle.Id);
                        Rally._vehiclesStillInRally.Remove(vehicle.Id);
                        vehicle.FinishedRally -= Rally.WhenVehicleFinishesRally;
                    });
            }

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

            public override Result AddVehicle(IAmVehicle vehicle)
            {
                return Result.Fail("Rally has already started. Vehicle cannot be added.");
            }

            public override Result RemoveVehicle(string vehicleId)
            {
                return Result.Fail("Rally has already started. Vehicle cannot be removed.");
            }

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

            public override Result AddVehicle(IAmVehicle vehicle)
            {
                return Result.Fail("Rally has already started. Vehicle cannot be added.");
            }

            public override Result RemoveVehicle(string vehicleId)
            {
                return Result.Fail("Rally has already started. Vehicle cannot be removed.");
            }

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
