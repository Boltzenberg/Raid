using RaidLib.DataModel;
using System;
using System.Collections.Generic;

namespace ClanBossTurns
{
    public static class Teams
    {
        public delegate Tuple<Champion, List<Constants.SkillId>, List<Constants.SkillId>> CreateChampion(ClanBoss.Level level);

        public static class Gunga
        {
            static Tuple<Champion, List<Constants.SkillId>, List<Constants.SkillId>> CreateManeater(ClanBoss.Level level)
            {
                List<Constants.SkillId> skillsToUse = new List<Constants.SkillId>()
                {
                    Constants.SkillId.A3,
                    Constants.SkillId.A2,
                    Constants.SkillId.A1,
                };

                List<Constants.SkillId> startupSkillOrder = new List<Constants.SkillId>();

                return new Tuple<Champion, List<Constants.SkillId>, List<Constants.SkillId>>(Champion.CreateManeater(227, 2), skillsToUse, startupSkillOrder);
            }

            static Tuple<Champion, List<Constants.SkillId>, List<Constants.SkillId>> CreatePainkeeper(ClanBoss.Level level)
            {
                List<Constants.SkillId> skillsToUse = new List<Constants.SkillId>()
                {
                    Constants.SkillId.A3,
                    Constants.SkillId.A2,
                    Constants.SkillId.A1,
                };

                List<Constants.SkillId> startupSkillOrder = new List<Constants.SkillId>()
                {
                    Constants.SkillId.A3,
                    Constants.SkillId.A1,
                };

                return new Tuple<Champion, List<Constants.SkillId>, List<Constants.SkillId>>(Champion.CreatePainkeeper(213, 2), skillsToUse, startupSkillOrder);
            }

            static Tuple<Champion, List<Constants.SkillId>, List<Constants.SkillId>> CreateFrozenBanshee(ClanBoss.Level level)
            {
                List<Constants.SkillId> skillsToUse = new List<Constants.SkillId>()
                {
                    Constants.SkillId.A3,
                    Constants.SkillId.A1,
                };

                List<Constants.SkillId> startupSkillOrder = new List<Constants.SkillId>();

                return new Tuple<Champion, List<Constants.SkillId>, List<Constants.SkillId>>(Champion.CreateFrozenBanshee(169, 0), skillsToUse, startupSkillOrder);
            }

            static Tuple<Champion, List<Constants.SkillId>, List<Constants.SkillId>> CreateGravechillKiller(ClanBoss.Level level)
            {
                List<Constants.SkillId> skillsToUse = new List<Constants.SkillId>()
                {
                    Constants.SkillId.A3,
                    Constants.SkillId.A2,
                    Constants.SkillId.A1,
                };

                List<Constants.SkillId> startupSkillOrder = new List<Constants.SkillId>();

                return new Tuple<Champion, List<Constants.SkillId>, List<Constants.SkillId>>(Champion.CreateGravechillKiller(168, 0), skillsToUse, startupSkillOrder);
            }

            static Tuple<Champion, List<Constants.SkillId>, List<Constants.SkillId>> CreateBulwark(ClanBoss.Level level)
            {
                List<Constants.SkillId> skillsToUse = new List<Constants.SkillId>()
                {
                    Constants.SkillId.A2,
                    Constants.SkillId.A1,
                };

                List<Constants.SkillId> startupSkillOrder = new List<Constants.SkillId>();

                return new Tuple<Champion, List<Constants.SkillId>, List<Constants.SkillId>>(Champion.CreateBulwark(106, 0), skillsToUse, startupSkillOrder);
            }

            public static List<CreateChampion> ChampionCreators()
            {
                return new List<CreateChampion>()
                {
                    CreateManeater,
                    CreatePainkeeper,
                    CreateFrozenBanshee,
                    CreateGravechillKiller,
                    CreateBulwark
                };
            }
        }

        public static class Ash
        {
            static Tuple<Champion, List<Constants.SkillId>, List<Constants.SkillId>> CreateManeater(ClanBoss.Level level)
            {
                List<Constants.SkillId> skillsToUse = new List<Constants.SkillId>()
                {
                    Constants.SkillId.A3,
                    Constants.SkillId.A2,
                    Constants.SkillId.A1,
                };

                List<Constants.SkillId> startupSkillOrder = new List<Constants.SkillId>();

                return new Tuple<Champion, List<Constants.SkillId>, List<Constants.SkillId>>(Champion.CreateManeater(98 + 143, 3), skillsToUse, startupSkillOrder);
            }

            static Tuple<Champion, List<Constants.SkillId>, List<Constants.SkillId>> CreatePainkeeper(ClanBoss.Level level)
            {
                List<Constants.SkillId> skillsToUse = new List<Constants.SkillId>()
                {
                    Constants.SkillId.A3,
                    Constants.SkillId.A2,
                    Constants.SkillId.A1,
                };

                List<Constants.SkillId> startupSkillOrder = new List<Constants.SkillId>()
                {
                    Constants.SkillId.A3,
                    Constants.SkillId.A1,
                };

                return new Tuple<Champion, List<Constants.SkillId>, List<Constants.SkillId>>(Champion.CreatePainkeeper(102 + 119, 1), skillsToUse, startupSkillOrder);
            }

            static Tuple<Champion, List<Constants.SkillId>, List<Constants.SkillId>> CreateFrozenBanshee(ClanBoss.Level level)
            {
                List<Constants.SkillId> skillsToUse = new List<Constants.SkillId>()
                {
                    Constants.SkillId.A3,
                    Constants.SkillId.A1,
                };

                List<Constants.SkillId> startupSkillOrder = new List<Constants.SkillId>();

                return new Tuple<Champion, List<Constants.SkillId>, List<Constants.SkillId>>(Champion.CreateFrozenBanshee(99 + 78, 0), skillsToUse, startupSkillOrder);
            }

            static Tuple<Champion, List<Constants.SkillId>, List<Constants.SkillId>> CreateOccultBrawler(ClanBoss.Level level)
            {
                List<Constants.SkillId> skillsToUse = new List<Constants.SkillId>()
                {
                    Constants.SkillId.A2,
                    Constants.SkillId.A1,
                };

                List<Constants.SkillId> startupSkillOrder = new List<Constants.SkillId>();

                return new Tuple<Champion, List<Constants.SkillId>, List<Constants.SkillId>>(Champion.CreateOccultBrawler(98 + 77, 0), skillsToUse, startupSkillOrder);
            }

            static Tuple<Champion, List<Constants.SkillId>, List<Constants.SkillId>> CreateAothar(ClanBoss.Level level)
            {
                List<Constants.SkillId> skillsToUse = new List<Constants.SkillId>()
                {
                    Constants.SkillId.A3,
                    Constants.SkillId.A2,
                    Constants.SkillId.A1,
                };

                List<Constants.SkillId> startupSkillOrder = new List<Constants.SkillId>();

                return new Tuple<Champion, List<Constants.SkillId>, List<Constants.SkillId>>(Champion.CreateAothar(92 + 24, 0), skillsToUse, startupSkillOrder);
            }

            public static List<CreateChampion> ChampionCreators()
            {
                return new List<CreateChampion>()
                {
                    CreateManeater,
                    CreatePainkeeper,
                    CreateFrozenBanshee,
                    CreateOccultBrawler,
                    CreateAothar
                };
            }
        }

        public static class DeadwoodJedi
        {
            static Tuple<Champion, List<Constants.SkillId>, List<Constants.SkillId>> CreateManeater(ClanBoss.Level level)
            {
                List<Constants.SkillId> skillsToUse = new List<Constants.SkillId>()
                {
                    Constants.SkillId.A3,
                    Constants.SkillId.A2,
                    Constants.SkillId.A1,
                };

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

                return new Tuple<Champion, List<Constants.SkillId>, List<Constants.SkillId>>(Champion.CreateManeater(240, 3), skillsToUse, startupSkillOrder);
            }

            static Tuple<Champion, List<Constants.SkillId>, List<Constants.SkillId>> CreatePainkeeper(ClanBoss.Level level)
            {
                List<Constants.SkillId> skillsToUse = new List<Constants.SkillId>()
                {
                    Constants.SkillId.A3,
                    Constants.SkillId.A2,
                    Constants.SkillId.A1,
                };

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

                return new Tuple<Champion, List<Constants.SkillId>, List<Constants.SkillId>>(Champion.CreatePainkeeper(221, 3), skillsToUse, startupSkillOrder);
            }

            static Tuple<Champion, List<Constants.SkillId>, List<Constants.SkillId>> CreateAothar(ClanBoss.Level level)
            {
                List<Constants.SkillId> skillsToUse = new List<Constants.SkillId>()
                {
                    Constants.SkillId.A3,
                    Constants.SkillId.A2,
                    Constants.SkillId.A1,
                };

                List<Constants.SkillId> startupSkillOrder = new List<Constants.SkillId>();

                int uiSpeed = 114;
                if (level == ClanBoss.Level.Nightmare)
                {
                    uiSpeed = 121;
                }

                return new Tuple<Champion, List<Constants.SkillId>, List<Constants.SkillId>>(Champion.CreateAothar(uiSpeed, 1), skillsToUse, startupSkillOrder);
            }

            static Tuple<Champion, List<Constants.SkillId>, List<Constants.SkillId>> CreateRhazinScarhide(ClanBoss.Level level)
            {
                List<Constants.SkillId> skillsToUse = new List<Constants.SkillId>()
                {
                    Constants.SkillId.A3,
                    Constants.SkillId.A2,
                    Constants.SkillId.A1,
                };

                List<Constants.SkillId> startupSkillOrder = new List<Constants.SkillId>();

                return new Tuple<Champion, List<Constants.SkillId>, List<Constants.SkillId>>(Champion.CreateRhazinScarhide(177, 1), skillsToUse, startupSkillOrder);
            }

            static Tuple<Champion, List<Constants.SkillId>, List<Constants.SkillId>> CreateSeptimus(ClanBoss.Level level)
            {
                List<Constants.SkillId> skillsToUse = new List<Constants.SkillId>()
                {
                    Constants.SkillId.A2,
                    Constants.SkillId.A1,
                };

                List<Constants.SkillId> startupSkillOrder = new List<Constants.SkillId>();

                return new Tuple<Champion, List<Constants.SkillId>, List<Constants.SkillId>>(Champion.CreateSeptimus(175, 2), skillsToUse, startupSkillOrder);
            }

            public static List<CreateChampion> ChampionCreators()
            {
                return new List<CreateChampion>()
                {
                    CreateManeater,
                    CreatePainkeeper,
                    CreateRhazinScarhide,
                    CreateSeptimus,
                    CreateAothar
                };
            }
        }

        public static class BTrain
        {

        }
    }
}
