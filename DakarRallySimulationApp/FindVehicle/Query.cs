using System;
using System.Collections.Generic;
using System.Text;

namespace DakarRallySimulation.App.FindVehicle
{
    public class Query
    {
        public string TeamName { get; set; }
        public string Model { get; set; }
        public DateTime? ManufacturingDateFrom { get; set; }
        public DateTime? ManufacturingDateTo { get; set; }
        public VehicleStatus? Status { get; set; }
        public decimal? DistanceFrom { get; set; }
        public decimal? DistanceTo { get; set; }
        public SortBy? SortBy { get; set; }
        public SortDirection? SortDirection { get; set; }
    }

    public enum SortBy
    {
        TeamName,
        Model,
        ManufacturingDate,
        Distance
    }

    public enum SortDirection
    {
        Up,
        Down
    }
}
