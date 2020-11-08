using System.Collections.Generic;

namespace RaidLib.DataModel.Champions
{
    public static class KreelaWitchArm
    {
        private static List<Skill> GetSkills()
        {
            List<Skill> skills = new List<Skill>();
            skills.Add(new Skill("Witchlight Barrier", Constants.SkillId.A1, 0, TurnAction.AttackOneEnemy()));
            skills.Add(
                new Skill(
                    "Binding Glow", 
                    Constants.SkillId.A2, 
                    3, 
                    new TurnAction(
                        1, 
                        Constants.Target.OneEnemy, 
                        new List<EffectToApply>() 
                        { 
                            new EffectToApply(Constants.Effect.AllyAttack, Constants.Target.ThreeRandomAllies, Constants.TimeInTurn.End) 
                        }, 
                        null, 
                        null)));
            skills.Add(new Skill("War Weirding", Constants.SkillId.A3, 4, TurnAction.AttackAllEnemies()));

            return skills;
        }

        public static Champion Create(double effectiveSpeed)
        {
            return new Champion("Kreela Witch-Arm", effectiveSpeed, GetSkills());
        }

        public static Champion Create(int uiSpeed, int speedSets, int perceptionSets)
        {
            return new Champion("Kreela Witch-Arm", 99, uiSpeed, speedSets, perceptionSets, GetSkills());
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
