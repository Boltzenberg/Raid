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

        static Tuple<Champion, List<Constants.SkillId>, List<Constants.SkillId>> CreateDPS1(ClanBoss.Level level)
        {
            return new Tuple<Champion, List<Constants.SkillId>, List<Constants.SkillId>>(Generic.Create("DPS1", 99, 223, 3, 0), Generic.AISkills, new List<Constants.SkillId>());
        }

        static Tuple<Champion, List<Constants.SkillId>, List<Constants.SkillId>> CreateKreela(ClanBoss.Level level)
        {
            int uiSpeed = 172;
            if (level == ClanBoss.Level.Nightmare)
            {
                uiSpeed = 181;
            }

            List<Constants.SkillId> startupSkillOrder = new List<Constants.SkillId>();
            switch (level)
            {
                case ClanBoss.Level.UltraNightmare:
                    startupSkillOrder.Add(Constants.SkillId.A1);
                    startupSkillOrder.Add(Constants.SkillId.A3);
                    startupSkillOrder.Add(Constants.SkillId.A2);
                    break;
                case ClanBoss.Level.Nightmare:
                    startupSkillOrder.Add(Constants.SkillId.A1);
                    startupSkillOrder.Add(Constants.SkillId.A1);
                    startupSkillOrder.Add(Constants.SkillId.A3);
                    startupSkillOrder.Add(Constants.SkillId.A2);
                    break;
                case ClanBoss.Level.Brutal:
                    startupSkillOrder.Add(Constants.SkillId.A1);
                    startupSkillOrder.Add(Constants.SkillId.A1);
                    startupSkillOrder.Add(Constants.SkillId.A3);
                    startupSkillOrder.Add(Constants.SkillId.A2);
                    break;
                default:
                    throw new ArgumentException("Only works for Brutal, Nightmare, or UltraNightmare");
            }

            return new Tuple<Champion, List<Constants.SkillId>, List<Constants.SkillId>>(KreelaWitchArm.Create(uiSpeed, 0, 0), KreelaWitchArm.AISkills, startupSkillOrder);
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

        static Tuple<Champion, List<Constants.SkillId>, List<Constants.SkillId>> CreateSlowBoi(ClanBoss.Level level)
        {
            return new Tuple<Champion, List<Constants.SkillId>, List<Constants.SkillId>>(Generic.Create("SlowBoi", 0, 123.0d), Generic.AISkills, new List<Constants.SkillId>());
        }

        public static List<Champion.CreateChampion> TeamWithKreela()
        {
            return new List<Champion.CreateChampion>()
                {
                    CreateSlowBoi,
                    CreateDPS1,
                    CreateKreela,
                    CreatePainkeeper,
                    CreateManeater,
                };
        }

        public static List<Champion.CreateChampion> TeamWithCatacombCouncilor()
        {
            return new List<Champion.CreateChampion>()
                {
                    CreateSlowBoi,
                    CreateDPS1,
                    CreateCatacombCouncilor,
                    CreatePainkeeper,
                    CreateManeater,
                };
        }
    }
}
