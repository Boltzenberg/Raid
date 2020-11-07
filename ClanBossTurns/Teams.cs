using RaidLib.DataModel;
using RaidLib.DataModel.Champions;
using RaidLib.Simulator;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ClanBossTurns
{
    public static class Teams
    {
        public delegate Tuple<Champion, List<Constants.SkillId>, List<Constants.SkillId>> CreateChampion(ClanBoss.Level level);

        public static class Chilli
        {
            static Tuple<Champion, List<Constants.SkillId>, List<Constants.SkillId>> CreateFrozenBanshee(ClanBoss.Level level)
            {
                return new Tuple<Champion, List<Constants.SkillId>, List<Constants.SkillId>>(FrozenBanshee.Create(156, 0, 0), FrozenBanshee.AISkills, new List<Constants.SkillId>());
            }

            static Tuple<Champion, List<Constants.SkillId>, List<Constants.SkillId>> CreateRhazinScarhide(ClanBoss.Level level)
            {
                return new Tuple<Champion, List<Constants.SkillId>, List<Constants.SkillId>>(RhazinScarhide.Create(149, 0, 0), RhazinScarhide.AISkills, new List<Constants.SkillId>());
            }

            static Tuple<Champion, List<Constants.SkillId>, List<Constants.SkillId>> CreateSeptimus(ClanBoss.Level level)
            {
                return new Tuple<Champion, List<Constants.SkillId>, List<Constants.SkillId>>(Septimus.Create(151, 0, 0), Septimus.AISkills, new List<Constants.SkillId>());
            }

            static Tuple<Champion, List<Constants.SkillId>, List<Constants.SkillId>> CreateCoffinSmasher(ClanBoss.Level level)
            {
                return new Tuple<Champion, List<Constants.SkillId>, List<Constants.SkillId>>(CoffinSmasher.Create(96, 0, 0), CoffinSmasher.AISkills, new List<Constants.SkillId>());
            }

            static Tuple<Champion, List<Constants.SkillId>, List<Constants.SkillId>> CreateSkullcrusher(ClanBoss.Level level)
            {
                return new Tuple<Champion, List<Constants.SkillId>, List<Constants.SkillId>>(Skullcrusher.Create(153, 0, 0), Skullcrusher.AISkills, new List<Constants.SkillId>());
            }

            public static List<CreateChampion> ChampionCreators()
            {
                return new List<CreateChampion>()
                {
                    CreateFrozenBanshee,
                    CreateSeptimus,
                    CreateRhazinScarhide,
                    CreateCoffinSmasher,
                    CreateSkullcrusher
                };
            }
        }

        public static class ChilliNM
        {
            static Tuple<Champion, List<Constants.SkillId>, List<Constants.SkillId>> CreateFrozenBanshee(ClanBoss.Level level)
            {
                return new Tuple<Champion, List<Constants.SkillId>, List<Constants.SkillId>>(FrozenBanshee.Create(171, 0, 0), FrozenBanshee.AISkills, new List<Constants.SkillId>());
            }

            static Tuple<Champion, List<Constants.SkillId>, List<Constants.SkillId>> CreateRhazinScarhide(ClanBoss.Level level)
            {
                return new Tuple<Champion, List<Constants.SkillId>, List<Constants.SkillId>>(RhazinScarhide.Create(171, 0, 0), RhazinScarhide.AISkills, new List<Constants.SkillId>());
            }

            static Tuple<Champion, List<Constants.SkillId>, List<Constants.SkillId>> CreateSeptimus(ClanBoss.Level level)
            {
                return new Tuple<Champion, List<Constants.SkillId>, List<Constants.SkillId>>(Septimus.Create(171, 0, 0), Septimus.AISkills, new List<Constants.SkillId>());
            }

            static Tuple<Champion, List<Constants.SkillId>, List<Constants.SkillId>> CreateCoffinSmasher(ClanBoss.Level level)
            {
                return new Tuple<Champion, List<Constants.SkillId>, List<Constants.SkillId>>(CoffinSmasher.Create(171, 0, 0), CoffinSmasher.AISkills, new List<Constants.SkillId>());
            }

            static Tuple<Champion, List<Constants.SkillId>, List<Constants.SkillId>> CreateSkullcrusher(ClanBoss.Level level)
            {
                return new Tuple<Champion, List<Constants.SkillId>, List<Constants.SkillId>>(Skullcrusher.Create(170, 0, 0), Skullcrusher.AISkills, new List<Constants.SkillId>());
            }

            public static IBattleParticipant GetStunTarget(List<IBattleParticipant> bps)
            {
                return bps.Where(bp => bp.Name == "Skullcrusher").First();
            }

            public static List<CreateChampion> ChampionCreators()
            {
                return new List<CreateChampion>()
                {
                    CreateSkullcrusher,
                    CreateFrozenBanshee,
                    CreateSeptimus,
                    CreateRhazinScarhide,
                    CreateCoffinSmasher,
                };
            }
        }

        public static class Rust
        {
            static Tuple<Champion, List<Constants.SkillId>, List<Constants.SkillId>> CreateManeater(ClanBoss.Level level)
            {
                List<Constants.SkillId> skillsToUse = new List<Constants.SkillId>()
                {
                    Constants.SkillId.A3,
                    Constants.SkillId.A1,
                };

                return new Tuple<Champion, List<Constants.SkillId>, List<Constants.SkillId>>(Maneater.Create(254, 0, 0), skillsToUse, new List<Constants.SkillId>());
            }

            static Tuple<Champion, List<Constants.SkillId>, List<Constants.SkillId>> CreatePainkeeper(ClanBoss.Level level)
            {
                List<Constants.SkillId> skillsToUse = new List<Constants.SkillId>()
                {
                    Constants.SkillId.A3,
                    Constants.SkillId.A2,
                    Constants.SkillId.A1,
                };

                return new Tuple<Champion, List<Constants.SkillId>, List<Constants.SkillId>>(Painkeeper.Create(227, 0, 0), skillsToUse, new List<Constants.SkillId>());
            }

            static Tuple<Champion, List<Constants.SkillId>, List<Constants.SkillId>> CreateDPS1(ClanBoss.Level level)
            {
                return new Tuple<Champion, List<Constants.SkillId>, List<Constants.SkillId>>(
                    new Champion("DPS1", 185, 185, 0, 0, new List<Skill>() { new Skill("A1", Constants.SkillId.A1, 0, TurnAction.AttackOneEnemy()) }),
                    new List<Constants.SkillId>() { Constants.SkillId.A1 },
                    new List<Constants.SkillId>());
            }

            static Tuple<Champion, List<Constants.SkillId>, List<Constants.SkillId>> CreateDPS2(ClanBoss.Level level)
            {
                return new Tuple<Champion, List<Constants.SkillId>, List<Constants.SkillId>>(
                    new Champion("DPS2", 185, 185, 0, 0, new List<Skill>() { new Skill("A1", Constants.SkillId.A1, 0, TurnAction.AttackOneEnemy()) }),
                    new List<Constants.SkillId>() { Constants.SkillId.A1 },
                    new List<Constants.SkillId>());
            }

            static Tuple<Champion, List<Constants.SkillId>, List<Constants.SkillId>> CreateSlowboi(ClanBoss.Level level)
            {
                return new Tuple<Champion, List<Constants.SkillId>, List<Constants.SkillId>>(
                    new Champion("Slowboi", 128, 128, 0, 0, new List<Skill>() { new Skill("A1", Constants.SkillId.A1, 0, TurnAction.AttackOneEnemy()) }),
                    new List<Constants.SkillId>() { Constants.SkillId.A1 },
                    new List<Constants.SkillId>());
            }

            public static List<CreateChampion> ChampionCreators()
            {
                return new List<CreateChampion>()
                {
                    CreateManeater,
                    CreatePainkeeper,
                    CreateDPS1,
                    CreateDPS2,
                    CreateSlowboi
                };
            }
        }

        public static class UnkillableBase
        {
            static Tuple<Champion, List<Constants.SkillId>, List<Constants.SkillId>> CreateManeater(ClanBoss.Level level)
            {
                List<Constants.SkillId> skillsToUse = new List<Constants.SkillId>()
                {
                    Constants.SkillId.A3,
                    Constants.SkillId.A1,
                };

                return new Tuple<Champion, List<Constants.SkillId>, List<Constants.SkillId>>(Maneater.Create(227, 2, 0), skillsToUse, new List<Constants.SkillId>());
            }

            static Tuple<Champion, List<Constants.SkillId>, List<Constants.SkillId>> CreatePainkeeper(ClanBoss.Level level)
            {
                List<Constants.SkillId> skillsToUse = new List<Constants.SkillId>()
                {
                    Constants.SkillId.A3,
                    Constants.SkillId.A2,
                    Constants.SkillId.A1,
                };

                return new Tuple<Champion, List<Constants.SkillId>, List<Constants.SkillId>>(Painkeeper.Create(213, 2, 0), skillsToUse, new List<Constants.SkillId>());
            }

            static Tuple<Champion, List<Constants.SkillId>, List<Constants.SkillId>> CreateDPS1(ClanBoss.Level level)
            {
                return new Tuple<Champion, List<Constants.SkillId>, List<Constants.SkillId>>(
                    new Champion("DPS1", 169, 169, 0, 0, new List<Skill>() { new Skill("A1", Constants.SkillId.A1, 0, TurnAction.AttackOneEnemy()) }),
                    new List<Constants.SkillId>() { Constants.SkillId.A1 },
                    new List<Constants.SkillId>());
            }

            static Tuple<Champion, List<Constants.SkillId>, List<Constants.SkillId>> CreateDPS2(ClanBoss.Level level)
            {
                return new Tuple<Champion, List<Constants.SkillId>, List<Constants.SkillId>>(
                    new Champion("DPS2", 169, 169, 0, 0, new List<Skill>() { new Skill("A1", Constants.SkillId.A1, 0, TurnAction.AttackOneEnemy()) }),
                    new List<Constants.SkillId>() { Constants.SkillId.A1 },
                    new List<Constants.SkillId>());
            }

            static Tuple<Champion, List<Constants.SkillId>, List<Constants.SkillId>> CreateSlowboi(ClanBoss.Level level)
            {
                return new Tuple<Champion, List<Constants.SkillId>, List<Constants.SkillId>>(
                    new Champion("Slowboi", 106, 106, 0, 0, new List<Skill>() { new Skill("A1", Constants.SkillId.A1, 0, TurnAction.AttackOneEnemy()) }),
                    new List<Constants.SkillId>() { Constants.SkillId.A1 },
                    new List<Constants.SkillId>());
            }

            public static List<CreateChampion> ChampionCreators()
            {
                return new List<CreateChampion>()
                {
                    CreateManeater,
                    CreatePainkeeper,
                    CreateDPS1,
                    CreateDPS2,
                    CreateSlowboi
                };
            }
        }

        public static class UnkillableDWJBase
        {
            static Tuple<Champion, List<Constants.SkillId>, List<Constants.SkillId>> CreateManeater(ClanBoss.Level level)
            {
                List<Constants.SkillId> skillsToUse = new List<Constants.SkillId>()
                {
                    Constants.SkillId.A3,
                    Constants.SkillId.A1,
                };

                return new Tuple<Champion, List<Constants.SkillId>, List<Constants.SkillId>>(Maneater.Create(240, 3, 0), skillsToUse, new List<Constants.SkillId>());
            }

            static Tuple<Champion, List<Constants.SkillId>, List<Constants.SkillId>> CreatePainkeeper(ClanBoss.Level level)
            {
                List<Constants.SkillId> skillsToUse = new List<Constants.SkillId>()
                {
                    Constants.SkillId.A3,
                    Constants.SkillId.A2,
                    Constants.SkillId.A1,
                };

                return new Tuple<Champion, List<Constants.SkillId>, List<Constants.SkillId>>(Painkeeper.Create(221, 3, 0), skillsToUse, new List<Constants.SkillId>());
            }

            static Tuple<Champion, List<Constants.SkillId>, List<Constants.SkillId>> CreateDPS1(ClanBoss.Level level)
            {
                return new Tuple<Champion, List<Constants.SkillId>, List<Constants.SkillId>>(
                    new Champion("DPS1", 91, 177, 1, 0, new List<Skill>() { new Skill("A1", Constants.SkillId.A1, 0, TurnAction.AttackOneEnemy()) }),
                    new List<Constants.SkillId>() { Constants.SkillId.A1 },
                    new List<Constants.SkillId>());
            }

            static Tuple<Champion, List<Constants.SkillId>, List<Constants.SkillId>> CreateDPS2(ClanBoss.Level level)
            {
                return new Tuple<Champion, List<Constants.SkillId>, List<Constants.SkillId>>(
                    new Champion("DPS2", 102, 175, 2, 0, new List<Skill>() { new Skill("A1", Constants.SkillId.A1, 0, TurnAction.AttackOneEnemy()) }),
                    new List<Constants.SkillId>() { Constants.SkillId.A1 },
                    new List<Constants.SkillId>());
            }

            static Tuple<Champion, List<Constants.SkillId>, List<Constants.SkillId>> CreateSlowboi(ClanBoss.Level level)
            {
                return new Tuple<Champion, List<Constants.SkillId>, List<Constants.SkillId>>(
                    new Champion("Slowboi", 92, 114, 1, 0, new List<Skill>() { new Skill("A1", Constants.SkillId.A1, 0, TurnAction.AttackOneEnemy()) }),
                    new List<Constants.SkillId>() { Constants.SkillId.A1 },
                    new List<Constants.SkillId>());
            }

            public static List<CreateChampion> ChampionCreators()
            {
                return new List<CreateChampion>()
                {
                    CreateManeater,
                    CreatePainkeeper,
                    CreateDPS1,
                    CreateDPS2,
                    CreateSlowboi
                };
            }
        }

        public static class Gunga
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

            public static List<CreateChampion> ChampionCreators()
            {
                return new List<CreateChampion>()
                {
                    CreateRhazinScarhide,
                    CreateFrozenBanshee,
                    //CreateGravechillKiller,
                    CreateBulwark,
                    CreateManeater,
                    CreatePainkeeper
                };
            }
        }

        public static class Ash
        {
            static Tuple<Champion, List<Constants.SkillId>, List<Constants.SkillId>> CreateManeater(ClanBoss.Level level)
            {
                return new Tuple<Champion, List<Constants.SkillId>, List<Constants.SkillId>>(Maneater.Create(98 + 143, 3, 0), Maneater.AISkills, new List<Constants.SkillId>());
            }

            static Tuple<Champion, List<Constants.SkillId>, List<Constants.SkillId>> CreatePainkeeper(ClanBoss.Level level)
            {
                List<Constants.SkillId> startupSkillOrder = new List<Constants.SkillId>()
                {
                    Constants.SkillId.A3,
                    Constants.SkillId.A1,
                };

                return new Tuple<Champion, List<Constants.SkillId>, List<Constants.SkillId>>(Painkeeper.Create(102 + 119, 1, 0), Painkeeper.AISkills, startupSkillOrder);
            }

            static Tuple<Champion, List<Constants.SkillId>, List<Constants.SkillId>> CreateFrozenBanshee(ClanBoss.Level level)
            {
                return new Tuple<Champion, List<Constants.SkillId>, List<Constants.SkillId>>(FrozenBanshee.Create(99 + 78, 0, 0), FrozenBanshee.AISkills, new List<Constants.SkillId>());
            }

            static Tuple<Champion, List<Constants.SkillId>, List<Constants.SkillId>> CreateOccultBrawler(ClanBoss.Level level)
            {
                return new Tuple<Champion, List<Constants.SkillId>, List<Constants.SkillId>>(OccultBrawler.Create(98 + 77, 0, 0), OccultBrawler.AISkills, new List<Constants.SkillId>());
            }

            static Tuple<Champion, List<Constants.SkillId>, List<Constants.SkillId>> CreateAothar(ClanBoss.Level level)
            {
                return new Tuple<Champion, List<Constants.SkillId>, List<Constants.SkillId>>(Aothar.Create(92 + 24, 0, 0), Aothar.AISkills, new List<Constants.SkillId>());
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
        }
    }
}
