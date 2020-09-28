using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaidLib.DataModel
{
    public class Skill
    {
        public Skill(string name, Constants.SkillId id, int cooldown, Constants.Effect effect, int effectDuration)
        {
            this.Name = name;
            this.Id = id;
            this.Cooldown = cooldown;
            this.Effect = effect;
            this.EffectDuration = effectDuration;
        }

        public string Name { get; private set; }
        public Constants.SkillId Id { get; private set; }
        public int Cooldown { get; private set; }
        public Constants.Effect Effect { get; private set; }
        public int EffectDuration { get; private set; }
    }
}
