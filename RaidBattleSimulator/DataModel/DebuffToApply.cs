using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaidBattleSimulator.DataModel
{
    public class DebuffToApply
    {
        public Constants.Debuff Debuff { get; private set; }
        public int Duration { get; private set; }
        public Constants.Target Target { get; private set; }

        public DebuffToApply(Constants.Debuff debuff, int duration, Constants.Target target)
        {
            this.Debuff = debuff;
            this.Duration = duration;
            this.Target = target;
        }
    }
}
