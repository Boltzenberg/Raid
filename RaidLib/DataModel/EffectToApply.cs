using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaidLib.DataModel
{
    public class EffectToApply
    {
        public Constants.Effect Effect { get; private set; }
        public Constants.Target Target { get; private set; }
        public Constants.TimeInTurn WhenToApply { get; private set; }

        public EffectToApply(Constants.Effect effect, Constants.Target target, Constants.TimeInTurn whenToApply)
        {
            this.Effect = effect;
            this.Target = target;
            this.WhenToApply = whenToApply;
        }
    }
}
