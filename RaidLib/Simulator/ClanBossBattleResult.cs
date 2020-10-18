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
        public int ClanBossTurn { get; private set; }

        public string ActorName { get; private set; }

        public int ActorTurn { get; private set; }

        public Constants.SkillId Skill { get; private set; }
        
        public string SkillName { get; private set; }

        public Constants.SkillId ExpectedAISkill { get; private set; }

        public List<BattleParticipantStats> BattleParticipants { get; private set; }

        public ClanBossBattleResult(int clanBossTurn, string actorName, int actorTurn, Constants.SkillId skill, string skillName, Constants.SkillId aiSkill, List<BattleParticipantStats> battleParticipantStats)
        {
            this.ClanBossTurn = clanBossTurn;
            this.ActorName = actorName;
            this.ActorTurn = actorTurn;
            this.Skill = skill;
            this.SkillName = skillName;
            this.ExpectedAISkill = aiSkill;
            this.BattleParticipants = battleParticipantStats;
        }

        public class BattleParticipantStats
        {
            public string Name { get; private set; }
            public bool IsClanBoss { get; private set; }
            public double TurnMeter { get; private set; }
            public Dictionary<Constants.Buff, int> ActiveBuffs { get; private set; }

            public BattleParticipantStats(string name, bool isClanBoss, double turnMeter, Dictionary<Constants.Buff, int> activeBuffs)
            {
                this.Name = name;
                this.IsClanBoss = isClanBoss;
                this.TurnMeter = turnMeter;
                this.ActiveBuffs = activeBuffs;
            }
        }
    }
}
