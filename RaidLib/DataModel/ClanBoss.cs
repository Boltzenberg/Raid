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

        public static ClanBoss Get(Level level, bool greenAffinity = false)
        {
            switch (level)
            {
                case Level.Easy: return new ClanBoss(90, greenAffinity);
                case Level.Normal: return new ClanBoss(120, greenAffinity);
                case Level.Hard: return new ClanBoss(140, greenAffinity);
                case Level.Brutal: return new ClanBoss(160, greenAffinity);
                case Level.Nightmare: return new ClanBoss(170, greenAffinity);
                case Level.UltraNightmare: return new ClanBoss(190, greenAffinity);
            }

            return null;
        }

        private ClanBoss(int speed, bool greenAffinity)
        {
            this.Speed = speed;
            List<DebuffToApply> a2Debuffs = new List<DebuffToApply>();
            if (greenAffinity)
            {
                a2Debuffs.Add(new DebuffToApply(Constants.Debuff.DecreaseSpeed15, 2, Constants.Target.AllEnemies));
            }

            this.Skills = new List<Skill>()
            {
                new Skill("Crushing Force", Constants.SkillId.A1, 0, TurnAction.AttackAllEnemies()),
                new Skill("Flesh Wither", Constants.SkillId.A2, 3, new TurnAction(1, Constants.Target.AllEnemies, null, null, a2Debuffs)),
                new Skill("Dark Nova", Constants.SkillId.A3, 3, new TurnAction(1, Constants.Target.OneEnemy, null, null, new List<DebuffToApply>() { new DebuffToApply(Constants.Debuff.Stun, 1, Constants.Target.OneEnemy) }))
            };
        }

        public int Speed { get; private set; }

        public List<Skill> Skills { get; private set; }
    }
}
