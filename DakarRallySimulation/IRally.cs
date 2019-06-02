using System;
using System.Collections.Generic;
using CSharpFunctionalExtensions;

namespace DakarRallySimulation.Domain
{
    public interface IAmRally : IRallyEvents
    {
        int Distance { get; }
        string Id { get; }
        bool IsFinished { get; }
        int Year { get; }
        Dictionary<string, IAmVehicle> Vehicles { get; } 

        Result AddVehicle(IAmVehicle vehicle);
        Result RemoveVehicle(string vehicleId);
        Result Start();
    }

    public interface IRallyEvents
    {
        event EventHandler Started;
        event EventHandler<IAmVehicle> VehicleAdded;
        event EventHandler<IAmVehicle> VehicleRemoved;
    }
}