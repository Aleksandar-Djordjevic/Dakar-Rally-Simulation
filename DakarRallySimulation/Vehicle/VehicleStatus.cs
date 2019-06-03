using System.Collections.Generic;

namespace DakarRallySimulation.Domain
{
    public class VehicleStatus
    {
        public string Name { get; }

        private VehicleStatus(string name)
        {
            Name = name;
        }

        public static VehicleStatus WaitingStart => new VehicleStatus("WaitingStart");
        public static VehicleStatus Running => new VehicleStatus("Running");
        public static VehicleStatus Repairing => new VehicleStatus("Repairing");
        public static VehicleStatus Broken => new VehicleStatus("Broken");
        public static VehicleStatus Finished => new VehicleStatus("Finished");

        public override bool Equals(object obj)
        {
            var status = obj as VehicleStatus;
            return status != null &&
                   Name == status.Name;
        }

        public override int GetHashCode()
        {
            return 539060726 + EqualityComparer<string>.Default.GetHashCode(Name);
        }

        public static bool operator ==(VehicleStatus object1, VehicleStatus object2)
        {
            if (ReferenceEquals(object1, null))
            {
                return ReferenceEquals(object2, null);
            }
            return object1.Equals(object2);
        }

        public static bool operator !=(VehicleStatus object1, VehicleStatus object2)
        {
            if (ReferenceEquals(object1, null))
            {
                return !ReferenceEquals(object2, null);
            }
            return !object1.Equals(object2);
        }
    }
}