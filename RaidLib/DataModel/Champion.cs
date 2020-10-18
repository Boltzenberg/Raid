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
        public Champion(string name, int baseSpeed, int uiSpeed, int speedSets, List<Skill> skills)
        {
            this.Name = name;
            this.BaseSpeed = baseSpeed;
            this.UISpeed = uiSpeed;
            this.SpeedSets = speedSets;
            this.Skills = skills;

            double setSpeedBoost = baseSpeed * speedSets * Constants.SetBonus.Speed;
            int artifactSpeed = uiSpeed - baseSpeed - (int)Math.Round(setSpeedBoost);
            this.EffectiveSpeed = baseSpeed + artifactSpeed + setSpeedBoost;
        }

        public Champion Clone(int uiSpeedDelta, int speedSets)
        {
            if (uiSpeedDelta == 0 && speedSets == this.SpeedSets)
            {
                return this;
            }

            return new Champion(this.Name, this.BaseSpeed, this.UISpeed + uiSpeedDelta, speedSets, this.Skills);
        }

        public string Name { get; private set; }

        public int BaseSpeed { get; private set; }

        public int UISpeed { get; private set; }
        public int SpeedSets { get; private set; }

        public double EffectiveSpeed { get; }

        public List<Skill> Skills { get; private set; }
    }
}
