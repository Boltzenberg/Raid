﻿using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaidLib.DataModel.Champions
{
    public static class Skullcrusher
    {
        public static Champion Create(int uiSpeed, int speedSets)
        {
            List<BuffToApply> a2Buffs = new List<BuffToApply>()
            {
                new BuffToApply(Constants.Buff.Unkillable, 1, Constants.Target.Self),
                new BuffToApply(Constants.Buff.Counterattack, 2, Constants.Target.AllAllies)
            };

            List<Skill> skills = new List<Skill>();
            skills.Add(new Skill("Smash", Constants.SkillId.A1, 0, new TurnAction(3, Constants.Target.OneEnemy, null, null, null)));
            skills.Add(new Skill("Stonewall", Constants.SkillId.A2, 3, new TurnAction(0, Constants.Target.None, null, a2Buffs, null)));

            return new Champion("Skullcrusher", 98, uiSpeed, speedSets, skills);
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
