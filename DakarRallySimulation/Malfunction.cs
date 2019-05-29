using System;

namespace DakarRallySimulation
{
    public class Malfunction
    {
        private DateTime _occuredOn;
        private bool _isHeavy;

        private Malfunction(bool isHeavy)
        {
            this._occuredOn = DateTime.UtcNow;
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