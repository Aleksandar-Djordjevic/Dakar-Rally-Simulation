using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DakarRallySimulation.Domain.VehicleHealthStatus;

namespace DakarRallySimulation.Domain.Vehicle
{
    public class Vehicle : IAmVehicle
    {
        public event EventHandler FinishedRally; 
        public event EventHandler Moved; 

        public string Id { get; }
        public VehicleType Type { get; }
        public string TeamName { get; }
        public string Model { get; }
        public DateTime ManufacturingDate { get; }

        public List<Malfunction> MalfunctionHistory { get; } = new List<Malfunction>();

        private readonly int _maxSpeed;
        private readonly TimeSpan _repairmentDuration;
        private readonly IProvideHealthStatus _healtStatusProvider;
        private readonly int _simulationResolutionTimeInSeconds;

        public decimal Distance { get; private set; }
        public DateTime? FinishedAt { get; private set; }
        public VehicleStatus Status { get; private set; }

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
                Malfunctions = MalfunctionHistory,
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
                OnMoved();

                if (Distance >= rally.Distance)
                {
                    Status = VehicleStatus.Finished;
                    FinishedAt = DateTime.UtcNow;
                    break;
                }

                switch (_healtStatusProvider.GetHealtStatus())
                {
                    case HealthStatus.HeavyMalfunction:
                        MalfunctionHistory.Add(Malfunction.CreateHeavy());
                        Status = VehicleStatus.Broken;
                        break;
                    case HealthStatus.LightMalfunction:
                        MalfunctionHistory.Add(Malfunction.CreateLight());
                        await Repair();
                        break;
                    case HealthStatus.WorkingProperly:
                    default:
                        break;
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

        protected virtual void OnMoved()
        {
            Moved?.Invoke(this, EventArgs.Empty);
        }

        
    }
}
