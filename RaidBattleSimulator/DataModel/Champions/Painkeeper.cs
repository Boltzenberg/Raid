using System.Collections.Generic;

namespace RaidBattleSimulator.DataModel.Champions
{
    public static class Painkeeper
    {
        private static List<Skill> GetSkills()
        {
            List<Skill> skills = new List<Skill>();
            skills.Add(new Skill(
                "Unflagging Advance",
                Constants.SkillId.A1,
                0,
                new TurnAction(1, Constants.Target.OneEnemy, new List<EffectToApply>() { new EffectToApply(Constants.Effect.FillTurnMeterBy10Percent, Constants.Target.Self, Constants.TimeInTurn.End) }, null, null)));
            skills.Add(new Skill("Spectacular Sweep", Constants.SkillId.A2, 4, TurnAction.AttackOneEnemy()));
            skills.Add(new Skill(
                "Combat Tactics",
                Constants.SkillId.A3,
                4,
                new TurnAction(0, Constants.Target.None, new List<EffectToApply>() { new EffectToApply(Constants.Effect.ReduceSkillCooldownBy1, Constants.Target.AllAllies, Constants.TimeInTurn.End) }, null, null)));

            return skills;
        }

        public static ChampionBase Create(int uiSpeed, int speedSets, int perceptionSets, double speedAuraPercentage)
        {
            return new ChampionBase("Painkeeper", 102, uiSpeed, speedSets, perceptionSets, speedAuraPercentage, GetSkills());
        }
    }
}
