using System.Collections.Generic;

namespace RaidLib.DataModel.Champions
{
    public static class CatacombCouncilor
    {
        private static List<Skill> GetSkills()
        {
            List<Skill> skills = new List<Skill>();
            skills.Add(new Skill("Grotesque Strength", Constants.SkillId.A1, 0, TurnAction.AttackOneEnemy()));
            skills.Add(new Skill("Ghoulish Feeding", Constants.SkillId.A2, 3, new TurnAction(3, Constants.Target.OneEnemy, null, null, null)));
            skills.Add(
                new Skill(
                    "Army of Death", 
                    Constants.SkillId.A3, 
                    4,
                    new TurnAction(
                        1, 
                        Constants.Target.OneEnemy, 
                        new List<EffectToApply>() 
                        { 
                            new EffectToApply(Constants.Effect.AllyAttack, Constants.Target.ThreeRandomAllies, Constants.TimeInTurn.End) 
                        },
                        null,
                        null)));

            return skills;
        }

        public static Champion Create(double effectiveSpeed)
        {
            return new Champion("Catacomb Councilor", effectiveSpeed, GetSkills());
        }

        public static Champion Create(int uiSpeed, int speedSets, int perceptionSets)
        {
            return new Champion("Catacomb Councilor", 92, uiSpeed, speedSets, perceptionSets, GetSkills());
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
