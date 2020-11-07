using RaidLib.DataModel;
using RaidLib.DataModel.Champions;
using System;
using System.Collections.Generic;

namespace RaidLib.Simulator.Teams
{
    public static class MultiLevelUnkillable
    {
        static Tuple<Champion, List<Constants.SkillId>, List<Constants.SkillId>> CreateManeater(ClanBoss.Level level)
        {
            List<Constants.SkillId> startupSkillOrder = new List<Constants.SkillId>();

            switch (level)
            {
                case ClanBoss.Level.Brutal:
                    startupSkillOrder.Add(Constants.SkillId.A1);
                    startupSkillOrder.Add(Constants.SkillId.A1);
                    startupSkillOrder.Add(Constants.SkillId.A3);
                    startupSkillOrder.Add(Constants.SkillId.A1);
                    startupSkillOrder.Add(Constants.SkillId.A1);
                    startupSkillOrder.Add(Constants.SkillId.A1);
                    startupSkillOrder.Add(Constants.SkillId.A3);
                    startupSkillOrder.Add(Constants.SkillId.A2);
                    startupSkillOrder.Add(Constants.SkillId.A1);
                    startupSkillOrder.Add(Constants.SkillId.A1);
                    startupSkillOrder.Add(Constants.SkillId.A3);
                    startupSkillOrder.Add(Constants.SkillId.A1);
                    startupSkillOrder.Add(Constants.SkillId.A2);
                    startupSkillOrder.Add(Constants.SkillId.A1);
                    startupSkillOrder.Add(Constants.SkillId.A3);
                    break;
                case ClanBoss.Level.Nightmare:
                    startupSkillOrder.Add(Constants.SkillId.A1);
                    startupSkillOrder.Add(Constants.SkillId.A3);
                    startupSkillOrder.Add(Constants.SkillId.A1);
                    startupSkillOrder.Add(Constants.SkillId.A1);
                    break;
                case ClanBoss.Level.UltraNightmare:
                    // Nothing special...just works!
                    break;
                default:
                    throw new ArgumentException("Only works for Brutal, Nightmare, or UltraNightmare");
            }

            // 240.28d
            return new Tuple<Champion, List<Constants.SkillId>, List<Constants.SkillId>>(Maneater.Create(241, 2, 0), Maneater.AISkills, startupSkillOrder);
        }

        static Tuple<Champion, List<Constants.SkillId>, List<Constants.SkillId>> CreatePainkeeper(ClanBoss.Level level)
        {
            List<Constants.SkillId> startupSkillOrder = new List<Constants.SkillId>();

            switch (level)
            {
                case ClanBoss.Level.Brutal:
                    startupSkillOrder.Add(Constants.SkillId.A1);
                    startupSkillOrder.Add(Constants.SkillId.A3);
                    startupSkillOrder.Add(Constants.SkillId.A1);
                    startupSkillOrder.Add(Constants.SkillId.A2);
                    startupSkillOrder.Add(Constants.SkillId.A1);
                    startupSkillOrder.Add(Constants.SkillId.A3);
                    startupSkillOrder.Add(Constants.SkillId.A1);
                    startupSkillOrder.Add(Constants.SkillId.A2);
                    startupSkillOrder.Add(Constants.SkillId.A1);
                    startupSkillOrder.Add(Constants.SkillId.A3);
                    startupSkillOrder.Add(Constants.SkillId.A1);
                    startupSkillOrder.Add(Constants.SkillId.A2);
                    startupSkillOrder.Add(Constants.SkillId.A1);
                    startupSkillOrder.Add(Constants.SkillId.A3);
                    break;
                case ClanBoss.Level.Nightmare:
                    startupSkillOrder.Add(Constants.SkillId.A3);
                    startupSkillOrder.Add(Constants.SkillId.A1);
                    startupSkillOrder.Add(Constants.SkillId.A1);
                    startupSkillOrder.Add(Constants.SkillId.A2);
                    break;
                case ClanBoss.Level.UltraNightmare:
                    startupSkillOrder.Add(Constants.SkillId.A3);
                    startupSkillOrder.Add(Constants.SkillId.A1);
                    break;
                default:
                    throw new ArgumentException("Only works for Brutal, Nightmare, or UltraNightmare");
            }

            // 220.72
            return new Tuple<Champion, List<Constants.SkillId>, List<Constants.SkillId>>(Painkeeper.Create(221, 2, 0), Painkeeper.AISkills, startupSkillOrder);
        }

        static Tuple<Champion, List<Constants.SkillId>, List<Constants.SkillId>> CreateRhazinScarhide(ClanBoss.Level level)
        {
            return new Tuple<Champion, List<Constants.SkillId>, List<Constants.SkillId>>(RhazinScarhide.Create(176.92d), RhazinScarhide.AISkills, new List<Constants.SkillId>());
        }

        static Tuple<Champion, List<Constants.SkillId>, List<Constants.SkillId>> CreateSeptimus(ClanBoss.Level level)
        {
            return new Tuple<Champion, List<Constants.SkillId>, List<Constants.SkillId>>(Septimus.Create(175.48d), Septimus.AISkills, new List<Constants.SkillId>());
        }

        static Tuple<Champion, List<Constants.SkillId>, List<Constants.SkillId>> CreateAothar(ClanBoss.Level level)
        {
            double effectiveSpeed = 114.04d;
            if (level == ClanBoss.Level.Nightmare)
            {
                effectiveSpeed = 121.04d;
            }

            return new Tuple<Champion, List<Constants.SkillId>, List<Constants.SkillId>>(Aothar.Create(effectiveSpeed), Aothar.AISkills, new List<Constants.SkillId>());
        }

        public static List<Champion.CreateChampion> ChampionCreators()
        {
            return new List<Champion.CreateChampion>()
                {
                    CreateManeater,
                    CreatePainkeeper,
                    CreateRhazinScarhide,
                    CreateSeptimus,
                    CreateAothar
                };
        }
    }
}
