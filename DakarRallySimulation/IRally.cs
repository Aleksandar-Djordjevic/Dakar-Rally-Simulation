﻿using System;
using CSharpFunctionalExtensions;

namespace DakarRallySimulation
{
    public interface IAmRally
    {
        int Distance { get; }
        string Id { get; }
        bool IsFinished { get; }
        int Year { get; }

        event EventHandler Started;
        event EventHandler<IAmVehicle> VehicleAdded;
        event EventHandler<IAmVehicle> VehicleRemoved;

        Result AddVehicle(IAmVehicle vehicle);
        Result RemoveVehicle(string vehicleId);
        Result Start();
    }
}