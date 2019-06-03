using System;
using DakarRallySimulation.Domain.Vehicle;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace DakarRallySimulation.App.GetLeaderboard
{
    public class Vehicle : IComparable
    {
        public string Id { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public VehicleType Type { get; set; }
        public decimal DistanceFromStart { get; set; }
        public DateTime? FinishedAt { get; set; }

        public int CompareTo(object obj)
        {
            var other = (Vehicle)obj;
            if (FinishedAt != null & other.FinishedAt != null)
            {
                return FinishedAt < other.FinishedAt ? -1 : 1;
            }
            if (FinishedAt == null & other.FinishedAt == null)
            {
                return DistanceFromStart > other.DistanceFromStart ? -1 : 1;
            }

            return FinishedAt != null ? -1 : 1;
        }
    }
}