using RaidLib.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaidLib.Simulator
{
    public class ClanBossBattleResult
    {
        public class Attack
        {
            public string ActorName { get; private set; }

            public int ActorTurn { get; private set; }

            public double ActorTurnMeter { get; private set; }

            public Constants.SkillId Skill { get; private set; }

            public string SkillName { get; private set; }

            public Constants.SkillId ExpectedAISkill { get; private set; }

            public Attack(string actorName, int actorTurn, double actorTurnMeter, Constants.SkillId skill, string skillName, Constants.SkillId expectedAISkill)
            {
                this.ActorName = actorName;
                this.ActorTurn = actorTurn;
                this.ActorTurnMeter = actorTurnMeter;
                this.Skill = skill;
                this.SkillName = skillName;
                this.ExpectedAISkill = expectedAISkill;
            }
        }

        public int ClanBossTurn { get; private set; }

        public Attack AttackDetails { get; private set; }

        public List<Attack> AdditionalAttacks { get; private set; }

        public List<BattleParticipantStats> BattleParticipants { get; private set; }

        public ClanBossBattleResult(int clanBossTurn, Attack attackDetails, List<BattleParticipantStats> battleParticipantStats, List<Attack> additionalAttacks)
        {
            this.ClanBossTurn = clanBossTurn;
            this.AttackDetails = attackDetails;
            this.BattleParticipants = battleParticipantStats;
            this.AdditionalAttacks = additionalAttacks;
        }

        public class BattleParticipantStats
        {
            public string Name { get; private set; }
            public bool IsClanBoss { get; private set; }
            public double TurnMeter { get; private set; }
            public Dictionary<Constants.Buff, int> ActiveBuffs { get; private set; }
            public Dictionary<Constants.Debuff, int> ActiveDebuffs { get; private set; }
            public Dictionary<Constants.SkillId, int> SkillCooldownMap { get; private set; }

            public BattleParticipantStats(string name, bool isClanBoss, double turnMeter, Dictionary<Constants.Buff, int> activeBuffs, Dictionary<Constants.Debuff, int> activeDebuffs, Dictionary<Constants.SkillId, int> skillCooldownMap)
            {
                this.Name = name;
                this.IsClanBoss = isClanBoss;
                this.TurnMeter = turnMeter;
                this.ActiveBuffs = activeBuffs;
                this.ActiveDebuffs = activeDebuffs;
                this.SkillCooldownMap = skillCooldownMap;
            }
        }
    }
}
