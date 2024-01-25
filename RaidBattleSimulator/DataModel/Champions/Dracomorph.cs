using System.Collections.Generic;

namespace RaidBattleSimulator.DataModel.Champions
{
    public static class Dracomorph
    {
        public static ChampionBase Create(int uiSpeed, int speedSets, int perceptionSets, double speedAuraPercentage)
        {
            List<Skill> skills = new List<Skill>();
            skills.Add(new Skill("Seeping Pain", Constants.SkillId.A1, 0, TurnAction.AttackOneEnemy()));
            skills.Add(new Skill("Poison Jaws", Constants.SkillId.A2, 3, new TurnAction(4, Constants.Target.OneEnemy, null, null, null)));
            skills.Add(new Skill("Baleful Eye", Constants.SkillId.A3, 3, new TurnAction(0, Constants.Target.OneEnemy, null, null, null)));

            return new ChampionBase("Dracomorph", 98, uiSpeed, speedSets, perceptionSets, speedAuraPercentage, skills);
        }
    }
}
