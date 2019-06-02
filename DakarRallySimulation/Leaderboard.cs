﻿using System;
using System.Collections.Generic;
using DakarRallySimulation.Domain.Vehicle;

namespace DakarRallySimulation.Domain
{
    public class Leaderboard
    {
        private List<IAmVehicle> _vehicles = new List<IAmVehicle>();

        public Leaderboard(Rally rally)
        {
            rally.VehicleAdded += WhenVehicleIsAdded;
            rally.VehicleRemoved += WhenVehicleIsRemoved;
        }

        public void GetAllVehicles()
        {

        }

        public void GetVehiclesByType()
        {

        }

        private void WhenVehicleIsAdded(object sender, IAmVehicle vehicle)
        {
            vehicle.Moved += WhenVehicleMoves;
            _vehicles.Add(vehicle);
            RefreshBoard();
        }

        private void WhenVehicleIsRemoved(object sender, IAmVehicle vehicle)
        {
            vehicle.Moved -= WhenVehicleMoves;
            _vehicles.Remove(vehicle);
            RefreshBoard();
        }

        private void WhenVehicleMoves(object sender, EventArgs e)
        {
            RefreshBoard();
        }

        private void RefreshBoard()
        {
            _vehicles.Sort();
        }
    }
}
