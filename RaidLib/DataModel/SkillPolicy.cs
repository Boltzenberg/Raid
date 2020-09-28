namespace RaidLib.DataModel
{
    public class SkillPolicy
    {
        public Constants.SkillId SkillId { get; private set; }
        public int DelayBeforeFirstUse { get; private set; }

        public SkillPolicy(Constants.SkillId skillId, int delayBeforeFirstUse)
        {
            this.SkillId = skillId;
            this.DelayBeforeFirstUse = delayBeforeFirstUse;
        }
    }
}
