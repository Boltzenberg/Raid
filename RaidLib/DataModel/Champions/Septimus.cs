using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaidLib.DataModel.Champions
{
    public static class Septimus
    {
        private static List<Skill> GetSkills()
        {
            List<Skill> skills = new List<Skill>();
            skills.Add(new Skill("Behead", Constants.SkillId.A1, 0, TurnAction.AttackOneEnemy()));
            skills.Add(new Skill("Holy Sword", Constants.SkillId.A2, 4, TurnAction.AttackOneEnemy()));
            skills.Add(new Skill("Giant Killer", Constants.SkillId.P1, 0, TurnAction.AttackOneEnemy()));

            return skills;
        }

        public static Champion Create(double effectiveSpeed)
        {
            return new Champion("Septimus", 102, effectiveSpeed, GetSkills());
        }

        public static Champion Create(int uiSpeed, int speedSets, int perceptionSets)
        {
            return new Champion("Septimus", 102, uiSpeed, speedSets, perceptionSets, GetSkills());
        }

        public static List<Constants.SkillId> AISkills
        {
            get
            {
                return new List<Constants.SkillId>()
                {
                    Constants.SkillId.A2,
                    Constants.SkillId.A1,
                };
            }
        }
    }
}
