using RaidLib.DataModel;
using RaidLib.DataModel.Champions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaidLib.Simulator.Teams
{
    public static class AllyAttackUnkillable
    {
        /*
        248 - Maneater
        232 - DPS
        223 - Pain Keeper
        172 - Kreela (or other Ally Attack/DPS)
        123 - Slow Boi
        */

        static Tuple<Champion, List<Constants.SkillId>, List<Constants.SkillId>> CreateManeater(ClanBoss.Level level)
        {
            // ME- a1, a1, a3, auto
            List<Constants.SkillId> startupSkillOrder = new List<Constants.SkillId>()
                {
                /*
                    Constants.SkillId.A1,
                    Constants.SkillId.A1,
                    Constants.SkillId.A1,
                    Constants.SkillId.A3,
                */
                };

            return new Tuple<Champion, List<Constants.SkillId>, List<Constants.SkillId>>(Maneater.Create(248, 0, 0), Maneater.AISkills, startupSkillOrder);
        }

        static Tuple<Champion, List<Constants.SkillId>, List<Constants.SkillId>> CreatePainkeeper(ClanBoss.Level level)
        {
            // PK - a1, a3, a1, a2
            List<Constants.SkillId> startupSkillOrder = new List<Constants.SkillId>()
                {
                /*
                    Constants.SkillId.A1,
                    Constants.SkillId.A3,
                    Constants.SkillId.A1,
                    Constants.SkillId.A2,
                */
                };

            return new Tuple<Champion, List<Constants.SkillId>, List<Constants.SkillId>>(Painkeeper.Create(223, 0, 0), Painkeeper.AISkills, startupSkillOrder);
        }

        static Tuple<Champion, List<Constants.SkillId>, List<Constants.SkillId>> CreateDPS1(ClanBoss.Level level)
        {
            return new Tuple<Champion, List<Constants.SkillId>, List<Constants.SkillId>>(Generic.Create("DPS1", 232), Generic.AISkills, new List<Constants.SkillId>());
        }

        static Tuple<Champion, List<Constants.SkillId>, List<Constants.SkillId>> CreateDPS2(ClanBoss.Level level)
        {
            return new Tuple<Champion, List<Constants.SkillId>, List<Constants.SkillId>>(Generic.Create("DPS2", 172), Generic.AISkills, new List<Constants.SkillId>());
        }

        static Tuple<Champion, List<Constants.SkillId>, List<Constants.SkillId>> CreateSlowBoi(ClanBoss.Level level)
        {
            return new Tuple<Champion, List<Constants.SkillId>, List<Constants.SkillId>>(Generic.Create("SlowBoi", 123), Generic.AISkills, new List<Constants.SkillId>());
        }

        public static List<Champion.CreateChampion> ChampionCreators()
        {
            return new List<Champion.CreateChampion>()
                {
                    CreateSlowBoi,
                    CreateDPS1,
                    CreateDPS2,
                    CreatePainkeeper,
                    CreateManeater,
                };
        }
    }
}
