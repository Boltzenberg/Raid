using RaidLib.DataModel;
using RaidLib.DataModel.Champions;
using System;
using System.Collections.Generic;

namespace RaidLib.Simulator.Teams
{
    public static class Mudd
    {
        public static class DoubleManeater
        {
            static Tuple<Champion, List<Constants.SkillId>, List<Constants.SkillId>> CreateManeater1(ClanBoss.Level level)
            {
                return new Tuple<Champion, List<Constants.SkillId>, List<Constants.SkillId>>(Maneater.Create("Maneater 1", 227, 2, 0), Maneater.AISkills, new List<Constants.SkillId>());
            }

            static Tuple<Champion, List<Constants.SkillId>, List<Constants.SkillId>> CreateManeater2(ClanBoss.Level level)
            {
                return new Tuple<Champion, List<Constants.SkillId>, List<Constants.SkillId>>(Maneater.Create("Maneater 2", 227, 2, 0), Maneater.AISkills, new List<Constants.SkillId>());
            }

            static Tuple<Champion, List<Constants.SkillId>, List<Constants.SkillId>> CreateDracomorph(ClanBoss.Level level)
            {
                return new Tuple<Champion, List<Constants.SkillId>, List<Constants.SkillId>>(Dracomorph.Create(213, 2, 0), Dracomorph.AISkills, new List<Constants.SkillId>());
            }

            static Tuple<Champion, List<Constants.SkillId>, List<Constants.SkillId>> CreateTurvold(ClanBoss.Level level)
            {
                return new Tuple<Champion, List<Constants.SkillId>, List<Constants.SkillId>>(FrozenBanshee.Create(169, 0, 0), FrozenBanshee.AISkills, new List<Constants.SkillId>());
            }

            static Tuple<Champion, List<Constants.SkillId>, List<Constants.SkillId>> CreateDPS(ClanBoss.Level level)
            {
                return new Tuple<Champion, List<Constants.SkillId>, List<Constants.SkillId>>(RhazinScarhide.Create(170, 0, 1), RhazinScarhide.AISkills, new List<Constants.SkillId>());
            }

            public static List<Champion.CreateChampion> ChampionCreators()
            {
                return new List<Champion.CreateChampion>()
                {
                    CreateManeater1,
                    CreateManeater2,
                    CreateDracomorph,
                    CreateTurvold,
                    CreateDPS
                };
            }
        }
    }
}
