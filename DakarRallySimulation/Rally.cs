using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DakarRallySimulation
{
    public class Rally
    {
        public readonly int Distance;
        private readonly int _year;
        public readonly List<Vehicle> Vehicles = new List<Vehicle>();
        private RallyState _state;

        public Rally(int year, int distance)
        {
            _year = year;
            Distance = distance;
            _state = new RallyPending(this);
        }

        public OperationResult<Rally, string> AddVehicle(Vehicle vehicle)
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

        private abstract class RallyState
        {
            protected Rally Rally;

            public RallyState(Rally rally)
            {
                Rally = rally;
            }

            public abstract OperationResult<Rally, string> AddVehicle(Vehicle vehicle);
            public abstract OperationResult<Rally, string> RemoveVehicle(string vehicleId);
            public abstract OperationResult<Rally, string> Start();
        }

        private class RallyPending : RallyState
        {
            public RallyPending(Rally rally) : base (rally) { }

            public override OperationResult<Rally, string> AddVehicle(Vehicle vehicle)
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
                Rally._state = new RallyRunning(Rally);
                Task.WhenAll(Rally.Vehicles.Select(vehicle => vehicle.StartRally(Rally)).ToArray());
                Rally._state = new RallyFinished(Rally);
                return OperationResult<Rally, string>.Done(Rally);
            }
        }

        private class RallyRunning : RallyState
        {
            public RallyRunning(Rally rally) : base (rally) { }

            public override OperationResult<Rally, string> AddVehicle(Vehicle vehicle)
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
        }

        private class RallyFinished : RallyState
        {
            public RallyFinished(Rally rally) : base (rally) { }

            public override OperationResult<Rally, string> AddVehicle(Vehicle vehicle)
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
        }
    }

}
