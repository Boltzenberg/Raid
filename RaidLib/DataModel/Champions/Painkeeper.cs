using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaidLib.DataModel.Champions
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

        public static Champion Create(double effectiveSpeed)
        {
            return new Champion("Painkeeper", 102, effectiveSpeed, GetSkills());
        }

        public static Champion Create(int uiSpeed, int speedSets, int perceptionSets)
        {
            return new Champion("Painkeeper", 102, uiSpeed, speedSets, perceptionSets, GetSkills());
        }

        public static List<Constants.SkillId> AISkills
        {
            get
            {
                return new List<Constants.SkillId>()
                {
                    Constants.SkillId.A3,
                    Constants.SkillId.A2,
                    Constants.SkillId.A1,
                };
            }
        }
    }
}
