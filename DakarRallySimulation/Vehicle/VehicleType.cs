using System.Collections.Generic;

namespace DakarRallySimulation.Domain.Vehicle
{
    public class VehicleType
    {
        public string Name { get; }

        private VehicleType(string name)
        {
            Name = name;
        }

        public static VehicleType Car => new VehicleType("Car");
        public static VehicleType Truck => new VehicleType("Truck");
        public static VehicleType Motorcycle => new VehicleType("Motorcycle");

        public override bool Equals(object obj)
        {
            var type = obj as VehicleType;
            return type != null &&
                   Name == type.Name;
        }

        public override int GetHashCode()
        {
            return 539060726 + EqualityComparer<string>.Default.GetHashCode(Name);
        }

        public static bool operator ==(VehicleType object1, VehicleType object2)
        {
            if (ReferenceEquals(object1, null))
            {
                return ReferenceEquals(object2, null);
            }
            return object1.Equals(object2);
        }

        public static bool operator !=(VehicleType object1, VehicleType object2)
        {
            if (ReferenceEquals(object1, null))
            {
                return !ReferenceEquals(object2, null);
            }
            return !object1.Equals(object2);
        }
    }
}
