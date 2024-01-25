using RaidBattleSimulator.DataModel.Champions;
using System.Collections.Generic;

namespace RaidBattleSimulator
{
    public class TurnResult
    {
        public Constants.SkillId SkillUsed { get; private set; }
        public Dictionary<ChampionBase, double> TurnMetersBeforeTurn { get; private set; }
        public bool GrantedExtraTurn { get; private set; }

        public TurnResult(Constants.SkillId skillUsed, Dictionary<ChampionBase, double> turnMetersBeforeTurn, bool grantedExtraTurn)
        {
            this.SkillUsed = skillUsed;
            this.TurnMetersBeforeTurn = turnMetersBeforeTurn;
            this.GrantedExtraTurn = grantedExtraTurn;
        }
    }
}
