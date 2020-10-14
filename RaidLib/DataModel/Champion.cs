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

            float setSpeedBoost = baseSpeed * speedSets * Constants.SetBonus.Speed;
            int artifactSpeed = uiSpeed - baseSpeed - (int)Math.Round(setSpeedBoost);
            this.EffectiveSpeed = baseSpeed + artifactSpeed + setSpeedBoost;
        }

        public string Name { get; private set; }

        public int BaseSpeed { get; private set; }

        public int UISpeed { get; private set; }
        public int SpeedSets { get; private set; }

        public float EffectiveSpeed { get; }

        public List<Skill> Skills { get; private set; }

        public static Champion CreateManeater(int uiSpeed, int speedSets)
        {
            List<Skill> skills = new List<Skill>();
            skills.Add(new Skill("Pummel", Constants.SkillId.A1, 0, TurnAction.AttackOneEnemy()));
            skills.Add(new Skill("Syphon", Constants.SkillId.A2, 3, TurnAction.AttackOneEnemy()));
            skills.Add(new Skill(
                "Ancient Blood",
                Constants.SkillId.A3,
                5,
                new TurnAction(
                    0,
                    Constants.Target.None,
                    null,
                    new List<BuffToApply>() {
                        new BuffToApply(Constants.Buff.BlockDebuffs, 2, Constants.Target.FullTeam),
                        new BuffToApply(Constants.Buff.Unkillable, 2, Constants.Target.FullTeam)
                    },
                null)));

            return new Champion("Maneater", 98, uiSpeed, speedSets, skills);
        }

        public static Champion CreatePainkeeper(int uiSpeed, int speedSets)
        {
            List<Skill> skills = new List<Skill>();
            skills.Add(new Skill(
                "Unflagging Advance",
                Constants.SkillId.A1,
                0,
                new TurnAction(1, Constants.Target.OneEnemy, new List<EffectToApply>() { new EffectToApply(Constants.Effect.FillTurnMeterBy10Percent, Constants.Target.Self) }, null, null)));
            skills.Add(new Skill("Spectacular Sweep", Constants.SkillId.A2, 4, TurnAction.AttackOneEnemy()));
            skills.Add(new Skill(
                "Combat Tactics",
                Constants.SkillId.A3,
                4,
                new TurnAction(0, Constants.Target.None, new List<EffectToApply>() { new EffectToApply(Constants.Effect.ReduceCooldownBy1, Constants.Target.AllAllies) }, null, null)));

            return new Champion("Painkeeper", 102, uiSpeed, speedSets, skills);
        }

        public static Champion CreateFrozenBanshee(int uiSpeed, int speedSets)
        {
            List<Skill> skills = new List<Skill>();
            skills.Add(new Skill("Death's Caress", Constants.SkillId.A1, 0, TurnAction.AttackOneEnemy()));
            skills.Add(new Skill("Cruel Exultation", Constants.SkillId.A2, 3, TurnAction.AttackOneEnemy()));
            skills.Add(new Skill("Frost Blight", Constants.SkillId.A3, 3, TurnAction.AttackOneEnemy()));

            return new Champion("Frozen Banshee", 99, uiSpeed, speedSets, skills);
        }

        public static Champion CreateGravechillKiller(int uiSpeed, int speedSets)
        {
            List<Skill> skills = new List<Skill>();
            skills.Add(new Skill("Freezing Toxin", Constants.SkillId.A1, 0, TurnAction.AttackOneEnemy()));
            skills.Add(new Skill("Blood Chill", Constants.SkillId.A2, 3, TurnAction.AttackOneEnemy()));
            skills.Add(new Skill("Icy Veins", Constants.SkillId.A3, 3, TurnAction.AttackOneEnemy()));

            return new Champion("Gravechill Killer", 96, uiSpeed, speedSets, skills);
        }

        public static Champion CreateBulwark(int uiSpeed, int speedSets)
        {
            List<Skill> skills = new List<Skill>();
            skills.Add(new Skill("Hefty Flail", Constants.SkillId.A1, 0, TurnAction.AttackOneEnemy()));
            skills.Add(new Skill("Meteoric Ignition", Constants.SkillId.A2, 3, TurnAction.AttackOneEnemy()));
            skills.Add(new Skill("Punishing Defense", Constants.SkillId.P1, 0, TurnAction.AttackOneEnemy()));

            return new Champion("Bulwark", 97, uiSpeed, speedSets, skills);
        }

        public static Champion CreateOccultBrawler(int uiSpeed, int speedSets)
        {
            List<Skill> skills = new List<Skill>();
            skills.Add(new Skill("Sorcerous Razor", Constants.SkillId.A1, 0, TurnAction.AttackOneEnemy()));
            skills.Add(new Skill("Curse Eater", Constants.SkillId.A2, 3, TurnAction.AttackOneEnemy()));
            skills.Add(new Skill("Ruination Ritual", Constants.SkillId.P1, 0, TurnAction.AttackOneEnemy()));

            return new Champion("Occult Brawler", 98, uiSpeed, speedSets, skills);
        }

        public static Champion CreateAothar(int uiSpeed, int speedSets)
        {
            List<Skill> skills = new List<Skill>();
            skills.Add(new Skill("Rage", Constants.SkillId.A1, 0, new TurnAction(2, Constants.Target.OneEnemy, null, null, null)));
            skills.Add(new Skill("Holy Flame", Constants.SkillId.A2, 3, new TurnAction(4, Constants.Target.OneEnemy, null, null, null)));
            skills.Add(new Skill("Brand", Constants.SkillId.A3, 3, TurnAction.AttackOneEnemy()));

            return new Champion("Aothar", 92, uiSpeed, speedSets, skills);
        }

        public static Champion CreateRhazinScarhide(int uiSpeed, int speedSets)
        {
            List<Skill> skills = new List<Skill>();
            skills.Add(new Skill("Bone Sword", Constants.SkillId.A1, 0, new TurnAction(3, Constants.Target.OneEnemy, null, null, null)));
            skills.Add(new Skill("Shear", Constants.SkillId.A2, 3, TurnAction.AttackOneEnemy()));
            skills.Add(new Skill("Bog Down", Constants.SkillId.A3, 6, TurnAction.AttackAllEnemies()));

            return new Champion("Rhazin Scarhide", 91, uiSpeed, speedSets, skills);
        }

        public static Champion CreateSeptimus(int uiSpeed, int speedSets)
        {
            List<Skill> skills = new List<Skill>();
            skills.Add(new Skill("Behead", Constants.SkillId.A1, 0, TurnAction.AttackOneEnemy()));
            skills.Add(new Skill("Holy Sword", Constants.SkillId.A2, 4, TurnAction.AttackOneEnemy()));
            skills.Add(new Skill("Giant Killer", Constants.SkillId.P1, 0, TurnAction.AttackOneEnemy()));

            return new Champion("Septimus", 102, uiSpeed, speedSets, skills);
        }
    }
}
