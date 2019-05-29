using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DakarRallySimulation
{
    public class Vehicle
    {
        public string Id { get; private set; }
        public string TeamName { get; private set; }
        public string Model { get; private set; }
        public DateTime ManufacturingDate { get; private set; }

        private readonly List<Malfunction> _malfunctionHistory = new List<Malfunction>();

        private readonly int _maxSpeed;
        private readonly TimeSpan _repairmentDuration;
        private readonly IProvideHealtStatus _healtStatusProvider;
        private readonly int _simulationResolutionTimeInSeconds;
        private decimal _distance;
        private VehicleState _state;
        private DateTime? _finishTime;

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
            _distance = 0;
            _state = VehicleState.WaitingStart;
        }

        public Task StartRally(Rally rally)
        {
            if (_state != VehicleState.WaitingStart)
            {
                throw new InvalidOperationException("Vehicle has already started rally.");
            }

            return GoGo(rally);
        }

        public VehicleStatistics GetStatistics()
        {
            return new VehicleStatistics
            {
                Status = _state,
                DistanceFromStart = _distance,
                Malfunctions = _malfunctionHistory,
                FinishTime = _finishTime
            };
        }

        private async Task GoGo(Rally rally)
        {
            _state = VehicleState.Running;

            while (_state != VehicleState.Broken)
            {
                await Task.Delay(TimeSpan.FromSeconds(_simulationResolutionTimeInSeconds));
                _distance += (decimal)(_maxSpeed * _simulationResolutionTimeInSeconds) / 3600;
                if (_distance >= rally.Distance)
                {
                    _state = VehicleState.Finished;
                    _finishTime = DateTime.UtcNow;
                    break;
                }

                switch (_healtStatusProvider.GetHealtStatus())
                {
                    case HealtStatus.HeavyMalfunction:
                        _malfunctionHistory.Add(Malfunction.CreateHeavy());
                        _state = VehicleState.Broken;
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
        }

        private async Task Repair()
        {
            _state = VehicleState.Repairing;
            await Task.Delay(_repairmentDuration);
            _state = VehicleState.Running;
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
