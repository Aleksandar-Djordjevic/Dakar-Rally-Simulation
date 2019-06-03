using System;
using DakarRallySimulation.App.GetRallyStatus;

namespace DakarRallySimulation.App
{
    public static class MappingExtensions
    {
        public static VehicleType ToDto(this Domain.VehicleType vehicleType)
        {
            if (vehicleType == Domain.VehicleType.Car)
                return VehicleType.Car;
            if (vehicleType == Domain.VehicleType.Truck)
                return VehicleType.Truck;
            if (vehicleType == Domain.VehicleType.Motorcycle)
                return VehicleType.Motorcycle;

            throw new ArgumentException();
        }

        public static VehicleStatus ToDto(this Domain.VehicleStatus vehicleStatus)
        {
            if (vehicleStatus == Domain.VehicleStatus.WaitingStart)
                return VehicleStatus.WaitingStart;
            if (vehicleStatus == Domain.VehicleStatus.Running)
                return VehicleStatus.Running;
            if (vehicleStatus == Domain.VehicleStatus.Repairing)
                return VehicleStatus.Repairing;
            if (vehicleStatus == Domain.VehicleStatus.Broken)
                return VehicleStatus.Broken;
            if (vehicleStatus == Domain.VehicleStatus.Finished)
                return VehicleStatus.Finished;

            throw new ArgumentException();
        }

        public static RallyStatus ToDto(this Domain.RallyStatus rallyStatus)
        {
            if (rallyStatus == Domain.RallyStatus.Pending)
                return RallyStatus.Pending;
            if (rallyStatus == Domain.RallyStatus.Running)
                return RallyStatus.Running;
            if (rallyStatus == Domain.RallyStatus.Finished)
                return RallyStatus.Finished;

            throw new ArgumentException();
        }
    }
}
