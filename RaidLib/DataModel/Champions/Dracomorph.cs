using System.Collections.Generic;

namespace RaidLib.DataModel.Champions
{
    public static class Dracomorph
    {
        public static Champion Create(int uiSpeed, int speedSets, int perceptionSets)
        {
            List<Skill> skills = new List<Skill>();
            skills.Add(new Skill("Seeping Pain", Constants.SkillId.A1, 0, TurnAction.AttackOneEnemy()));
            skills.Add(new Skill("Poison Jaws", Constants.SkillId.A2, 3, new TurnAction(4, Constants.Target.OneEnemy, null, null, null)));
            skills.Add(new Skill("Baleful Eye", Constants.SkillId.A3, 3, new TurnAction(0, Constants.Target.OneEnemy, null, null, null)));

            return new Champion("Dracomorph", 98, uiSpeed, speedSets, perceptionSets, skills);
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
