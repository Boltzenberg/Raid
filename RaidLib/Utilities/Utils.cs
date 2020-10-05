using RaidLib.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaidLib.Utilities
{
    public static class Utils
    {
        public static Champion FindSlowestChampion(List<Champion> champions)
        {
            Champion slowest = champions[0];
            foreach (Champion champion in champions)
            {
                if (champion.EffectiveSpeed < slowest.EffectiveSpeed)
                {
                    slowest = champion;
                }
            }

            return slowest;
        }
    }
}
