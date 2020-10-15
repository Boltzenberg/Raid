using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaidLib.DataModel.Champions
{
    public static class RhazinScarhide
    {
        public static Champion Create(int uiSpeed, int speedSets)
        {
            List<Skill> skills = new List<Skill>();
            skills.Add(new Skill("Bone Sword", Constants.SkillId.A1, 0, new TurnAction(3, Constants.Target.OneEnemy, null, null, null)));
            skills.Add(new Skill("Shear", Constants.SkillId.A2, 3, TurnAction.AttackOneEnemy()));
            skills.Add(new Skill("Bog Down", Constants.SkillId.A3, 6, TurnAction.AttackAllEnemies()));

            return new Champion("Rhazin Scarhide", 91, uiSpeed, speedSets, skills);
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
