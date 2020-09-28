using RaidLib.DataModel;
using RaidLib.Simulator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ClanBossTurns
{
    class Program
    {
        static Tuple<Champion, List<SkillPolicy>> CreateManeater()
        {
            List<Skill> skills = new List<Skill>();
            skills.Add(new Skill("Pummel", Constants.SkillId.A1, 0, Constants.Effect.None, 0));
            skills.Add(new Skill("Syphon", Constants.SkillId.A2, 3, Constants.Effect.None, 0));
            skills.Add(new Skill("Ancient Blood", Constants.SkillId.A3, 5, Constants.Effect.None, 0));

            List<SkillPolicy> policies = new List<SkillPolicy>()
            {
                new SkillPolicy(Constants.SkillId.A3, 0),
                new SkillPolicy(Constants.SkillId.A2, 0),
                new SkillPolicy(Constants.SkillId.A1, 0),
            };

            return new Tuple<Champion, List<SkillPolicy>>(new Champion("Maneater", 98, 227, 20310, 1112, 2, skills), policies);
        }

        static Tuple<Champion, List<SkillPolicy>> CreatePainkeeper()
        {
            List<Skill> skills = new List<Skill>();
            skills.Add(new Skill("Unflagging Advance", Constants.SkillId.A1, 0, Constants.Effect.FillSelfTurnMeterBy10Percent, 0));
            skills.Add(new Skill("Spectacular Sweep", Constants.SkillId.A2, 4, Constants.Effect.None, 0));
            skills.Add(new Skill("Combat Tactics", Constants.SkillId.A3, 4, Constants.Effect.AllyReduceCooldownBy1, 0));

            List<SkillPolicy> policies = new List<SkillPolicy>()
            {
                new SkillPolicy(Constants.SkillId.A3, 0),
                new SkillPolicy(Constants.SkillId.A2, 3),
                new SkillPolicy(Constants.SkillId.A1, 0),
            };

            return new Tuple<Champion, List<SkillPolicy>>(new Champion("Painkeeper", 102, 213, 19320, 771, 2, skills), policies);
        }

        static Tuple<Champion, List<SkillPolicy>> CreateFrozenBanshee()
        {
            List<Skill> skills = new List<Skill>();
            skills.Add(new Skill("Death's Caress", Constants.SkillId.A1, 0, Constants.Effect.None, 0));
            skills.Add(new Skill("Cruel Exultation", Constants.SkillId.A2, 3, Constants.Effect.None, 0));
            skills.Add(new Skill("Frost Blight", Constants.SkillId.A3, 3, Constants.Effect.None, 0));

            List<SkillPolicy> policies = new List<SkillPolicy>()
            {
                new SkillPolicy(Constants.SkillId.A3, 0),
                new SkillPolicy(Constants.SkillId.A1, 0),
            };

            return new Tuple<Champion, List<SkillPolicy>>(new Champion("Frozen Banshee", 99, 169, 13440, 916, 0, skills), policies);
        }

        static Tuple<Champion, List<SkillPolicy>> CreateGravechillKiller()
        {
            List<Skill> skills = new List<Skill>();
            skills.Add(new Skill("Freezing Toxin", Constants.SkillId.A1, 0, Constants.Effect.None, 0));
            skills.Add(new Skill("Blood Chill", Constants.SkillId.A2, 3, Constants.Effect.None, 0));
            skills.Add(new Skill("Icy Veins", Constants.SkillId.A3, 3, Constants.Effect.None, 0));

            List<SkillPolicy> policies = new List<SkillPolicy>()
            {
                new SkillPolicy(Constants.SkillId.A3, 0),
                new SkillPolicy(Constants.SkillId.A2, 0),
                new SkillPolicy(Constants.SkillId.A1, 0),
            };

            return new Tuple<Champion, List<SkillPolicy>>(new Champion("Gravechill Killer", 96, 168, 12375, 815, 0, skills), policies);
        }

        static Tuple<Champion, List<SkillPolicy>> CreateBulwark()
        {
            List<Skill> skills = new List<Skill>();
            skills.Add(new Skill("Hefty Flail", Constants.SkillId.A1, 0, Constants.Effect.None, 0));
            skills.Add(new Skill("Meteoric Ignition", Constants.SkillId.A2, 3, Constants.Effect.None, 0));
            skills.Add(new Skill("Punishing Defense", Constants.SkillId.P1, 0, Constants.Effect.None, 0));

            List<SkillPolicy> policies = new List<SkillPolicy>()
            {
                new SkillPolicy(Constants.SkillId.A2, 0),
                new SkillPolicy(Constants.SkillId.A1, 0),
            };

            return new Tuple<Champion, List<SkillPolicy>>(new Champion("Bulwark", 97, 106, 10125, 924, 0, skills), policies);
        }

        private delegate Tuple<Champion, List<SkillPolicy>> CreateChampion();

        static void Main(string[] args)
        {
            List<CreateChampion> championCreators = new List<CreateChampion>()
            {
                CreateManeater,
                CreatePainkeeper,
                CreateFrozenBanshee,
                CreateGravechillKiller,
                CreateBulwark
            };

            List<Champion> champions = new List<Champion>();
            Dictionary<Champion, List<SkillPolicy>> skillPoliciesByChampion = new Dictionary<Champion, List<SkillPolicy>>();
            foreach (CreateChampion cc in championCreators)
            {
                Tuple<Champion, List<SkillPolicy>> tuple = cc();
                champions.Add(tuple.Item1);
                skillPoliciesByChampion[tuple.Item1] = tuple.Item2;
            }

            ClanBossBattle battle = new ClanBossBattle(ClanBoss.Level.Nightmare, champions, skillPoliciesByChampion);
            battle.Run();
            Console.ReadLine();
        }
    }
}
