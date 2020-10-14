using RaidLib.DataModel;
using System;
using System.Collections.Generic;

namespace ClanBossTurns
{
    public static class Teams
    {
        public delegate Tuple<Champion, List<SkillPolicy>> CreateChampion(ClanBoss.Level level);

        public static class Gunga
        {
            static Tuple<Champion, List<SkillPolicy>> CreateManeater(ClanBoss.Level level)
            {
                List<SkillPolicy> policies = new List<SkillPolicy>()
                {
                    new SkillPolicy(Constants.SkillId.A3, 0),
                    new SkillPolicy(Constants.SkillId.A2, 0),
                    new SkillPolicy(Constants.SkillId.A1, 0),
                };

                return new Tuple<Champion, List<SkillPolicy>>(Champion.CreateManeater(227, 2), policies);
            }

            static Tuple<Champion, List<SkillPolicy>> CreatePainkeeper(ClanBoss.Level level)
            {
                List<SkillPolicy> policies = new List<SkillPolicy>()
                {
                    new SkillPolicy(Constants.SkillId.A3, 0),
                    new SkillPolicy(Constants.SkillId.A2, 3),
                    new SkillPolicy(Constants.SkillId.A1, 0),
                };

                return new Tuple<Champion, List<SkillPolicy>>(Champion.CreatePainkeeper(213, 2), policies);
            }

            static Tuple<Champion, List<SkillPolicy>> CreateFrozenBanshee(ClanBoss.Level level)
            {
                List<SkillPolicy> policies = new List<SkillPolicy>()
                {
                    new SkillPolicy(Constants.SkillId.A3, 0),
                    new SkillPolicy(Constants.SkillId.A1, 0),
                };

                return new Tuple<Champion, List<SkillPolicy>>(Champion.CreateFrozenBanshee(169, 0), policies);
            }

            static Tuple<Champion, List<SkillPolicy>> CreateGravechillKiller(ClanBoss.Level level)
            {
                List<SkillPolicy> policies = new List<SkillPolicy>()
                {
                    new SkillPolicy(Constants.SkillId.A3, 0),
                    new SkillPolicy(Constants.SkillId.A2, 0),
                    new SkillPolicy(Constants.SkillId.A1, 0),
                };

                return new Tuple<Champion, List<SkillPolicy>>(Champion.CreateGravechillKiller(168, 0), policies);
            }

            static Tuple<Champion, List<SkillPolicy>> CreateBulwark(ClanBoss.Level level)
            {
                List<SkillPolicy> policies = new List<SkillPolicy>()
                {
                    new SkillPolicy(Constants.SkillId.A2, 0),
                    new SkillPolicy(Constants.SkillId.A1, 0),
                };

                return new Tuple<Champion, List<SkillPolicy>>(Champion.CreateBulwark(106, 0), policies);
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
            static Tuple<Champion, List<SkillPolicy>> CreateManeater(ClanBoss.Level level)
            {
                List<SkillPolicy> policies = new List<SkillPolicy>()
                {
                    new SkillPolicy(Constants.SkillId.A3, 0),
                    new SkillPolicy(Constants.SkillId.A2, 0),
                    new SkillPolicy(Constants.SkillId.A1, 0),
                };

                return new Tuple<Champion, List<SkillPolicy>>(Champion.CreateManeater(98 + 143, 3), policies);
            }

            static Tuple<Champion, List<SkillPolicy>> CreatePainkeeper(ClanBoss.Level level)
            {
                List<SkillPolicy> policies = new List<SkillPolicy>()
                {
                    new SkillPolicy(Constants.SkillId.A3, 0),
                    new SkillPolicy(Constants.SkillId.A2, 3),
                    new SkillPolicy(Constants.SkillId.A1, 0),
                };

                return new Tuple<Champion, List<SkillPolicy>>(Champion.CreatePainkeeper(102 + 119, 1), policies);
            }

            static Tuple<Champion, List<SkillPolicy>> CreateFrozenBanshee(ClanBoss.Level level)
            {
                List<SkillPolicy> policies = new List<SkillPolicy>()
                {
                    new SkillPolicy(Constants.SkillId.A3, 0),
                    new SkillPolicy(Constants.SkillId.A1, 0),
                };

                return new Tuple<Champion, List<SkillPolicy>>(Champion.CreateFrozenBanshee(99 + 78, 0), policies);
            }

            static Tuple<Champion, List<SkillPolicy>> CreateOccultBrawler(ClanBoss.Level level)
            {
                List<SkillPolicy> policies = new List<SkillPolicy>()
                {
                    new SkillPolicy(Constants.SkillId.A2, 0),
                    new SkillPolicy(Constants.SkillId.A1, 0),
                };

                return new Tuple<Champion, List<SkillPolicy>>(Champion.CreateOccultBrawler(98 + 77, 0), policies);
            }

            static Tuple<Champion, List<SkillPolicy>> CreateAothar(ClanBoss.Level level)
            {
                List<SkillPolicy> policies = new List<SkillPolicy>()
                {
                    new SkillPolicy(Constants.SkillId.A3, 0),
                    new SkillPolicy(Constants.SkillId.A2, 0),
                    new SkillPolicy(Constants.SkillId.A1, 0),
                };

                return new Tuple<Champion, List<SkillPolicy>>(Champion.CreateAothar(92 + 24, 0), policies);
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
            static Tuple<Champion, List<SkillPolicy>> CreateManeater(ClanBoss.Level level)
            {
                List<SkillPolicy> policies = new List<SkillPolicy>();

                switch (level)
                {
                    case ClanBoss.Level.Brutal:
                        break;
                    case ClanBoss.Level.Nightmare:
                        break;
                    case ClanBoss.Level.UltraNightmare:
                        policies.Add(new SkillPolicy(Constants.SkillId.A3, 0));
                        policies.Add(new SkillPolicy(Constants.SkillId.A2, 0));
                        policies.Add(new SkillPolicy(Constants.SkillId.A1, 0));
                        break;
                    default:
                        throw new ArgumentException("Only works for Brutal, Nightmare, or UltraNightmare");
                }

                return new Tuple<Champion, List<SkillPolicy>>(Champion.CreateManeater(240, 3), policies);
            }

            static Tuple<Champion, List<SkillPolicy>> CreatePainkeeper(ClanBoss.Level level)
            {
                List<SkillPolicy> policies = new List<SkillPolicy>();

                switch (level)
                {
                    case ClanBoss.Level.Brutal:
                        break;
                    case ClanBoss.Level.Nightmare:
                        break;
                    case ClanBoss.Level.UltraNightmare:
                        policies.Add(new SkillPolicy(Constants.SkillId.A3, 0));
                        policies.Add(new SkillPolicy(Constants.SkillId.A2, 3));
                        policies.Add(new SkillPolicy(Constants.SkillId.A1, 0));
                        break;
                    default:
                        throw new ArgumentException("Only works for Brutal, Nightmare, or UltraNightmare");
                }

                return new Tuple<Champion, List<SkillPolicy>>(Champion.CreatePainkeeper(221, 3), policies);
            }

            static Tuple<Champion, List<SkillPolicy>> CreateAothar(ClanBoss.Level level)
            {
                List<SkillPolicy> policies = new List<SkillPolicy>()
                {
                    new SkillPolicy(Constants.SkillId.A3, 0),
                    new SkillPolicy(Constants.SkillId.A2, 0),
                    new SkillPolicy(Constants.SkillId.A1, 0),
                };

                int uiSpeed = 114;
                if (level == ClanBoss.Level.Nightmare)
                {
                    uiSpeed = 121;
                }

                return new Tuple<Champion, List<SkillPolicy>>(Champion.CreateAothar(uiSpeed, 1), policies);
            }

            static Tuple<Champion, List<SkillPolicy>> CreateRhazinScarhide(ClanBoss.Level level)
            {
                List<SkillPolicy> policies = new List<SkillPolicy>()
                {
                    new SkillPolicy(Constants.SkillId.A3, 0),
                    new SkillPolicy(Constants.SkillId.A2, 0),
                    new SkillPolicy(Constants.SkillId.A1, 0),
                };

                return new Tuple<Champion, List<SkillPolicy>>(Champion.CreateRhazinScarhide(177, 1), policies);
            }

            static Tuple<Champion, List<SkillPolicy>> CreateSeptimus(ClanBoss.Level level)
            {
                List<SkillPolicy> policies = new List<SkillPolicy>()
                {
                    new SkillPolicy(Constants.SkillId.A2, 0),
                    new SkillPolicy(Constants.SkillId.A1, 0),
                };

                return new Tuple<Champion, List<SkillPolicy>>(Champion.CreateSeptimus(175, 2), policies);
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
