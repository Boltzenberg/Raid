using RaidLib.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaidLib.Simulator
{
    public class ClanBossInBattle : IBattleParticipant
    {
        private ClanBoss clanBoss;
        private Skill[] skills;
        public double TurnMeter { get; private set; }
        public double TurnMeterIncreaseOnClockTick { get; private set; }
        public int TurnCount { get; private set; }
        public bool IsClanBoss { get { return true; } }
        public bool LeaveOutOfAllyAttack { get { return false; } }
        public string Name { get { return Constants.Names.ClanBoss; } }

        public ClanBossInBattle(ClanBoss clanBoss)
        {
            this.clanBoss = clanBoss;
            this.TurnCount = 0;
            this.TurnMeter = 0;
            this.TurnMeterIncreaseOnClockTick = Constants.TurnMeter.DeltaPerTurn(this.clanBoss.Speed);
            this.ActiveBuffs = new Dictionary<Constants.Buff, int>();
            this.ActiveDebuffs = new Dictionary<Constants.Debuff, int>();
            this.skills = new Skill[3];
            this.skills[0] = clanBoss.Skills.Where(s => s.Id == Constants.SkillId.A1).First();
            this.skills[1] = clanBoss.Skills.Where(s => s.Id == Constants.SkillId.A2).First();
            this.skills[2] = clanBoss.Skills.Where(s => s.Id == Constants.SkillId.A3).First();
        }

        private ClanBossInBattle(ClanBossInBattle other)
        {
            this.clanBoss = other.clanBoss;
            this.TurnCount = other.TurnCount;
            this.TurnMeter = other.TurnMeter;
            this.TurnMeterIncreaseOnClockTick = other.TurnMeterIncreaseOnClockTick;
            this.ActiveBuffs = new Dictionary<Constants.Buff, int>(other.ActiveBuffs);
            this.ActiveDebuffs = new Dictionary<Constants.Debuff, int>(other.ActiveDebuffs);
            this.skills = other.skills;
        }

        public IBattleParticipant Clone()
        {
            return new ClanBossInBattle(this);
        }

        public Dictionary<Constants.Buff, int> ActiveBuffs { get; private set; }
        public Dictionary<Constants.Debuff, int> ActiveDebuffs { get; private set; }

        public void GetAttacked(int hitCount)
        {

        }

        public void ApplyBuff(BuffToApply buff)
        {

        }

        public void ApplyDebuff(DebuffToApply debuff)
        {
            if (!this.ActiveBuffs.ContainsKey(Constants.Buff.BlockDebuffs) && this.ActiveDebuffs.Count <= 10)
            {
                this.ActiveDebuffs[debuff.Debuff] = debuff.Duration;
            }
        }

        public void ApplyEffect(Constants.Effect effect)
        {

        }

        public Dictionary<Constants.SkillId, int> GetSkillToCooldownMap()
        {
            Dictionary<Constants.SkillId, int> cooldowns = new Dictionary<Constants.SkillId, int>();
            return cooldowns;
        }

        public IEnumerable<Skill> GetPassiveSkills()
        {
            yield break;
        }

        public IEnumerable<Skill> AllAvailableSkills()
        {
            yield return NextAISkill();
        }

        public Skill NextAISkill()
        {
            return this.skills[this.TurnCount % this.skills.Length];
        }

        public Skill GetA1()
        {
            return this.skills.Where(s => s.Id == Constants.SkillId.A1).First();
        }

        public void AdditionalAttack()
        {

        }

        public void TakeTurn(Skill skill)
        {
            foreach (Constants.Debuff debuff in new List<Constants.Debuff>(this.ActiveDebuffs.Keys))
            {
                this.ActiveDebuffs[debuff]--;
                if (this.ActiveDebuffs[debuff] == 0)
                {
                    this.ActiveDebuffs.Remove(debuff);
                }
            }

            this.TurnMeter = 0;
            this.TurnCount++;
        }

        public void ClockTick()
        {
            this.TurnMeter += this.TurnMeterIncreaseOnClockTick;
        }
    }
}
