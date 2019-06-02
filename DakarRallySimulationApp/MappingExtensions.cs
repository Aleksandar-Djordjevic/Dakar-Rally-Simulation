using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;
using DakarRallySimulation.App.GetRallyStatus;

namespace DakarRallySimulation.App
{
    public static class MappingExtensions
    {
        public static VehicleType ToDto(this Domain.Vehicle.VehicleType vehicleType)
        {
            if (vehicleType == Domain.Vehicle.VehicleType.Car)
                return VehicleType.Car;
            if (vehicleType == Domain.Vehicle.VehicleType.Truck)
                return VehicleType.Truck;
            if (vehicleType == Domain.Vehicle.VehicleType.Motorcycle)
                return VehicleType.Motorcycle;

            throw new ArgumentException();
        }

        public static VehicleStatus ToDto(this Domain.Vehicle.VehicleStatus vehicleStatus)
        {
            if (vehicleStatus == Domain.Vehicle.VehicleStatus.WaitingStart)
                return VehicleStatus.WaitingStart;
            if (vehicleStatus == Domain.Vehicle.VehicleStatus.Running)
                return VehicleStatus.Running;
            if (vehicleStatus == Domain.Vehicle.VehicleStatus.Repairing)
                return VehicleStatus.Repairing;
            if (vehicleStatus == Domain.Vehicle.VehicleStatus.Broken)
                return VehicleStatus.Broken;
            if (vehicleStatus == Domain.Vehicle.VehicleStatus.Finished)
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
