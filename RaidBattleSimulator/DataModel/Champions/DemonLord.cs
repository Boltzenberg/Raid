using System.Collections.Generic;

namespace RaidBattleSimulator.DataModel.Champions
{
    public class DemonLord : ChampionBase
    {
        public static ChampionBase CreateUltraNightmare(bool greenAffinity)
        {
            return CreateDemonLord(190, greenAffinity);
        }

        public static ChampionBase CreateNightmare(bool greenAffinity)
        {
            return CreateDemonLord(170, greenAffinity);
        }

        public static ChampionBase CreateBrutal(bool greenAffinity)
        {
            return CreateDemonLord(160, greenAffinity);
        }

        private static ChampionBase CreateDemonLord(int speed, bool greenAffinity)
        {
            List<DebuffToApply> a2Debuffs = new List<DebuffToApply>();
            if (greenAffinity)
            {
                a2Debuffs.Add(new DebuffToApply(Constants.Debuff.DecreaseSpeed15, 2, Constants.Target.AllEnemies));
            }

            List<Skill> skills = new List<Skill>()
            {
                new Skill("Crushing Force", Constants.SkillId.A1, 0, TurnAction.AttackAllEnemies()),
                new Skill("Flesh Wither", Constants.SkillId.A2, 3, new TurnAction(1, Constants.Target.AllEnemies, null, null, a2Debuffs)),
                new Skill("Dark Nova", Constants.SkillId.A3, 3, new TurnAction(1, Constants.Target.OneEnemy, null, null, new List<DebuffToApply>() { new DebuffToApply(Constants.Debuff.Stun, 1, Constants.Target.OneEnemy) }))
            };

            return new DemonLord(speed, greenAffinity, skills);
        }

        private static readonly Constants.SkillId[] SkillsInOrder = { Constants.SkillId.A1, Constants.SkillId.A2, Constants.SkillId.A3 };
        private int lastTurn = 0;
        private DemonLord(int speed, bool greenAffinity, List<Skill> skills) : base("Demon Lord", speed, speed, 0, 0, 0.0d, skills)
        { }

        protected override Skill GetNextSkillToUse()
        {
            return this.Skills[SkillsInOrder[lastTurn++ % this.Skills.Count]];
        }
    }
}
