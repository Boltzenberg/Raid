using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaidLib.DataModel.Champions
{
    public static class Maneater
    {
        public static Champion Create(int uiSpeed, int speedSets)
        {
            List<Skill> skills = new List<Skill>();
            skills.Add(new Skill("Pummel", Constants.SkillId.A1, 0, TurnAction.AttackOneEnemy()));
            skills.Add(new Skill("Syphon", Constants.SkillId.A2, 3, TurnAction.AttackOneEnemy()));
            skills.Add(new Skill(
                "Ancient Blood",
                Constants.SkillId.A3,
                5,
                new TurnAction(
                    0,
                    Constants.Target.None,
                    null,
                    new List<BuffToApply>() {
                        new BuffToApply(Constants.Buff.BlockDebuffs, 2, Constants.Target.FullTeam),
                        new BuffToApply(Constants.Buff.Unkillable, 2, Constants.Target.FullTeam)
                    },
                null)));

            return new Champion("Maneater", 98, uiSpeed, speedSets, skills);
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
