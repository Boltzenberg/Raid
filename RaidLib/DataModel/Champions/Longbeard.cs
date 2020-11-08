using System.Collections.Generic;

namespace RaidLib.DataModel.Champions
{
    public static class Longbeard
    {
        private static List<Skill> GetSkills()
        {
            List<Skill> skills = new List<Skill>();
            skills.Add(new Skill("Unstoppable Force", Constants.SkillId.A1, 0, TurnAction.AttackOneEnemy()));
            skills.Add(new Skill("Brittleness Curse", Constants.SkillId.A2, 2, TurnAction.AttackOneEnemy()));
            skills.Add(
                new Skill(
                    "Horde's Fury", 
                    Constants.SkillId.A3, 
                    4,
                    new TurnAction(
                        1, 
                        Constants.Target.OneEnemy, 
                        new List<EffectToApply>() 
                        { 
                            new EffectToApply(Constants.Effect.AllyAttack, Constants.Target.AllAllies, Constants.TimeInTurn.End) 
                        },
                        null,
                        null)));

            return skills;
        }

        public static Champion Create(double effectiveSpeed)
        {
            return new Champion("Longbeard", effectiveSpeed, GetSkills());
        }

        public static Champion Create(int uiSpeed, int speedSets, int perceptionSets)
        {
            return new Champion("Longbeard", 97, uiSpeed, speedSets, perceptionSets, GetSkills());
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
