﻿using RaidLib.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;

namespace RaidLib.Simulator
{
    public class ChampionInBattle : IBattleParticipant
    {
        private List<SkillInBattle> skillsToUse;
        private List<Constants.SkillId> startupSkillOrder;

        public Dictionary<Constants.Buff, int> ActiveBuffs { get; private set; }
        public Dictionary<Constants.Debuff, int> ActiveDebuffs { get; private set; }
        public Champion Champ { get; private set; }
        public double TurnMeter { get; private set; }
        public int TurnCount { get; private set; }
        public bool IsClanBoss { get { return false; } }
        public string Name { get { return this.Champ.Name; } }
        public bool LeaveOutOfAllyAttack { get; set; }

        public ChampionInBattle(Champion champion, List<Constants.SkillId> skillsToUseInThisBattle, List<Constants.SkillId> startupSkillOrder)
        {
            this.Champ = champion;
            this.TurnCount = 0;
            this.TurnMeter = 0;
            this.ActiveBuffs = new Dictionary<Constants.Buff, int>();
            this.ActiveDebuffs = new Dictionary<Constants.Debuff, int>();
            this.skillsToUse = new List<SkillInBattle>();
            foreach (Constants.SkillId skillId in skillsToUseInThisBattle)
            {
                Skill skill = this.Champ.Skills.Where(s => s.Id == skillId).First();
                if (Constants.Skills.Active.Contains(skill.Id))
                {
                    this.skillsToUse.Add(new SkillInBattle(skill));
                }
            }
            this.startupSkillOrder = startupSkillOrder;
        }

        private ChampionInBattle(ChampionInBattle other)
        {
            this.Champ = other.Champ;
            this.TurnCount = other.TurnCount;
            this.TurnMeter = other.TurnMeter;
            this.ActiveBuffs = new Dictionary<Constants.Buff, int>(other.ActiveBuffs);
            this.ActiveDebuffs = new Dictionary<Constants.Debuff, int>(other.ActiveDebuffs);
            this.LeaveOutOfAllyAttack = other.LeaveOutOfAllyAttack;
            this.skillsToUse = new List<SkillInBattle>();
            foreach (SkillInBattle sib in other.skillsToUse)
            {
                this.skillsToUse.Add(new SkillInBattle(sib));
            }
            this.startupSkillOrder = other.startupSkillOrder;
        }

        public IBattleParticipant Clone()
        {
            return new ChampionInBattle(this);
        }

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
                case Constants.Effect.ReduceDebuffCooldownBy1:
                    {
                        foreach (Constants.Debuff debuff in new List<Constants.Debuff>(this.ActiveDebuffs.Keys))
                        {
                            if (this.ActiveDebuffs[debuff] == 1)
                            {
                                this.ActiveDebuffs.Remove(debuff);
                            }
                            else
                            {
                                this.ActiveDebuffs[debuff]--;
                            }
                        }
                        break;
                    }
                case Constants.Effect.ReduceSkillCooldownBy1:
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
                case Constants.Effect.ExtraTurn:
                    {
                        this.TurnMeter = double.MaxValue;
                        break;
                    }
                default:
                    break;
            }
        }

        public Dictionary<Constants.SkillId, int> GetSkillToCooldownMap()
        {
            Dictionary<Constants.SkillId, int> cooldowns = new Dictionary<Constants.SkillId, int>();
            foreach (SkillInBattle sib in this.skillsToUse)
            {
                cooldowns[sib.Skill.Id] = sib.CooldownsRemaining;
            }

            return cooldowns;
        }

        public IEnumerable<Skill> GetPassiveSkills()
        {
            return this.Champ.Skills.Where(s => s.IsPassive);
        }

        public IEnumerable<Skill> AllAvailableSkills()
        {
            if (this.ActiveDebuffs.ContainsKey(Constants.Debuff.Stun))
            {
                yield return Skill.StunRecovery;
                yield break;
            }

            if (this.TurnCount < this.startupSkillOrder.Count)
            {
                SkillInBattle skillToUse = this.skillsToUse.First(s => s.Skill.Id == this.startupSkillOrder[this.TurnCount]);
                if (skillToUse.CooldownsRemaining > 0)
                {
                    throw new Exception(string.Format("Startup skills for champion {0} on turn {1} tried to use skill {2} but it is still on cooldown!", this.Champ.Name, this.TurnCount, this.startupSkillOrder[this.TurnCount]));
                }
                yield return skillToUse.Skill;
                yield break;
            }
            else
            {
                foreach (SkillInBattle sib in this.skillsToUse)
                {
                    if (sib.CooldownsRemaining > 0)
                    {
                        continue;
                    }

                    yield return sib.Skill;
                }
            }
        }

        public Skill NextAISkill()
        {
            if (this.ActiveDebuffs.ContainsKey(Constants.Debuff.Stun))
            {
                return Skill.StunRecovery;
            }

            SkillInBattle skillToUse = null;
            if (this.TurnCount < this.startupSkillOrder.Count)
            {
                skillToUse = this.skillsToUse.First(s => s.Skill.Id == this.startupSkillOrder[this.TurnCount]);
                if (skillToUse.CooldownsRemaining > 0)
                {
                    throw new Exception(string.Format("Startup skills for champion {0} on turn {1} tried to use skill {2} but it is still on cooldown!", this.Champ.Name, this.TurnCount, this.startupSkillOrder[this.TurnCount]));
                }
            }

            if (skillToUse == null)
            {
                foreach (SkillInBattle sib in this.skillsToUse)
                {
                    if (sib.CooldownsRemaining > 0)
                    {
                        continue;
                    }

                    skillToUse = sib;
                    break;
                }
            }

            return skillToUse.Skill;
        }

        public Skill GetA1()
        {
            return this.skillsToUse.Where(s => s.Skill.Id == Constants.SkillId.A1).First().Skill;
        }

        public void AdditionalAttack()
        {
            this.TakeTurn(this.GetA1(), true);
        }

        public void TakeTurn(Skill skill)
        {
            this.TakeTurn(skill, false);
        }

        private void TakeTurn(Skill skill, bool isAdditionalAttack)
        {
            if (skill.Id != Constants.SkillId.RE)
            {
                SkillInBattle skillToUse = this.skillsToUse.FirstOrDefault(sib => sib.Skill == skill);
                if (skillToUse == null || skillToUse.CooldownsRemaining != 0)
                {
                    throw new ArgumentException(string.Format("Skill {0} ({1}) is not available!", skill.Id, skill.Name));
                }

                skillToUse.CooldownsRemaining = skillToUse.Skill.Cooldown;
            }

            if (!isAdditionalAttack)
            {
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

                this.TurnMeter = 0;
            }

            this.TurnCount++;
        }

        public double TurnMeterIncreaseOnClockTick 
        {
            get
            {
                double buffDebuffSpeedDelta = 0.0d;
                if (this.ActiveBuffs.ContainsKey(Constants.Buff.IncreaseSpeed30))
                {
                    buffDebuffSpeedDelta += 0.3d;
                }
                else if (this.ActiveBuffs.ContainsKey(Constants.Buff.IncreaseSpeed15))
                {
                    buffDebuffSpeedDelta += 0.15d;
                }

                if (this.ActiveDebuffs.ContainsKey(Constants.Debuff.DecreaseSpeed30))
                {
                    buffDebuffSpeedDelta -= 0.3d;
                }
                else if (this.ActiveDebuffs.ContainsKey(Constants.Debuff.DecreaseSpeed15))
                {
                    buffDebuffSpeedDelta -= 0.15;
                }

                return Constants.TurnMeter.DeltaPerTurn(this.Champ.EffectiveSpeed + (this.Champ.BaseSpeed * buffDebuffSpeedDelta));
            }
        }

        public void ClockTick()
        {
            if (this.TurnMeter != double.MaxValue)
            {
                this.TurnMeter += this.TurnMeterIncreaseOnClockTick;
            }
        }
    }
}
