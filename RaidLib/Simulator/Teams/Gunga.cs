using RaidLib.DataModel;
using RaidLib.DataModel.Champions;
using System;
using System.Collections.Generic;

namespace RaidLib.Simulator.Teams
{
    public static class Gunga
    {
        public static class AllyAttackUnkillable
        {
            static Tuple<Champion, List<Constants.SkillId>, List<Constants.SkillId>> CreateManeater(ClanBoss.Level level)
            {
                List<Constants.SkillId> startupSkillOrder = new List<Constants.SkillId>();
                switch (level)
                {
                    case ClanBoss.Level.UltraNightmare:
                        startupSkillOrder.Add(Constants.SkillId.A3);
                        break;
                    case ClanBoss.Level.Nightmare:
                        startupSkillOrder.Add(Constants.SkillId.A1);
                        startupSkillOrder.Add(Constants.SkillId.A3);
                        break;
                    case ClanBoss.Level.Brutal:
                        startupSkillOrder.Add(Constants.SkillId.A1);
                        startupSkillOrder.Add(Constants.SkillId.A1);
                        startupSkillOrder.Add(Constants.SkillId.A3);
                        break;
                    default:
                        throw new ArgumentException("Only works for Brutal, Nightmare, or UltraNightmare");
                }

                return new Tuple<Champion, List<Constants.SkillId>, List<Constants.SkillId>>(Maneater.Create(248, 2, 0), Maneater.AISkills, startupSkillOrder);
            }

            static Tuple<Champion, List<Constants.SkillId>, List<Constants.SkillId>> CreatePainkeeper(ClanBoss.Level level)
            {
                List<Constants.SkillId> startupSkillOrder = new List<Constants.SkillId>();
                switch (level)
                {
                    case ClanBoss.Level.UltraNightmare:
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
                    case ClanBoss.Level.Brutal:
                        startupSkillOrder.Add(Constants.SkillId.A1);
                        startupSkillOrder.Add(Constants.SkillId.A3);
                        startupSkillOrder.Add(Constants.SkillId.A1);
                        startupSkillOrder.Add(Constants.SkillId.A2);
                        break;
                    default:
                        throw new ArgumentException("Only works for Brutal, Nightmare, or UltraNightmare");
                }

                return new Tuple<Champion, List<Constants.SkillId>, List<Constants.SkillId>>(Painkeeper.Create(222, 2, 0), Painkeeper.AISkills, startupSkillOrder);
            }

            static Tuple<Champion, List<Constants.SkillId>, List<Constants.SkillId>> CreateCatacombCouncilor(ClanBoss.Level level)
            {
                int uiSpeed = 173;
                if (level == ClanBoss.Level.Nightmare)
                {
                    uiSpeed = 181;
                }

                List<Constants.SkillId> startupSkillOrder = new List<Constants.SkillId>();
                switch (level)
                {
                    case ClanBoss.Level.UltraNightmare:
                        startupSkillOrder.Add(Constants.SkillId.A1);
                        startupSkillOrder.Add(Constants.SkillId.A2);
                        startupSkillOrder.Add(Constants.SkillId.A3);
                        break;
                    case ClanBoss.Level.Nightmare:
                        startupSkillOrder.Add(Constants.SkillId.A1);
                        startupSkillOrder.Add(Constants.SkillId.A1);
                        startupSkillOrder.Add(Constants.SkillId.A2);
                        startupSkillOrder.Add(Constants.SkillId.A3);
                        break;
                    case ClanBoss.Level.Brutal:
                        startupSkillOrder.Add(Constants.SkillId.A1);
                        startupSkillOrder.Add(Constants.SkillId.A2);
                        startupSkillOrder.Add(Constants.SkillId.A3);
                        break;
                    default:
                        throw new ArgumentException("Only works for Brutal, Nightmare, or UltraNightmare");
                }

                return new Tuple<Champion, List<Constants.SkillId>, List<Constants.SkillId>>(CatacombCouncilor.Create(uiSpeed, 0, 0), CatacombCouncilor.AISkills, startupSkillOrder);
            }

            static Tuple<Champion, List<Constants.SkillId>, List<Constants.SkillId>> CreateSlowBoiFB(ClanBoss.Level level)
            {
                List<Constants.SkillId> startupSkillOrder = new List<Constants.SkillId>();
                startupSkillOrder.Add(Constants.SkillId.A1);
                return new Tuple<Champion, List<Constants.SkillId>, List<Constants.SkillId>>(FrozenBanshee.Create(123.0d), FrozenBanshee.AISkills, startupSkillOrder);
            }

            static Tuple<Champion, List<Constants.SkillId>, List<Constants.SkillId>> CreateDPSFB(ClanBoss.Level level)
            {
                return new Tuple<Champion, List<Constants.SkillId>, List<Constants.SkillId>>(FrozenBanshee.Create(223.0d), FrozenBanshee.AISkills, new List<Constants.SkillId>());
            }

            static Tuple<Champion, List<Constants.SkillId>, List<Constants.SkillId>> CreateSlowBoiRhazin(ClanBoss.Level level)
            {
                return new Tuple<Champion, List<Constants.SkillId>, List<Constants.SkillId>>(RhazinScarhide.Create(123.0d), RhazinScarhide.AISkills, new List<Constants.SkillId>());
            }

            static Tuple<Champion, List<Constants.SkillId>, List<Constants.SkillId>> CreateDPSRhazin(ClanBoss.Level level)
            {
                return new Tuple<Champion, List<Constants.SkillId>, List<Constants.SkillId>>(RhazinScarhide.Create(223.0d), RhazinScarhide.AISkills, new List<Constants.SkillId>());
            }

            public static List<Champion.CreateChampion> SlowRhazin()
            {
                return new List<Champion.CreateChampion>()
                {
                    CreateDPSFB,
                    CreateSlowBoiRhazin,
                    CreateCatacombCouncilor,
                    CreatePainkeeper,
                    CreateManeater,
                };
            }

            public static List<Champion.CreateChampion> SlowFB()
            {
                return new List<Champion.CreateChampion>()
                {
                    CreateSlowBoiFB,
                    CreateDPSRhazin,
                    CreateCatacombCouncilor,
                    CreatePainkeeper,
                    CreateManeater,
                };
            }
        }

        public static class MultiLevel4to3
        {
            static Tuple<Champion, List<Constants.SkillId>, List<Constants.SkillId>> CreateManeater(ClanBoss.Level level)
            {
                List<Constants.SkillId> startupSkillOrder = new List<Constants.SkillId>();
                switch (level)
                {
                    case ClanBoss.Level.UltraNightmare:
                        startupSkillOrder.Add(Constants.SkillId.A3);
                        break;
                    case ClanBoss.Level.Nightmare:
                        startupSkillOrder.Add(Constants.SkillId.A1);
                        startupSkillOrder.Add(Constants.SkillId.A3);
                        break;
                    case ClanBoss.Level.Brutal:
                        startupSkillOrder.Add(Constants.SkillId.A1);
                        startupSkillOrder.Add(Constants.SkillId.A1);
                        startupSkillOrder.Add(Constants.SkillId.A3);
                        break;
                    default:
                        throw new ArgumentException("Only works for Brutal, Nightmare, or UltraNightmare");
                }

                return new Tuple<Champion, List<Constants.SkillId>, List<Constants.SkillId>>(Maneater.Create(248, 2, 0), Maneater.AISkills, startupSkillOrder);
            }

            static Tuple<Champion, List<Constants.SkillId>, List<Constants.SkillId>> CreatePainkeeper(ClanBoss.Level level)
            {
                List<Constants.SkillId> startupSkillOrder = new List<Constants.SkillId>();
                switch (level)
                {
                    case ClanBoss.Level.UltraNightmare:
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
                    case ClanBoss.Level.Brutal:
                        startupSkillOrder.Add(Constants.SkillId.A1);
                        startupSkillOrder.Add(Constants.SkillId.A3);
                        startupSkillOrder.Add(Constants.SkillId.A1);
                        startupSkillOrder.Add(Constants.SkillId.A2);
                        break;
                    default:
                        throw new ArgumentException("Only works for Brutal, Nightmare, or UltraNightmare");
                }

                return new Tuple<Champion, List<Constants.SkillId>, List<Constants.SkillId>>(Painkeeper.Create(222, 2, 0), Painkeeper.AISkills, startupSkillOrder);
            }

            static Tuple<Champion, List<Constants.SkillId>, List<Constants.SkillId>> CreateFrozenBanshee(ClanBoss.Level level)
            {
                return new Tuple<Champion, List<Constants.SkillId>, List<Constants.SkillId>>(FrozenBanshee.Create(223, 3, 0), FrozenBanshee.AISkills, new List<Constants.SkillId>());
            }

            static Tuple<Champion, List<Constants.SkillId>, List<Constants.SkillId>> CreateRhazinScarhide(ClanBoss.Level level)
            {
                int effectiveSpeed = 172;
                if (level == ClanBoss.Level.Nightmare)
                {
                    effectiveSpeed = 182;
                }

                return new Tuple<Champion, List<Constants.SkillId>, List<Constants.SkillId>>(RhazinScarhide.Create(effectiveSpeed, 0, 1), RhazinScarhide.AISkills, new List<Constants.SkillId>());
            }

            static Tuple<Champion, List<Constants.SkillId>, List<Constants.SkillId>> CreateSlowboi(ClanBoss.Level level)
            {
                return new Tuple<Champion, List<Constants.SkillId>, List<Constants.SkillId>>(Bulwark.Create(123, 0, 0), Bulwark.AISkills, new List<Constants.SkillId>());
            }

            public static List<Champion.CreateChampion> ChampionCreators()
            {
                return new List<Champion.CreateChampion>()
                {
                    CreateFrozenBanshee,
                    CreateRhazinScarhide,
                    CreateSlowboi,
                    CreatePainkeeper,
                    CreateManeater,
                };
            }
        }


        public static class MultiLevel4to3Green
        {
            static Tuple<Champion, List<Constants.SkillId>, List<Constants.SkillId>> CreateManeater(ClanBoss.Level level)
            {
                List<Constants.SkillId> startupSkillOrder = new List<Constants.SkillId>();
                switch (level)
                {
                    case ClanBoss.Level.UltraNightmare:
                        startupSkillOrder.Add(Constants.SkillId.A3);
                        break;
                    case ClanBoss.Level.Nightmare:
                        startupSkillOrder.Add(Constants.SkillId.A1);
                        startupSkillOrder.Add(Constants.SkillId.A3);
                        break;
                    case ClanBoss.Level.Brutal:
                        startupSkillOrder.Add(Constants.SkillId.A1);
                        startupSkillOrder.Add(Constants.SkillId.A1);
                        startupSkillOrder.Add(Constants.SkillId.A3);
                        break;
                    default:
                        throw new ArgumentException("Only works for Brutal, Nightmare, or UltraNightmare");
                }

                return new Tuple<Champion, List<Constants.SkillId>, List<Constants.SkillId>>(Maneater.Create(248, 2, 0), Maneater.AISkills, startupSkillOrder);
            }

            static Tuple<Champion, List<Constants.SkillId>, List<Constants.SkillId>> CreatePainkeeper(ClanBoss.Level level)
            {
                List<Constants.SkillId> startupSkillOrder = new List<Constants.SkillId>();
                switch (level)
                {
                    case ClanBoss.Level.UltraNightmare:
                        startupSkillOrder.Add(Constants.SkillId.A1);
                        startupSkillOrder.Add(Constants.SkillId.A1);
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
                    case ClanBoss.Level.Brutal:
                        startupSkillOrder.Add(Constants.SkillId.A1);
                        startupSkillOrder.Add(Constants.SkillId.A3);
                        startupSkillOrder.Add(Constants.SkillId.A1);
                        startupSkillOrder.Add(Constants.SkillId.A2);
                        break;
                    default:
                        throw new ArgumentException("Only works for Brutal, Nightmare, or UltraNightmare");
                }

                return new Tuple<Champion, List<Constants.SkillId>, List<Constants.SkillId>>(Painkeeper.Create(222, 2, 0), Painkeeper.AISkills, startupSkillOrder);
            }

            static Tuple<Champion, List<Constants.SkillId>, List<Constants.SkillId>> CreateFrozenBanshee(ClanBoss.Level level)
            {
                return new Tuple<Champion, List<Constants.SkillId>, List<Constants.SkillId>>(FrozenBanshee.Create(184, 3, 0), FrozenBanshee.AISkills, new List<Constants.SkillId>());
            }

            static Tuple<Champion, List<Constants.SkillId>, List<Constants.SkillId>> CreateRhazinScarhide(ClanBoss.Level level)
            {
                int effectiveSpeed = 172;
                if (level == ClanBoss.Level.Nightmare)
                {
                    effectiveSpeed = 182;
                }

                return new Tuple<Champion, List<Constants.SkillId>, List<Constants.SkillId>>(RhazinScarhide.Create(effectiveSpeed, 0, 1), RhazinScarhide.AISkills, new List<Constants.SkillId>());
            }

            static Tuple<Champion, List<Constants.SkillId>, List<Constants.SkillId>> CreateSlowboi(ClanBoss.Level level)
            {
                return new Tuple<Champion, List<Constants.SkillId>, List<Constants.SkillId>>(Bulwark.Create(123, 0, 0), Bulwark.AISkills, new List<Constants.SkillId>());
            }

            public static List<Champion.CreateChampion> ChampionCreators()
            {
                return new List<Champion.CreateChampion>()
                {
                    CreateFrozenBanshee,
                    CreateRhazinScarhide,
                    CreateSlowboi,
                    CreatePainkeeper,
                    CreateManeater,
                };
            }
        }

        #region Retired
        public static class Nightmare
        {
            static Tuple<Champion, List<Constants.SkillId>, List<Constants.SkillId>> CreateManeater(ClanBoss.Level level)
            {
                return new Tuple<Champion, List<Constants.SkillId>, List<Constants.SkillId>>(Maneater.Create(227, 2, 0), Maneater.AISkills, new List<Constants.SkillId>());
            }

            static Tuple<Champion, List<Constants.SkillId>, List<Constants.SkillId>> CreatePainkeeper(ClanBoss.Level level)
            {
                List<Constants.SkillId> startupSkillOrder = new List<Constants.SkillId>()
                {
                    Constants.SkillId.A3,
                    Constants.SkillId.A1,
                };

                return new Tuple<Champion, List<Constants.SkillId>, List<Constants.SkillId>>(Painkeeper.Create(213, 2, 0), Painkeeper.AISkills, startupSkillOrder);
            }

            static Tuple<Champion, List<Constants.SkillId>, List<Constants.SkillId>> CreateFrozenBanshee(ClanBoss.Level level)
            {
                return new Tuple<Champion, List<Constants.SkillId>, List<Constants.SkillId>>(FrozenBanshee.Create(169, 0, 0), FrozenBanshee.AISkills, new List<Constants.SkillId>());
            }

            static Tuple<Champion, List<Constants.SkillId>, List<Constants.SkillId>> CreateRhazinScarhide(ClanBoss.Level level)
            {
                return new Tuple<Champion, List<Constants.SkillId>, List<Constants.SkillId>>(RhazinScarhide.Create(170, 0, 1), RhazinScarhide.AISkills, new List<Constants.SkillId>());
            }

            static Tuple<Champion, List<Constants.SkillId>, List<Constants.SkillId>> CreateGravechillKiller(ClanBoss.Level level)
            {
                return new Tuple<Champion, List<Constants.SkillId>, List<Constants.SkillId>>(GravechillKiller.Create(168, 0, 0), GravechillKiller.AISkills, new List<Constants.SkillId>());
            }

            static Tuple<Champion, List<Constants.SkillId>, List<Constants.SkillId>> CreateBulwark(ClanBoss.Level level)
            {
                return new Tuple<Champion, List<Constants.SkillId>, List<Constants.SkillId>>(Bulwark.Create(106, 0, 0), Bulwark.AISkills, new List<Constants.SkillId>());
            }

            public static List<Champion.CreateChampion> ChampionCreators()
            {
                return new List<Champion.CreateChampion>()
                {
                    CreateRhazinScarhide,
                    CreateFrozenBanshee,
                    CreateBulwark,
                    CreateManeater,
                    CreatePainkeeper
                };
            }
        }

        public static class MultiLevel
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

                return new Tuple<Champion, List<Constants.SkillId>, List<Constants.SkillId>>(Painkeeper.Create(218, 2, 0), Painkeeper.AISkills, startupSkillOrder);
            }

            static Tuple<Champion, List<Constants.SkillId>, List<Constants.SkillId>> CreateFrozenBanshee(ClanBoss.Level level)
            {
                return new Tuple<Champion, List<Constants.SkillId>, List<Constants.SkillId>>(FrozenBanshee.Create(178, 0, 0), FrozenBanshee.AISkills, new List<Constants.SkillId>());
            }

            static Tuple<Champion, List<Constants.SkillId>, List<Constants.SkillId>> CreateRhazinScarhide(ClanBoss.Level level)
            {
                return new Tuple<Champion, List<Constants.SkillId>, List<Constants.SkillId>>(RhazinScarhide.Create(175, 0, 0), RhazinScarhide.AISkills, new List<Constants.SkillId>());
            }

            static Tuple<Champion, List<Constants.SkillId>, List<Constants.SkillId>> CreateSlowboi(ClanBoss.Level level)
            {
                if (level == ClanBoss.Level.Nightmare)
                {
                    return new Tuple<Champion, List<Constants.SkillId>, List<Constants.SkillId>>(Bulwark.Create(121, 0, 0), Bulwark.AISkills, new List<Constants.SkillId>());
                }
                else
                {
                    return new Tuple<Champion, List<Constants.SkillId>, List<Constants.SkillId>>(Bulwark.Create(112, 0, 0), Bulwark.AISkills, new List<Constants.SkillId>());
                }
            }

            public static List<Champion.CreateChampion> ChampionCreators()
            {
                return new List<Champion.CreateChampion>()
                {
                    CreateRhazinScarhide,
                    CreateFrozenBanshee,
                    CreateSlowboi,
                    CreateManeater,
                    CreatePainkeeper
                };
            }
        }
        #endregion
    }
}
