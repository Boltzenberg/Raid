using RaidLib.DataModel;

namespace RaidLib.Simulator
{
    public class SkillInBattle
    {
        public Skill Skill { get; private set; }
        public int CooldownsRemaining { get; set; }

        public SkillInBattle(Skill skill)
        {
            this.Skill = skill;
            this.CooldownsRemaining = 0;
        }

        public SkillInBattle(SkillInBattle other)
        {
            this.Skill = other.Skill;
            this.CooldownsRemaining = other.CooldownsRemaining;
        }
    }
}
