using System;
using System.Collections.Generic;
using System.Text;

namespace DakarRallySimulation.Domain
{
    public class RallyStatus
    {
        public string Name { get; }

        private RallyStatus(string name)
        {
            Name = name;
        }

        public static RallyStatus Pending => new RallyStatus("Pending");
        public static RallyStatus Running => new RallyStatus("Running");
        public static RallyStatus Finished => new RallyStatus("Finished");

        public override bool Equals(object obj)
        {
            var status = obj as RallyStatus;
            return status != null &&
                   Name == status.Name;
        }

        public override int GetHashCode()
        {
            return 539060726 + EqualityComparer<string>.Default.GetHashCode(Name);
        }

        public static bool operator ==(RallyStatus object1, RallyStatus object2)
        {
            if (ReferenceEquals(object1, null))
            {
                return ReferenceEquals(object2, null);
            }
            return object1.Equals(object2);
        }

        public static bool operator !=(RallyStatus object1, RallyStatus object2)
        {
            if (ReferenceEquals(object1, null))
            {
                return !ReferenceEquals(object2, null);
            }
            return !object1.Equals(object2);
        }
    }
}
