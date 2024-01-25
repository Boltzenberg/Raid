using System.Collections.Generic;

namespace RaidBattleSimulator.DataModel.Champions
{
    public static class Seeker
    {
        private static List<Skill> GetSkills()
        {
            List<Skill> skills = new List<Skill>();
            skills.Add(new Skill("Devour", Constants.SkillId.A1, 0, TurnAction.AttackOneEnemy()));
            skills.Add(new Skill(
                "Tailwind",
                Constants.SkillId.A2,
                3,
                new TurnAction(0, Constants.Target.None, new List<EffectToApply>() { 
                    new EffectToApply(Constants.Effect.FillTurnMeterBy30Percent, Constants.Target.FullTeam, Constants.TimeInTurn.End),
                    new EffectToApply(Constants.Effect.ExtraTurn, Constants.Target.Self, Constants.TimeInTurn.End)
                }, null, null)));

            return skills;
        }

        public static ChampionBase Create(int uiSpeed, int speedSets, int perceptionSets, double speedAuraPercentage)
        {
            return new ChampionBase("Seeker", 103, uiSpeed, speedSets, perceptionSets, speedAuraPercentage, GetSkills());
        }
    }
}
