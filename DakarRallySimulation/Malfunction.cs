using System;

namespace DakarRallySimulation.Domain
{
    public class Malfunction
    {
        public DateTime OccuredOn { get; }
        public bool IsHeavy { get; }

        private Malfunction(bool isHeavy)
        {
            this.OccuredOn = DateTime.UtcNow;
            IsHeavy = isHeavy;
        }

        public static Malfunction CreateHeavy() {
            return new Malfunction(true);
        }
        public static Malfunction CreateLight()
        {
            return new Malfunction(false);
        }
    }
}