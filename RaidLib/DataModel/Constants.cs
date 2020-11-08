using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaidLib.DataModel
{
    public static class Constants
    {
        public static class Names
        {
            public const string ClanBoss = "CLAN BOSS";
        }

        public static class SetBonus
        {
            public const double Speed = 0.12d;
            public const double Perception = 0.05d;
        }

        public enum SkillId
        {
            None,
            RE,
            A1,
            A2,
            A3,
            A4,
            P1
        }

        public enum Buff
        {
            BlockDebuffs,
            Counterattack,
            Unkillable,
        }

        public enum Debuff
        {
            DecreaseDefense60,
            DecreaseAttack50,
            HPBurn,
            Poison5,
            PoisonSensitivity,
            Stun,
            Weaken15,
        }

        public enum Target
        {
            None,
            AllEnemies,
            OneEnemy,
            Self,      // Just me
            OneAlly,   // Just one ally
            ThreeRandomAllies, // Usually used for ally attack
            AllAllies, // Everybody on my team but me
            FullTeam   // My whole team
        }

        public enum Effect
        {
            None,
            AllyAttack,
            FillTurnMeterBy10Percent,
            ReduceDebuffCooldownBy1,
            ReduceSkillCooldownBy1,
        }

        public enum TimeInTurn
        {
            Beginning,
            End
        }

        public static class TurnMeter
        {
            public const double Full = 1.0d;

            public static double DeltaPerTurn(double effectiveSpeed)
            {
                //const double magicSpeedNumber = 10000 / 7;
                const double magicSpeedNumber = 1428.571429d;
                return effectiveSpeed / magicSpeedNumber;
            }
        }

        public static class Skills
        {
            public static readonly HashSet<SkillId> Active = new HashSet<SkillId> { SkillId.A1, SkillId.A2, SkillId.A3, SkillId.A4 };
        }
    }
}
