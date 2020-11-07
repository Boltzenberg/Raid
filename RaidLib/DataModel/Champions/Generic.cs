using System.Collections.Generic;

namespace RaidLib.DataModel.Champions
{
    public static class Generic
    {
        public static Champion Create(string name, double effectiveSpeed)
        {
            List<Skill> skills = new List<Skill>();
            skills.Add(new Skill("A1", Constants.SkillId.A1, 0, TurnAction.AttackOneEnemy()));

            return new Champion(name, effectiveSpeed, skills);
        }

        public static Champion Create(string name, int baseSpeed, int uiSpeed, int speedSets, int perceptionSets)
        {
            List<Skill> skills = new List<Skill>();
            skills.Add(new Skill("A1", Constants.SkillId.A1, 0, TurnAction.AttackOneEnemy()));

            return new Champion(name, baseSpeed, uiSpeed, speedSets, perceptionSets, skills);
        }

        public static List<Constants.SkillId> AISkills
        {
            get
            {
                return new List<Constants.SkillId>()
                {
                    Constants.SkillId.A1,
                };
            }
        }
    }
}
