using RaidLib.DataModel;
using RaidLib.Simulator;
using RaidLib.Utilities;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.IO;
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
            skills.Add(new Skill("Death's Caress", Constants.SkillId.A1, 0, TurnAction.AttackOneEnemy()));
            skills.Add(new Skill("Cruel Exultation", Constants.SkillId.A2, 3, TurnAction.AttackOneEnemy()));
            skills.Add(new Skill("Frost Blight", Constants.SkillId.A3, 3, TurnAction.AttackOneEnemy()));

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
            skills.Add(new Skill("Freezing Toxin", Constants.SkillId.A1, 0, TurnAction.AttackOneEnemy()));
            skills.Add(new Skill("Blood Chill", Constants.SkillId.A2, 3, TurnAction.AttackOneEnemy()));
            skills.Add(new Skill("Icy Veins", Constants.SkillId.A3, 3, TurnAction.AttackOneEnemy()));

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
            skills.Add(new Skill("Hefty Flail", Constants.SkillId.A1, 0, TurnAction.AttackOneEnemy()));
            skills.Add(new Skill("Meteoric Ignition", Constants.SkillId.A2, 3, TurnAction.AttackOneEnemy()));
            skills.Add(new Skill("Punishing Defense", Constants.SkillId.P1, 0, TurnAction.AttackOneEnemy()));

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
            List<ClanBossBattleResult> results = battle.Run();
            Console.WriteLine();
            Console.WriteLine("Run is over!");
            using (StreamWriter csv = new StreamWriter(File.OpenWrite("results.csv")))
            {
                csv.WriteLine("ClockTick,Clan Boss Turn,Actor,SkillId,SkillName,{0} Turn Meter,{1} Turn Meter,{2} Turn Meter,{3} Turn Meter,{4} Turn Meter,Clan Boss Turn Meter", champions[0].Name, champions[1].Name, champions[2].Name, champions[3].Name, champions[4].Name);
                foreach (ClanBossBattleResult result in results)
                {
                    csv.WriteLine("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10}",
                        result.ClockTick,
                        result.ClanBossTurn,
                        result.ActorName,
                        result.Skill != Constants.SkillId.None ? result.Skill.ToString() : string.Empty,
                        result.SkillName,
                        result.Champions.Where(c => c.Champion.Name == champions[0].Name).First().TurnMeter,
                        result.Champions.Where(c => c.Champion.Name == champions[1].Name).First().TurnMeter,
                        result.Champions.Where(c => c.Champion.Name == champions[2].Name).First().TurnMeter,
                        result.Champions.Where(c => c.Champion.Name == champions[3].Name).First().TurnMeter,
                        result.Champions.Where(c => c.Champion.Name == champions[4].Name).First().TurnMeter,
                        result.ClanBoss.TurnMeter);
                }
            }

            int lastKillableTurn = ClanBossBattleResultsAnalysis.LastClanBossTurnThatHitKillableChampion(results, Utils.FindSlowestChampion(champions));
            Console.WriteLine("Last turn where there was a hit on a champion that wasn't unkillable:  {0}", lastKillableTurn);
            Console.ReadLine();
        }
    }
}
