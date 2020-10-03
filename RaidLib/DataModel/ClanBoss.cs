using System.Collections.Generic;

namespace RaidLib.DataModel
{
    public class ClanBoss
    {
        public enum Level
        {
            Easy,
            Normal,
            Hard,
            Brutal,
            Nightmare,
            UltraNightmare
        }

        public static ClanBoss Get(Level level)
        {
            switch (level)
            {
                case Level.Easy: return new ClanBoss(90);
                case Level.Normal: return new ClanBoss(120);
                case Level.Hard: return new ClanBoss(140);
                case Level.Brutal: return new ClanBoss(160);
                case Level.Nightmare: return new ClanBoss(170);
                case Level.UltraNightmare: return new ClanBoss(190);
            }

            return null;
        }

        private ClanBoss(int speed)
        {
            this.Speed = speed;
            this.Skills = new List<Skill>()
            {
                new Skill("Crushing Force", Constants.SkillId.A1, 0, TurnAction.AttackAllEnemies()),
                new Skill("Flesh Wither", Constants.SkillId.A2, 3, TurnAction.AttackAllEnemies()),
                new Skill("Dark Nova", Constants.SkillId.A3, 3, new TurnAction(1, Constants.Target.OneEnemy, null, null, new List<DebuffToApply>() { new DebuffToApply(Constants.Debuff.Stun, 1, Constants.Target.OneEnemy) }))
            };
        }

        public int Speed { get; private set; }

        public List<Skill> Skills { get; private set; }
    }
}
