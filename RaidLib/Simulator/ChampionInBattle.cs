using RaidLib.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RaidLib.Simulator
{
    public class ChampionInBattle
    {
        private List<SkillInBattle> skillsToUse;
        private int turnCount;

        public ChampionInBattle(Champion champion, List<SkillPolicy> skillPolicies, Clock clock)
        {
            this.Champ = champion;
            this.TurnMeter = 0;
            this.turnCount = 0;
            this.ActiveBuffs = new Dictionary<Constants.Buff, int>();
            this.ActiveDebuffs = new Dictionary<Constants.Debuff, int>();
            this.TurnMeterIncreaseOnClockTick = Constants.TurnMeter.DeltaPerTurn(this.Champ.EffectiveSpeed);
            this.skillsToUse = new List<SkillInBattle>();
            foreach (SkillPolicy skillPolicy in skillPolicies)
            {
                Skill skill = this.Champ.Skills.Where(s => s.Id == skillPolicy.SkillId).First();
                if (Constants.Skills.Active.Contains(skill.Id))
                {
                    skillsToUse.Add(new SkillInBattle(skill, skillPolicy));
                }
            }
            clock.OnTick += this.OnClockTick;
        }

        public Dictionary<Constants.Buff, int> ActiveBuffs { get; private set; }
        public Dictionary<Constants.Debuff, int> ActiveDebuffs { get; private set; }

        public void ApplyBuff(BuffToApply buff)
        {
            if (this.ActiveBuffs.Count < 10)
            {
                this.ActiveBuffs[buff.Buff] = buff.Duration;
            }
        }

        public void ApplyDebuff(DebuffToApply debuff)
        {
            if (!this.ActiveBuffs.ContainsKey(Constants.Buff.BlockDebuffs) && this.ActiveDebuffs.Count <= 10)
            {
                this.ActiveDebuffs[debuff.Debuff] = debuff.Duration;
            }
        }

        public Dictionary<Constants.SkillId, int> SkillCooldowns
        {
            get
            {
                Dictionary<Constants.SkillId, int> results = new Dictionary<Constants.SkillId, int>();
                foreach (SkillInBattle sib in this.skillsToUse)
                {
                    results[sib.Skill.Id] = sib.CooldownsRemaining;
                }

                return results;
            }
        }

        public bool IsUnkillable
        {
            get
            {
                return this.ActiveBuffs.ContainsKey(Constants.Buff.Unkillable);
            }
        }

        public void GetAttacked(int hitCount)
        {

        }

        public void ApplyEffect(Constants.Effect effect)
        {
            switch (effect)
            {
                case Constants.Effect.FillTurnMeterBy10Percent:
                    {
                        this.TurnMeter += 0.1f;
                        break;
                    }
                case Constants.Effect.ReduceCooldownBy1:
                    {
                        foreach (SkillInBattle skill in this.skillsToUse)
                        {
                            if (skill.CooldownsRemaining > 0)
                            {
                                skill.CooldownsRemaining--;
                            }
                        }
                        break;
                    }
                default:
                    break;
            }
        }

        public Champion Champ { get; private set; }

        public float TurnMeter { get; private set; }

        public float TurnMeterIncreaseOnClockTick { get; private set; }

        public Skill TakeTurn()
        {
            this.turnCount++;

            SkillInBattle skillToUse = null;
            foreach (SkillInBattle sib in this.skillsToUse)
            {
                if (sib.SkillPolicy.DelayBeforeFirstUse > this.turnCount)
                {
                    continue;
                }

                if (sib.CooldownsRemaining > 0)
                {
                    continue;
                }

                skillToUse = sib;
                break;
            }

            skillToUse.CooldownsRemaining = skillToUse.Skill.Cooldown;

            foreach (SkillInBattle sib in this.skillsToUse)
            {
                if (sib.CooldownsRemaining > 0)
                {
                    sib.CooldownsRemaining--;
                }
            }

            foreach (Constants.Buff buff in new List<Constants.Buff>(this.ActiveBuffs.Keys))
            {
                this.ActiveBuffs[buff]--;
                if (this.ActiveBuffs[buff] == 0)
                {
                    this.ActiveBuffs.Remove(buff);
                }
            }

            foreach (Constants.Debuff debuff in new List<Constants.Debuff>(this.ActiveDebuffs.Keys))
            {
                this.ActiveDebuffs[debuff]--;
                if (this.ActiveDebuffs[debuff] == 0)
                {
                    this.ActiveDebuffs.Remove(debuff);
                }
            }

            //Console.WriteLine(" {0} uses skill {1} ({2}) with turn meter {3}!", this.Champ.Name, skillToUse.Skill.Id, skillToUse.Skill.Name, this.TurnMeter);
            Console.WriteLine(" {0} Turn {1}: skill {2} ({3})", this.Champ.Name, this.turnCount, skillToUse.Skill.Id, skillToUse.Skill.Name);
            this.TurnMeter = 0;

            return skillToUse.Skill;
        }

        private void OnClockTick(object sender)
        {
            this.TurnMeter += this.TurnMeterIncreaseOnClockTick;
        }
    }
}
