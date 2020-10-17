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
        // Each iteration through the simulation loop
        public int ClockTick { get; private set; }

        // How many turns has the clan boss had at this point?
        public int ClanBossTurn { get; private set; }

        // Who went in this clock tick?
        public string ActorName { get; private set; }

        public Constants.SkillId Skill { get; private set; }
        
        public string SkillName { get; private set; }

        public List<ChampionStats> Champions { get; private set; }

        public ClanBossStats ClanBoss { get; private set; }

        public ClanBossBattleResult(int clockTick, int clanBossTurn, string actorName, Constants.SkillId skill, string skillName, List<ChampionStats> championStats, ClanBossStats clanBossStats)
        {
            this.ClockTick = clockTick;
            this.ClanBossTurn = clanBossTurn;
            this.ActorName = actorName;
            this.Skill = skill;
            this.SkillName = skillName;
            this.Champions = championStats;
            this.ClanBoss = clanBossStats;
        }

        public class ClanBossStats
        {
            public double TurnMeter { get; private set; }
            public Dictionary<Constants.Buff, int> ActiveBuffs { get; private set; }
            public Dictionary<Constants.Debuff, int> ActiveDebuffs { get; private set; }

            public ClanBossStats(double turnMeter, Dictionary<Constants.Buff, int> activeBuffs, Dictionary<Constants.Debuff, int> activeDebuffs)
            {
                this.TurnMeter = turnMeter;
                this.ActiveBuffs = activeBuffs;
                this.ActiveDebuffs = activeDebuffs;
            }
        }

        public class ChampionStats
        {
            public Champion Champion { get; private set; }
            public double TurnMeter { get; private set; }
            public Dictionary<Constants.Buff, int> ActiveBuffs { get; private set; }
            public Dictionary<Constants.Debuff, int> ActiveDebuffs { get; private set; }
            public Dictionary<Constants.SkillId, int> SkillCooldowns { get; private set; }

            public ChampionStats(Champion champion, double turnMeter, Dictionary<Constants.Buff, int> activeBuffs, Dictionary<Constants.Debuff, int> activeDebuffs, Dictionary<Constants.SkillId, int> skillCooldowns)
            {
                this.Champion = champion;
                this.TurnMeter = turnMeter;
                this.ActiveBuffs = activeBuffs;
                this.ActiveDebuffs = activeDebuffs;
                this.SkillCooldowns = skillCooldowns;
            }
        }
    }
}
