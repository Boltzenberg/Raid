using RaidLib.DataModel;
using RaidLib.DataModel.Champions;
using System;
using System.Collections.Generic;

namespace RaidLib.Simulator.Teams
{
    public static class DeadwoodJedi
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

            return new Tuple<Champion, List<Constants.SkillId>, List<Constants.SkillId>>(Maneater.Create(240, 3, 0), Maneater.AISkills, startupSkillOrder);
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

            return new Tuple<Champion, List<Constants.SkillId>, List<Constants.SkillId>>(Painkeeper.Create(221, 3, 0), Painkeeper.AISkills, startupSkillOrder);
        }

        static Tuple<Champion, List<Constants.SkillId>, List<Constants.SkillId>> CreateRhazinScarhide(ClanBoss.Level level)
        {
            return new Tuple<Champion, List<Constants.SkillId>, List<Constants.SkillId>>(RhazinScarhide.Create(177, 1, 0), RhazinScarhide.AISkills, new List<Constants.SkillId>());
        }

        static Tuple<Champion, List<Constants.SkillId>, List<Constants.SkillId>> CreateSeptimus(ClanBoss.Level level)
        {
            return new Tuple<Champion, List<Constants.SkillId>, List<Constants.SkillId>>(Septimus.Create(175, 2, 0), Septimus.AISkills, new List<Constants.SkillId>());
        }

        static Tuple<Champion, List<Constants.SkillId>, List<Constants.SkillId>> CreateAothar(ClanBoss.Level level)
        {
            int uiSpeed = 114;
            if (level == ClanBoss.Level.Nightmare)
            {
                uiSpeed = 121;
            }

            return new Tuple<Champion, List<Constants.SkillId>, List<Constants.SkillId>>(Aothar.Create(uiSpeed, 1, 0), Aothar.AISkills, new List<Constants.SkillId>());
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
