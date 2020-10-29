using RaidLib.Simulator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaidLib.DataModel
{
    public class Champion
    {
        public Champion(string name, int baseSpeed, int uiSpeed, int speedSets, int perceptionSets, List<Skill> skills)
        {
            this.Name = name;
            this.BaseSpeed = baseSpeed;
            this.UISpeed = uiSpeed;
            this.SpeedSets = speedSets;
            this.PerceptionSets = perceptionSets;
            this.Skills = skills;

            double setSpeedBoost = (baseSpeed * speedSets * Constants.SetBonus.Speed) + (baseSpeed * perceptionSets * Constants.SetBonus.Perception);
            int artifactSpeed = uiSpeed - baseSpeed - (int)Math.Round(setSpeedBoost);
            this.EffectiveSpeed = baseSpeed + artifactSpeed + setSpeedBoost;
        }

        public Champion Clone(int uiSpeedDelta, int speedSets, int perceptionSets)
        {
            if (uiSpeedDelta == 0 && speedSets == this.SpeedSets && perceptionSets == this.PerceptionSets)
            {
                return this;
            }

            return new Champion(
                this.Name, 
                this.BaseSpeed,
                this.UISpeed + uiSpeedDelta,
                speedSets == -1 ? this.SpeedSets : speedSets,
                perceptionSets == -1 ? this.PerceptionSets : perceptionSets,
                this.Skills);
        }

        public string Name { get; private set; }

        public int BaseSpeed { get; private set; }

        public int UISpeed { get; private set; }
        public int SpeedSets { get; private set; }
        public int PerceptionSets { get; private set; }

        public double EffectiveSpeed { get; }

        public List<Skill> Skills { get; private set; }
    }
}
