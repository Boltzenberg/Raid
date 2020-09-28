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
        public Champion(string name, int baseSpeed, int uiSpeed, int baseHP, int baseDefense, int speedSets, List<Skill> skills)
        {
            this.Name = name;
            this.BaseSpeed = baseSpeed;
            this.UISpeed = uiSpeed;
            this.BaseHP = baseHP;
            this.BaseDefense = baseDefense;
            this.SpeedSets = speedSets;
            this.Skills = skills;

            float setSpeedBoost = baseSpeed * speedSets * Constants.SetBonus.Speed;
            int artifactSpeed = uiSpeed - baseSpeed - (int)Math.Round(setSpeedBoost);
            this.EffectiveSpeed = baseSpeed + artifactSpeed + setSpeedBoost;
        }

        public string Name { get; private set; }

        public int BaseSpeed { get; private set; }
        public int BaseHP { get; private set; }
        public int BaseDefense { get; private set; }

        public int UISpeed { get; private set; }
        public int SpeedSets { get; private set; }

        public float EffectiveSpeed { get; }

        public List<Skill> Skills { get; private set; }
    }
}
