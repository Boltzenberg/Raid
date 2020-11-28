using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaidLib.DataModel.Champions
{
    public static class FrozenBanshee
    {
        private static List<Skill> GetSkills()
        {
            List<Skill> skills = new List<Skill>();
            skills.Add(new Skill("Death's Caress", Constants.SkillId.A1, 0, TurnAction.AttackOneEnemy()));
            skills.Add(new Skill("Cruel Exultation", Constants.SkillId.A2, 3, TurnAction.AttackOneEnemy()));
            skills.Add(new Skill("Frost Blight", Constants.SkillId.A3, 3, new TurnAction(1, Constants.Target.OneEnemy, null, null, new List<DebuffToApply>() { new DebuffToApply(Constants.Debuff.PoisonSensitivity, 2, Constants.Target.OneEnemy) })));

            return skills;
        }

        public static Champion Create(double effectiveSpeed)
        {
            return new Champion("Frozen Banshee", 99, effectiveSpeed, GetSkills());
        }

        public static Champion Create(int uiSpeed, int speedSets, int perceptionSets)
        {
            return new Champion("Frozen Banshee", 99, uiSpeed, speedSets, perceptionSets, GetSkills());
        }

        public static List<Constants.SkillId> AISkills
        {
            get
            {
                return new List<Constants.SkillId>()
                {
                    Constants.SkillId.A3,
                    Constants.SkillId.A1,
                };
            }
        }
    }
}
