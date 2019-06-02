using System;
using System.Collections.Generic;
using CSharpFunctionalExtensions;
using DakarRallySimulation.Domain.Vehicle;

namespace DakarRallySimulation.Domain
{
    public interface IAmRally
    {
        event EventHandler Started;
        event EventHandler<IAmVehicle> VehicleAdded;
        event EventHandler<IAmVehicle> VehicleRemoved;

        int Distance { get; }
        string Id { get; }
        bool IsFinished { get; }
        int Year { get; }
        RallyStatus GetStatus();
        Dictionary<string, IAmVehicle> Vehicles { get; } 

        Result AddVehicle(IAmVehicle vehicle);
        Result RemoveVehicle(string vehicleId);
        Result Start();
    }
}