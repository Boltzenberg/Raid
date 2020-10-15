using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaidLib.DataModel.Champions
{
    public static class Aothar
    {
        public static Champion Create(int uiSpeed, int speedSets)
        {
            List<Skill> skills = new List<Skill>();
            skills.Add(new Skill("Rage", Constants.SkillId.A1, 0, new TurnAction(2, Constants.Target.OneEnemy, null, null, null)));
            skills.Add(new Skill("Holy Flame", Constants.SkillId.A2, 3, new TurnAction(4, Constants.Target.OneEnemy, null, null, null)));
            skills.Add(new Skill("Brand", Constants.SkillId.A3, 3, TurnAction.AttackOneEnemy()));

            return new Champion("Aothar", 92, uiSpeed, speedSets, skills);
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
