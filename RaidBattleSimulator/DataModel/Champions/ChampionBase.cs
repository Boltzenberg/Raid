using System;
using System.Collections.Generic;
using System.Linq;

namespace RaidBattleSimulator.DataModel.Champions
{
    public class ChampionBase
    {
        public string Name { get; private set; }

        public double Speed { get; private set; }

        public double BaseTickRate { get; private set; }

        public Dictionary<Constants.SkillId, Skill> Skills { get; private set; }

        private Dictionary<Constants.SkillId, int> skillCooldowns = new Dictionary<Constants.SkillId, int>();

        private Dictionary<Constants.Buff, int> activeBuffs = new Dictionary<Constants.Buff, int>();

        private Dictionary<Constants.Debuff, int> activeDebuffs = new Dictionary<Constants.Debuff, int>();

        private int turnCount = 0;

        public ChampionBase(string name, int baseSpeed, int uiSpeed, int speedSets, int perceptionSets, double speedAuraPercentage, List<Skill> skills)
        {
            this.Name = name;
            this.Skills = new Dictionary<Constants.SkillId, Skill>();
            foreach (Skill skill in skills)
            {
                this.Skills[skill.Id] = skill;
                this.skillCooldowns[skill.Id] = 0;
            }

            // Speed Aura only applies to base speed
            double auraSpeedBoost = baseSpeed * speedAuraPercentage; 

            // To get the true speed from the UI speed and sets, calculate the speed boost from the sets which applies only to the base speed
            double setSpeedBoost = (baseSpeed * speedSets * Constants.SetBonus.Speed) + (baseSpeed * perceptionSets * Constants.SetBonus.Perception);

            // The speed boost from the artifacts comes from the UI reported speed once we take out the base speed and the set speed boost (rounded)
            int artifactSpeed = uiSpeed - baseSpeed - (int)Math.Round(setSpeedBoost);

            // The effective speed of this champ is the base speed plus artifact speed plus set speed boost plus aura speed boost
            this.Speed = baseSpeed + artifactSpeed + setSpeedBoost + auraSpeedBoost;

            // Ticks come from speed divided by the magic number
            this.BaseTickRate = Constants.TurnMeter.DeltaPerTick(this.Speed);
        }

        /// <summary>
        /// Gets the turn meter delta per tick
        /// </summary>
        /// <param name="speedBuff">If a speed buff or debuff is present, this is the delta.  Base is 1.0, so 30% speed buff is 1.3</param>
        /// <returns></returns>
        public double GetTurnMeterDeltaForTick(double speedBuff)
        {
            double result = (this.BaseTickRate * speedBuff);

            return result;
        }

        public IEnumerable<Constants.Buff> GetActiveBuffs
        {
            get
            {
                foreach (Constants.Buff buff in this.activeBuffs.Keys)
                {
                    yield return buff;
                }
            }
        }

        public IEnumerable<Constants.Debuff> GetActiveDebuffs
        {
            get
            {
                foreach (Constants.Debuff debuff in this.activeDebuffs.Keys)
                {
                    yield return debuff;
                }
            }
        }

        protected void ReduceSkillCooldowns(int reduction)
        {
            foreach (Constants.SkillId skillId in this.Skills.Keys)
            {
                this.skillCooldowns[skillId] = Math.Max(this.skillCooldowns[skillId] - reduction, 0);
            }
        }

        protected void AddBuff(BuffToApply buff)
        {
            // Extend existing buff
            if (this.activeBuffs.ContainsKey(buff.Buff) && Constants.BuffTraits.IsSingleton(buff.Buff))
            {
                if (this.activeBuffs[buff.Buff] < buff.Duration)
                {
                    this.activeBuffs[buff.Buff] = buff.Duration;
                }
            }
            // Apply a new buff
            else if (this.activeBuffs.Count < Constants.BuffTraits.MaxBuffs)
            {
                this.activeBuffs[buff.Buff] = buff.Duration;
            }
        }

        protected void AddDebuff(DebuffToApply debuff)
        {
            if (this.activeBuffs.ContainsKey(Constants.Buff.BlockDebuffs))
            {
                // debuff blocked!
                return;
            }

            // extend existing debuff
            if (this.activeDebuffs.ContainsKey(debuff.Debuff) && Constants.DebuffTraits.IsSingleton(debuff.Debuff))
            {
                if (this.activeDebuffs[debuff.Debuff] < debuff.Duration)
                {
                    this.activeDebuffs[debuff.Debuff] = debuff.Duration;
                }
            }
            // Apply a new debuff
            else if (this.activeDebuffs.Count < Constants.DebuffTraits.MaxDebuffs)
            {
                this.activeDebuffs[debuff.Debuff] = debuff.Duration;
            }
        }

        protected virtual Skill GetNextSkillToUse()
        {
            // If the champ is stunned, just recover
            if (this.activeDebuffs.ContainsKey(Constants.Debuff.Stun))
            {
                return Skill.StunRecovery;
            }

            List<Constants.SkillId> skillIdsToCheck = new List<Constants.SkillId>() { Constants.SkillId.A4, Constants.SkillId.A3, Constants.SkillId.A2, Constants.SkillId.A1 };

            foreach (Constants.SkillId skillId in skillIdsToCheck)
            {
                if (this.skillCooldowns.ContainsKey(skillId))
                {
                    if (this.skillCooldowns[skillId] == 0 && turnCount >= this.Skills[skillId].InitialDelay)
                    {
                        return this.Skills[skillId];
                    }
                }
            }

            return this.Skills[Constants.SkillId.A1];
        }

        private List<ChampionBase> GetImpactedChampions(Constants.Target target, Battle battle)
        {
            List<ChampionBase> championsImpacted = new List<ChampionBase>();
            switch (target)
            {
                case Constants.Target.Self:
                    championsImpacted.Add(this);
                    break;
                case Constants.Target.AllAllies:
                    championsImpacted.AddRange(battle.Team.Where(c => c != this));
                    break;
                case Constants.Target.FullTeam:
                    championsImpacted.AddRange(battle.Team);
                    break;
                case Constants.Target.OneEnemy:
                    if (battle.Team.Contains(this))
                    {
                        championsImpacted.Add(battle.Enemies.First());
                    }
                    else
                    {
                        championsImpacted.Add(battle.Team.First());
                    }
                    break;
                default:
                    throw new NotSupportedException("Didn't implement that yet");
            }
            return championsImpacted;
        }

        private void ApplyEffect(EffectToApply effect, Battle battle)
        {
            List<ChampionBase> championsImpacted = this.GetImpactedChampions(effect.Target, battle);

            switch (effect.Effect)
            {
                case Constants.Effect.FillTurnMeterBy10Percent:
                    foreach (ChampionBase champ in championsImpacted)
                    {
                        battle.UpdateTurnMeter(champ, 0.1d);
                    }
                    break;
                case Constants.Effect.FillTurnMeterBy30Percent:
                    foreach (ChampionBase champ in championsImpacted)
                    {
                        battle.UpdateTurnMeter(champ, 0.3d);
                    }
                    break;
                case Constants.Effect.ReduceSkillCooldownBy1:
                    foreach (ChampionBase champ in championsImpacted)
                    {
                        champ.ReduceSkillCooldowns(1);
                    }
                    break;
                case Constants.Effect.ExtraTurn:
                    break; // handled elsewhere
                default:
                    throw new NotSupportedException("Didn't implement that yet");
            }
        }

        private void ApplyBuff(BuffToApply buff, Battle battle)
        {
            List<ChampionBase> championsImpacted = this.GetImpactedChampions(buff.Target, battle);

            foreach (ChampionBase champ in championsImpacted)
            {
                champ.AddBuff(buff);
            }
        }

        private void ApplyDebuff(DebuffToApply debuff, Battle battle)
        {
            List<ChampionBase> championsImpacted = this.GetImpactedChampions(debuff.Target, battle);

            foreach (ChampionBase champ in championsImpacted)
            {
                champ.AddDebuff(debuff);
            }
        }

        public TurnResult TakeTurn(Battle battle)
        {
            bool takeExtraTurn = false;
            
            // Get the skill to use
            Skill skillToUse = this.GetNextSkillToUse();

            // Update skill cooldowns
            if (skillToUse.AllowSkillCooldowns)
            {
                foreach (Constants.SkillId skillToSetCooldownOn in this.Skills.Keys)
                {
                    if (skillToSetCooldownOn == skillToUse.Id)
                    {
                        this.skillCooldowns[skillToUse.Id] = skillToUse.Cooldown - 1;
                    }
                    else
                    {
                        this.skillCooldowns[skillToSetCooldownOn] = Math.Max(0, this.skillCooldowns[skillToSetCooldownOn] - 1);
                    }
                }
            }

            this.turnCount++;

            // Apply the turn action for the skill being used
            TurnAction action = skillToUse.TurnAction;
            if (action.EffectsToApply != null)
            {
                foreach (EffectToApply effect in action.EffectsToApply)
                {
                    this.ApplyEffect(effect, battle);
                    if (effect.Effect == Constants.Effect.ExtraTurn)
                    {
                        takeExtraTurn = true;
                    }
                }
            }

            // Update the durations of all buffs and debuffs
            foreach (Constants.Buff buff in new List<Constants.Buff>(this.activeBuffs.Keys))
            {
                this.activeBuffs[buff]--;
                if (this.activeBuffs[buff] == 0)
                {
                    this.activeBuffs.Remove(buff);
                }
            }

            foreach (Constants.Debuff debuff in new List<Constants.Debuff>(this.activeDebuffs.Keys))
            {
                this.activeDebuffs[debuff]--;
                if (this.activeDebuffs[debuff] == 0)
                {
                    this.activeDebuffs.Remove(debuff);
                }
            }

            if (action.BuffsToApply != null)
            {
                foreach (BuffToApply buff in action.BuffsToApply)
                {
                    this.ApplyBuff(buff, battle);
                }
            }

            if (action.DebuffsToApply != null)
            {
                foreach (DebuffToApply debuff in action.DebuffsToApply)
                {
                    this.ApplyDebuff(debuff, battle);
                }
            }

            return new TurnResult(skillToUse.Id, battle.GetCurrentTurnMeters(), takeExtraTurn);
        }

        public override string ToString()
        {
            return this.Name;
        }
    }
}
