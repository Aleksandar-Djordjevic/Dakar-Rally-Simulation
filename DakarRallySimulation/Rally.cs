using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DakarRallySimulation
{
    public class Rally
    {
        public int Year { get; }
        public int Distance { get; }
        public bool IsFinished { get; private set; }
        public readonly List<IAmVehicle> Vehicles = new List<IAmVehicle>();

        private RallyState _state;
        private HashSet<string> _vehiclesStillInRally;

        public Rally(int year, int distance)
        {
            Year = year;
            Distance = distance;
            _state = new RallyPending(this);
        }

        public OperationResult<Rally, string> AddVehicle(IAmVehicle vehicle)
        {
            return _state.AddVehicle(vehicle);
        }

        public OperationResult<Rally, string> RemoveVehicle(string vehicleId)
        {
            return _state.RemoveVehicle(vehicleId);
        }

        public OperationResult<Rally, string> Start()
        {
            return _state.Start();
        }

        private void VehicleFinishedRally(object sender, string vehicleId)
        {
            _state.VehicleFinishedRally(vehicleId);
        }

        private abstract class RallyState
        {
            protected readonly Rally Rally;

            public RallyState(Rally rally)
            {
                Rally = rally;
            }

            public abstract OperationResult<Rally, string> AddVehicle(IAmVehicle vehicle);
            public abstract OperationResult<Rally, string> RemoveVehicle(string vehicleId);
            public abstract OperationResult<Rally, string> Start();

            public abstract void VehicleFinishedRally(string vehicleId);
        }

        private class RallyPending : RallyState
        {
            public RallyPending(Rally rally) : base (rally) { }

            public override OperationResult<Rally, string> AddVehicle(IAmVehicle vehicle)
            {
                if (Rally.Vehicles.Any(alreadyAdded => alreadyAdded.Id == vehicle.Id))
                    return OperationResult<Rally, string>.Failed("Vehicle already added to the rally.");

                Rally.Vehicles.Add(vehicle);
                return OperationResult<Rally, string>.Done(Rally);
            }

            public override OperationResult<Rally, string> RemoveVehicle(string vehicleId)
            {
                if (Rally.Vehicles.RemoveAll(vehicle => vehicle.Id == vehicleId) > 0)
                    return OperationResult<Rally, string>.Done(Rally);
                else
                    return OperationResult<Rally, string>.Failed("Vehicle does not exist.");
            }

            public override OperationResult<Rally, string> Start()
            {
                if (Rally.Vehicles.Any())
                {
                    Rally._vehiclesStillInRally = new HashSet<string>(Rally.Vehicles.Select(vehicle => vehicle.Id));
                    Rally.Vehicles.ForEach(vehicle => vehicle.FinishedRally += Rally.VehicleFinishedRally);
                    Rally._state = new RallyRunning(Rally);
                    Rally.Vehicles.ForEach(vehicle => vehicle.StartRally(Rally));
                }
                else
                {
                    Rally._state = new RallyFinished(Rally);
                }
                return OperationResult<Rally, string>.Done(Rally);
            }

            public override void VehicleFinishedRally(string vehicleId)
            {
                // log error
            }
        }

        private class RallyRunning : RallyState
        {
            public RallyRunning(Rally rally) : base (rally) { }

            public override OperationResult<Rally, string> AddVehicle(IAmVehicle vehicle)
            {
                return OperationResult<Rally, string>.Failed("Rally has already started. Vehicle cannot be added.");
            }

            public override OperationResult<Rally, string> RemoveVehicle(string vehicleId)
            {
                return OperationResult<Rally, string>.Failed("Rally has already started. Vehicle cannot be removed.");
            }

            public override OperationResult<Rally, string> Start()
            {
                return OperationResult<Rally, string>.Failed("Rally is already started.");
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

            public override OperationResult<Rally, string> AddVehicle(IAmVehicle vehicle)
            {
                return OperationResult<Rally, string>.Failed("Rally has already started. Vehicle cannot be added.");
            }

            public override OperationResult<Rally, string> RemoveVehicle(string vehicleId)
            {
                return OperationResult<Rally, string>.Failed("Rally has already started. Vehicle cannot be removed.");
            }

            public override OperationResult<Rally, string> Start()
            {
                return OperationResult<Rally, string>.Failed("Rally is already started.");
            }

            public override void VehicleFinishedRally(string vehicleId)
            {
                // log error
            }
        }
    }

}
