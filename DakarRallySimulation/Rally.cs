using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;

namespace DakarRallySimulation
{
    public class Rally : IAmRally
    {
        public event EventHandler Started;
        public event EventHandler<IAmVehicle> VehicleAdded;
        public event EventHandler<IAmVehicle> VehicleRemoved;

        public string Id { get; }
        public int Year { get; }
        public int Distance { get; }
        public bool IsFinished { get; private set; }
        public readonly List<IAmVehicle> Vehicles = new List<IAmVehicle>();

        private RallyState _state;
        private HashSet<string> _vehiclesStillInRally = new HashSet<string>();

        public Rally(int year, int distance)
        {
            Id = year.ToString();
            Year = year;
            Distance = distance;
            _state = new RallyPending(this);
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

            public override Result AddVehicle(IAmVehicle vehicle)
            {
                if (Rally.Vehicles.Any(alreadyAdded => alreadyAdded.Id == vehicle.Id))
                    return Result.Fail("Vehicle with same ID already added to the rally.");

                Rally.Vehicles.Add(vehicle);
                Rally._vehiclesStillInRally.Add(vehicle.Id);
                vehicle.FinishedRally += Rally.WhenVehicleFinishesRally;

                return Result.Ok();
            }

            public override Result RemoveVehicle(string vehicleId)
            {
                var vehicle = Rally.Vehicles.Find(v => v.Id == vehicleId);

                if (vehicle == null)
                    return Result.Fail("Vehicle does not exist.");

                Rally.Vehicles.Remove(vehicle);
                Rally._vehiclesStillInRally.Remove(vehicle.Id);
                vehicle.FinishedRally -= Rally.WhenVehicleFinishesRally;

                return Result.Ok();
            }

            public override Result Start()
            {
                if (Rally.Vehicles.Any())
                {
                    Rally._state = new RallyRunning(Rally);
                    Rally.OnStarted();
                    Rally.Vehicles.ForEach(vehicle => vehicle.StartRally(Rally));
                }
                else
                {
                    Rally._state = new RallyFinished(Rally);
                }
                return Result.Ok();
            }

            public override void VehicleFinishedRally(string vehicleId)
            {
                // log error
            }
        }

        private class RallyRunning : RallyState
        {
            public RallyRunning(Rally rally) : base (rally) { }

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
