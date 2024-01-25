using System;
using System.Collections.Generic;

namespace RaidBattleSimulator.DataModel.Champions
{
    public static class Maneater
    {
        private static List<Skill> GetSkills(int[] initialSkillDelays)
        {
            List<Skill> skills = new List<Skill>();
            skills.Add(new Skill("Pummel", Constants.SkillId.A1, 0, TurnAction.AttackOneEnemy(), initialSkillDelays[0]));
            skills.Add(new Skill("Syphon", Constants.SkillId.A2, 3, TurnAction.AttackOneEnemy(), initialSkillDelays[1]));
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
                null),
                initialSkillDelays[2]));

            return skills;
        }

        public static ChampionBase Create(int uiSpeed, int speedSets, int perceptionSets, double speedAuraPercentage, int[] initialSkillDelays)
        {
            return Create("Maneater", uiSpeed, speedSets, perceptionSets, speedAuraPercentage, initialSkillDelays);
        }

        public static ChampionBase Create(string name, int uiSpeed, int speedSets, int perceptionSets, double speedAuraPercentage, int[] initialSkillDelays)
        {
            return new ChampionBase(name, 98, uiSpeed, speedSets, perceptionSets, speedAuraPercentage, GetSkills(initialSkillDelays));
        }
    }
}
