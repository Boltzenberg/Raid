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
        public static Champion FindSlowestChampion(IEnumerable<Champion> champions)
        {
            Champion slowest = champions.First();
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
