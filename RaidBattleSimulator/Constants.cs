using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaidBattleSimulator
{
    public static class Constants
    {
        public static class SetBonus
        {
            public const double Speed = 0.12d;
            public const double Perception = 0.05d;
        }

        public static class TurnMeter
        {
            public const double Full = 1.0d;

            public static double DeltaPerTick(double effectiveSpeed)
            {
                //const double magicSpeedNumber = 10000 / 7;
                const double magicSpeedNumber = 1428.571429d;
                return effectiveSpeed / magicSpeedNumber;
            }
        }
    }
}
