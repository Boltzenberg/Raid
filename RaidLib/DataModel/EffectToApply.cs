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

        public EffectToApply(Constants.Effect effect, Constants.Target target)
        {
            this.Effect = effect;
            this.Target = target;
        }
    }
}
