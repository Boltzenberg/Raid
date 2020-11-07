using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaidLib.DataModel.Champions
{
    public static class Bulwark
    {
        private static List<Skill> GetSkills()
        {
            List<Skill> skills = new List<Skill>();
            skills.Add(new Skill("Hefty Flail", Constants.SkillId.A1, 0, TurnAction.AttackOneEnemy()));
            skills.Add(new Skill("Meteoric Ignition", Constants.SkillId.A2, 3, TurnAction.AttackOneEnemy()));
            skills.Add(new Skill("Punishing Defense", Constants.SkillId.P1, 0, TurnAction.AttackOneEnemy()));

            return skills;
        }

        public static Champion Create(double effectiveSpeed)
        {
            return new Champion("Bulwark", effectiveSpeed, GetSkills());
        }

        public static Champion Create(int uiSpeed, int speedSets, int perceptionSets)
        {
            return new Champion("Bulwark", 97, uiSpeed, speedSets, perceptionSets, GetSkills());
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
