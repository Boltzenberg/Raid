using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaidLib.DataModel.Champions
{
    public static class YagaTheInsatiable
    {
        public static Champion Create(int uiSpeed, int speedSets, int perceptionSets)
        {
            List<Skill> skills = new List<Skill>();
            skills.Add(new Skill("Virulent Cleaver", Constants.SkillId.A1, 0, TurnAction.AttackOneEnemy()));
            skills.Add(new Skill("Hungering Plague", Constants.SkillId.A2, 4, TurnAction.AttackOneEnemy()));
            skills.Add(new Skill("Rot From Within", Constants.SkillId.A3, 5, TurnAction.AttackOneEnemy()));

            return new Champion("Yaga The Insatiable", 95, uiSpeed, speedSets, perceptionSets, skills);
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
