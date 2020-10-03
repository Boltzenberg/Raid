using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaidLib.DataModel
{
    public class BuffToApply
    {
        public Constants.Buff Buff { get; private set; }
        public int Duration { get; private set; }
        public Constants.Target Target { get; private set; }

        public BuffToApply(Constants.Buff buff, int duration, Constants.Target target)
        {
            this.Buff = buff;
            this.Duration = duration;
            this.Target = target;
        }
    }
}
