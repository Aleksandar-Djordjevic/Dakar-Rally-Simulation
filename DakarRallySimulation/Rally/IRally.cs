using System;
using System.Collections.Immutable;
using CSharpFunctionalExtensions;

namespace DakarRallySimulation.Domain
{
    public interface IAmRally
    {
        event EventHandler Started;

        string Id { get; }
        int Year { get; }
        int Distance { get; }
        bool IsFinished { get; }
        RallyStatus GetStatus();
        ImmutableDictionary<string, IAmVehicle> Vehicles { get; } 

        Result AddVehicle(IAmVehicle vehicle);
        Result RemoveVehicle(string vehicleId);
        Result Start();
    }
}