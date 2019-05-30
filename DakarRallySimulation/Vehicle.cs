using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DakarRallySimulation
{
    public class Vehicle : IAmVehicle
    {
        public event EventHandler FinishedRally; 

        public string Id { get; }
        public string TeamName { get; }
        public string Model { get; }
        public DateTime ManufacturingDate { get; }

        private readonly List<Malfunction> _malfunctionHistory = new List<Malfunction>();

        private readonly int _maxSpeed;
        private readonly TimeSpan _repairmentDuration;
        private readonly IProvideHealtStatus _healtStatusProvider;
        private readonly int _simulationResolutionTimeInSeconds;
        private DateTime? _finishTime;

        public decimal Distance { get; private set; }

        public VehicleState State { get; private set; }

        public Vehicle(string id, string teamName, string model, DateTime manufacturingDate, int maxSpeed,
            TimeSpan repairmentDuration, int simulationResolutionTimeInSeconds, IProvideHealtStatus healtStatusProvider)
        {
            Id = id;
            TeamName = teamName;
            Model = model;
            ManufacturingDate = manufacturingDate;
            _maxSpeed = maxSpeed;
            _repairmentDuration = repairmentDuration;
            _simulationResolutionTimeInSeconds = simulationResolutionTimeInSeconds;
            _healtStatusProvider = healtStatusProvider;
            Distance = 0;
            State = VehicleState.WaitingStart;
        }

        public void StartRally(Rally rally)
        {
            if (State != VehicleState.WaitingStart)
            {
                throw new InvalidOperationException("Vehicle has already started rally.");
            }

            GoGo(rally);
        }

        public VehicleStatistics GetStatistics()
        {
            return new VehicleStatistics
            {
                Status = State,
                DistanceFromStart = Distance,
                Malfunctions = _malfunctionHistory,
                FinishTime = _finishTime
            };
        }

        private async Task GoGo(Rally rally)
        {
            State = VehicleState.Running;

            while (State != VehicleState.Broken)
            {
                await Task.Delay(TimeSpan.FromSeconds(_simulationResolutionTimeInSeconds));
                Distance += (decimal)(_maxSpeed * _simulationResolutionTimeInSeconds) / 3600;
                if (Distance >= rally.Distance)
                {
                    State = VehicleState.Finished;
                    _finishTime = DateTime.UtcNow;
                    break;
                }

                switch (_healtStatusProvider.GetHealtStatus())
                {
                    case HealtStatus.HeavyMalfunction:
                        _malfunctionHistory.Add(Malfunction.CreateHeavy());
                        State = VehicleState.Broken;
                        break;
                    case HealtStatus.LightMalfunction:
                        _malfunctionHistory.Add(Malfunction.CreateLight());
                        await Repair();
                        break;
                    case HealtStatus.WorkingProperly:
                    default:
                        break;
                }
            }

            OnVehicleFinishedRally();
        }

        private async Task Repair()
        {
            State = VehicleState.Repairing;
            await Task.Delay(_repairmentDuration);
            State = VehicleState.Running;
        }

        protected virtual void OnVehicleFinishedRally()
        {
            FinishedRally?.Invoke(this, EventArgs.Empty);
        }
    }

    public enum VehicleState
    {
        WaitingStart,
        Running,
        Repairing,
        Broken,
        Finished
    }
}
