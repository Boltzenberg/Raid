using RaidLib.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RaidLib.Simulator
{
    public class ChampionInBattle
    {
        private Dictionary<Constants.Buff, int> activeBuffs;
        private Dictionary<Constants.Debuff, int> activeDebuffs;
        private List<SkillInBattle> skillsToUse;
        private int turnCount;

        public ChampionInBattle(Champion champion, List<SkillPolicy> skillPolicies, Clock clock)
        {
            this.Champ = champion;
            this.TurnMeter = 0;
            this.turnCount = 0;
            this.activeBuffs = new Dictionary<Constants.Buff, int>();
            this.activeDebuffs = new Dictionary<Constants.Debuff, int>();
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

        public void ApplyBuff(Constants.Buff buff, int duration)
        {

        }

        public void ApplyDebuff(Constants.Debuff debuff, int duration)
        {

        }

        public void ApplyEffect(Constants.Effect effect)
        {
            switch (effect)
            {
                case Constants.Effect.FillSelfTurnMeterBy10Percent:
                    {
                        this.TurnMeter += 0.1f;
                        break;
                    }
                case Constants.Effect.AllyReduceCooldownBy1:
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

        public Constants.Effect TakeTurn()
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

            //Console.WriteLine(" {0} uses skill {1} ({2}) with turn meter {3}!", this.Champ.Name, skillToUse.Skill.Id, skillToUse.Skill.Name, this.TurnMeter);
            Console.WriteLine(" {0} Turn {1}: skill {2} ({3})", this.Champ.Name, this.turnCount, skillToUse.Skill.Id, skillToUse.Skill.Name);
            this.TurnMeter = 0;

            return skillToUse.Skill.Effect;
        }

        private void OnClockTick(object sender)
        {
            this.TurnMeter += this.TurnMeterIncreaseOnClockTick;
        }
    }
}
