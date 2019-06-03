using System.Collections.Generic;

namespace DakarRallySimulation.Domain
{
    public class HealthStatus
    {
        public string Name { get; }

        private HealthStatus(string name)
        {
            Name = name;
        }

        public static HealthStatus WorkingProperly => new HealthStatus("WorkingProperly");
        public static HealthStatus LightMalfunction => new HealthStatus("LightMalfunction");
        public static HealthStatus HeavyMalfunction => new HealthStatus("HeavyMalfunction");

        public override bool Equals(object obj)
        {
            var status = obj as HealthStatus;
            return status != null &&
                   Name == status.Name;
        }

        public override int GetHashCode()
        {
            return 539060726 + EqualityComparer<string>.Default.GetHashCode(Name);
        }

        public static bool operator ==(HealthStatus object1, HealthStatus object2)
        {
            if (ReferenceEquals(object1, null))
            {
                return ReferenceEquals(object2, null);
            }
            return object1.Equals(object2);
        }

        public static bool operator !=(HealthStatus object1, HealthStatus object2)
        {
            if (ReferenceEquals(object1, null))
            {
                return !ReferenceEquals(object2, null);
            }
            return !object1.Equals(object2);
        }
    }
}
