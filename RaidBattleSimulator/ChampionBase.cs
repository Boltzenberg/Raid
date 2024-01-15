using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaidBattleSimulator
{
    public class ChampionBase
    {
        public string Name { get; private set; }

        public double Speed { get; private set; }

        public double BaseTickRate { get; private set; }

        public ChampionBase(string name, int baseSpeed, int uiSpeed, int speedSets, int perceptionSets, double speedAuraPercentage)
        {
            this.Name = name;

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
        /// <param name="turnMeterBoost">If a turn meter boost is applied, this is the value as a decimal, so 20% is .2</param>
        /// <returns></returns>
        public double GetTurnMeterDeltaForTick(double speedBuff, double turnMeterBoost)
        {
            return (this.BaseTickRate * speedBuff) + turnMeterBoost;
        }

        public override string ToString()
        {
            return this.Name;
        }
    }
}
