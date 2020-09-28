using RaidLib.DataModel;

namespace RaidLib.Simulator
{
    public class SkillInBattle
    {
        public Skill Skill { get; private set; }
        public SkillPolicy SkillPolicy { get; private set; }
        public int CooldownsRemaining { get; set; }

        public SkillInBattle(Skill skill, SkillPolicy policy)
        {
            this.Skill = skill;
            this.SkillPolicy = policy;
            this.CooldownsRemaining = 0;
        }
    }
}
