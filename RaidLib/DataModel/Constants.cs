using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaidLib.DataModel
{
    public static class Constants
    {
        public static class SetBonus
        {
            public const float Speed = 0.12f;
        }

        public enum SkillId
        {
            A1,
            A2,
            A3,
            A4,
            P1
        }

        public enum Buff
        {
            BlockDebuffs,
            Unkillable,
        }

        public enum Debuff
        {
            PoisonSensitivity,
            DecreaseDefense60,
            DecreaseAttack50,
            Poison5,
            Weaken15,
            HPBurn,
        }

        public enum Effect
        {
            None,
            FillSelfTurnMeterBy10Percent,
            AllyReduceCooldownBy1,
        }

        public static class Effects
        {
            public static readonly HashSet<Effect> ImpactsCurrentChamp = new HashSet<Effect> { Effect.FillSelfTurnMeterBy10Percent };
            public static readonly HashSet<Effect> ImpactsTeam = new HashSet<Effect> { };
            public static readonly HashSet<Effect> ImpactsAllies = new HashSet<Effect> { Effect.AllyReduceCooldownBy1 };
        }

        public static class TurnMeter
        {
            public const float Full = 1.0f;

            public static float DeltaPerTurn(float effectiveSpeed)
            {
                const float magicSpeedNumber = 10000 / 7;
                return effectiveSpeed / magicSpeedNumber;
            }
        }

        public static class Skills
        {
            public static readonly HashSet<SkillId> Active = new HashSet<SkillId> { SkillId.A1, SkillId.A2, SkillId.A3, SkillId.A4 };
        }
    }
}
