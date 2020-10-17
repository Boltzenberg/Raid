using RaidLib.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaidLib.Simulator
{
    public class ClanBossInBattle
    {
        private ClanBoss clanBoss;
        private Skill[] skills;
        private int turnCount;

        public ClanBossInBattle(ClanBoss clanBoss, Clock clock)
        {
            this.clanBoss = clanBoss;
            this.TurnMeter = 0;
            this.TurnMeterIncreaseOnClockTick = Constants.TurnMeter.DeltaPerTurn(this.clanBoss.Speed);
            this.ActiveBuffs = new Dictionary<Constants.Buff, int>();
            this.ActiveDebuffs = new Dictionary<Constants.Debuff, int>();
            this.skills = new Skill[3];
            this.skills[0] = clanBoss.Skills.Where(s => s.Id == Constants.SkillId.A1).First();
            this.skills[1] = clanBoss.Skills.Where(s => s.Id == Constants.SkillId.A2).First();
            this.skills[2] = clanBoss.Skills.Where(s => s.Id == Constants.SkillId.A3).First();
            this.turnCount = 0;
            clock.OnTick += this.OnClockTick;
        }

        public Dictionary<Constants.Buff, int> ActiveBuffs { get; private set; }
        public Dictionary<Constants.Debuff, int> ActiveDebuffs { get; private set; }

        public void ApplyBuff(BuffToApply buff)
        {

        }

        public void ApplyDebuff(DebuffToApply debuff)
        {

        }

        public double TurnMeter { get; private set; }

        public double TurnMeterIncreaseOnClockTick { get; private set; }

        public Skill TakeTurn()
        {
            int skill = this.turnCount % this.skills.Length;
            this.turnCount++;
            //Console.WriteLine("Clan Boss uses skill {0} ({1}) on turn {2} with turn meter {3}!", skills[skill].Id, skills[skill].Name, this.turnCount, this.TurnMeter);
            Console.WriteLine("Clan Boss Turn {0}: skill {1} ({2})", this.turnCount, skills[skill].Id, skills[skill].Name);
            this.TurnMeter = 0;

            return this.skills[skill];
        }

        private void OnClockTick(object sender)
        {
            this.TurnMeter += this.TurnMeterIncreaseOnClockTick;
        }
    }
}
