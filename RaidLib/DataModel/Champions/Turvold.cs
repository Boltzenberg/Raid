using System.Collections.Generic;

namespace RaidLib.DataModel.Champions
{
    public static class Turvold
    {
        public static Champion Create(int uiSpeed, int speedSets, int perceptionSets)
        {
            List<Skill> skills = new List<Skill>();
            skills.Add(new Skill("Crackling Blade", Constants.SkillId.A1, 0, new TurnAction(2, Constants.Target.OneEnemy, null, null, null)));
            skills.Add(
                new Skill(
                    "Ancestor's Power", 
                    Constants.SkillId.A2, 
                    3, 
                    new TurnAction(
                        0, 
                        Constants.Target.None, 
                        new List<EffectToApply>() 
                        { 
                            new EffectToApply(Constants.Effect.ExtraTurn, Constants.Target.Self, Constants.TimeInTurn.End) 
                        },
                        new List<BuffToApply>()
                        {
                            new BuffToApply(Constants.Buff.IncreaseAttack50, 2, Constants.Target.Self),
                            new BuffToApply(Constants.Buff.IncreaseCritRate30, 2, Constants.Target.Self),
                            new BuffToApply(Constants.Buff.IncreaseSpeed30, 2, Constants.Target.Self)
                        },
                        null)));
            skills.Add(new Skill("Juggernaut", Constants.SkillId.A3, 3, new TurnAction(2, Constants.Target.OneEnemy, null, null, null)));

            return new Champion("Turvold", 93, uiSpeed, speedSets, perceptionSets, skills);
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
