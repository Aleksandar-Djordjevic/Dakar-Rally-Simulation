using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Threading.Tasks;

namespace DakarRallySimulation.Domain
{
    public class Vehicle : IAmVehicle
    {
        public event EventHandler FinishedRally; 

        public string Id { get; }
        public VehicleType Type { get; }
        public string TeamName { get; }
        public string Model { get; }
        public DateTime ManufacturingDate { get; }
        public decimal Distance { get; private set; }
        public DateTime? FinishedAt { get; private set; }
        public VehicleStatus Status { get; private set; }
        ImmutableList<Malfunction> IAmVehicle.MalfunctionHistory => _malfunctionHistory.ToImmutableList();

        private readonly int _maxSpeed;
        private readonly TimeSpan _repairmentDuration;
        private readonly IProvideHealthStatus _healtStatusProvider;
        private readonly int _simulationResolutionTimeInSeconds;
        private readonly List<Malfunction> _malfunctionHistory;

        public Vehicle(VehicleType type, string id, string teamName, string model, DateTime manufacturingDate, int maxSpeed,
            TimeSpan repairmentDuration, int simulationResolutionTimeInSeconds, IProvideHealthStatus healtStatusProvider)
        {
            Type = type;
            Id = id;
            TeamName = teamName;
            Model = model;
            ManufacturingDate = manufacturingDate;
            _maxSpeed = maxSpeed;
            _repairmentDuration = repairmentDuration;
            _simulationResolutionTimeInSeconds = simulationResolutionTimeInSeconds;
            _malfunctionHistory = new List<Malfunction>();
            _healtStatusProvider = healtStatusProvider;
            Distance = 0;
            Status = VehicleStatus.WaitingStart;
        }

        public void StartRally(Rally rally)
        {
            if (Status != VehicleStatus.WaitingStart)
            {
                throw new InvalidOperationException("Vehicle has already started rally.");
            }

            GoGo(rally);
        }

        public VehicleStatistics GetStatistics()
        {
            return new VehicleStatistics
            {
                Status = Status,
                DistanceFromStart = Distance,
                Malfunctions = _malfunctionHistory,
                FinishTime = FinishedAt
            };
        }

        private async Task GoGo(Rally rally)
        {
            Status = VehicleStatus.Running;

            while (Status != VehicleStatus.Broken)
            {
                await Task.Delay(TimeSpan.FromSeconds(_simulationResolutionTimeInSeconds));
                Distance += (decimal)(_maxSpeed * _simulationResolutionTimeInSeconds) / 3600;

                if (Distance >= rally.Distance)
                {
                    Status = VehicleStatus.Finished;
                    FinishedAt = DateTime.UtcNow;
                    break;
                }

                if (_healtStatusProvider.GetHealtStatus() == HealthStatus.HeavyMalfunction)
                {
                    _malfunctionHistory.Add(Malfunction.CreateHeavy());
                    Status = VehicleStatus.Broken;
                }
                else if (_healtStatusProvider.GetHealtStatus() == HealthStatus.LightMalfunction)
                {
                    _malfunctionHistory.Add(Malfunction.CreateLight());
                    await Repair();
                }
            }

            OnVehicleFinishedRally();
        }

        private async Task Repair()
        {
            Status = VehicleStatus.Repairing;
            await Task.Delay(_repairmentDuration);
            Status = VehicleStatus.Running;
        }

        protected virtual void OnVehicleFinishedRally()
        {
            FinishedRally?.Invoke(this, EventArgs.Empty);
        }
    }
}
