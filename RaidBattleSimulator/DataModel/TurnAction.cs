using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaidBattleSimulator.DataModel
{
    public class TurnAction
    {
        public int AttackCount { get; private set; }
        public Constants.Target AttackTarget { get; private set; }

        public List<EffectToApply> EffectsToApply { get; private set; }
        public List<BuffToApply> BuffsToApply { get; private set; }
        public List<DebuffToApply> DebuffsToApply { get; private set; }

        public TurnAction(int attackCount, Constants.Target attackTarget, List<EffectToApply> effectsToApply, List<BuffToApply> buffsToApply, List<DebuffToApply> debuffsToApply)
        {
            this.AttackCount = attackCount;
            this.AttackTarget = attackTarget;
            this.EffectsToApply = effectsToApply;
            this.BuffsToApply = buffsToApply;
            this.DebuffsToApply = debuffsToApply;
        }

        public static TurnAction AttackOneEnemy()
        {
            return new TurnAction(1, Constants.Target.OneEnemy, null, null, null);
        }

        public static TurnAction AttackAllEnemies()
        {
            return new TurnAction(1, Constants.Target.AllEnemies, null, null, null);
        }
    }
}
