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
        private static void RunUnkillableSearcher(ClanBoss.Level clanBossLevel, List<Teams.CreateChampion> championCreators)
        {
            Dictionary<string, Champion> initialChampions = new Dictionary<string, Champion>();
            Dictionary<Champion, Tuple<List<Constants.SkillId>, List<Constants.SkillId>>> skillPoliciesByChampionBase = new Dictionary<Champion, Tuple<List<Constants.SkillId>, List<Constants.SkillId>>>();
            foreach (Teams.CreateChampion cc in championCreators)
            {
                Tuple<Champion, List<Constants.SkillId>, List<Constants.SkillId>> tuple = cc(clanBossLevel);
                skillPoliciesByChampionBase[tuple.Item1] = new Tuple<List<Constants.SkillId>, List<Constants.SkillId>>(tuple.Item2, tuple.Item3);
                initialChampions[tuple.Item1.Name] = tuple.Item1;
            }

            UnkillableSearcher searcher = new UnkillableSearcher(skillPoliciesByChampionBase);
            Dictionary<int, List<List<Champion>>> unkillableTeamsByChangedChampionCount = new Dictionary<int, List<List<Champion>>>();
            foreach (List<Champion> unkillableTeam in searcher.Search(clanBossLevel))
            {
                int key = 0;
                foreach (Champion c in unkillableTeam)
                {
                    Champion cBase = initialChampions[c.Name];
                    string descriptor;
                    if (cBase.EffectiveSpeed != c.EffectiveSpeed)
                    {
                        key++;
                        descriptor = string.Format("{0} (UPDATED)", c.Name);
                    }
                    else
                    {
                        descriptor = c.Name;
                    }

                    Console.WriteLine("{0}", descriptor);
                    Console.WriteLine("  Base Speed:      {0}", c.BaseSpeed);
                    Console.WriteLine("  UI Speed:        {0}", c.UISpeed);
                    Console.WriteLine("  Speed Sets:      {0}", c.SpeedSets);
                    Console.WriteLine("  Effective Speed: {0}", c.EffectiveSpeed);
                }
                Console.WriteLine();

                List<List<Champion>> teams;
                if (!unkillableTeamsByChangedChampionCount.TryGetValue(key, out teams))
                {
                    teams = new List<List<Champion>>();
                    unkillableTeamsByChangedChampionCount[key] = teams;
                }
                teams.Add(new List<Champion>(unkillableTeam));
            }

            string filename = string.Format("{0}.txt", clanBossLevel);
            using (StreamWriter writer = new StreamWriter(File.OpenWrite(filename)))
            {
                foreach (int key in unkillableTeamsByChangedChampionCount.Keys.OrderBy(k => k))
                {
                    foreach (List<Champion> team in unkillableTeamsByChangedChampionCount[key])
                    {
                        foreach (Champion c in team)
                        {
                            Champion cBase = initialChampions[c.Name];
                            writer.WriteLine("{0}{1}", c.Name, c.EffectiveSpeed == cBase.EffectiveSpeed ? string.Empty : " (UPDATED)");
                            writer.WriteLine("  Base Speed:      {0}", c.BaseSpeed);
                            writer.WriteLine("  UI Speed:        {0}", c.UISpeed);
                            writer.WriteLine("  Speed Sets:      {0}", c.SpeedSets);
                            writer.WriteLine("  Effective Speed: {0}", c.EffectiveSpeed);
                        }
                        writer.WriteLine();
                    }
                    writer.WriteLine("================");
                }
            }

            System.Diagnostics.Process.Start(filename);
        }

        static void FixGungasTeam(ClanBoss.Level clanBossLevel)
        {
            List<Champion> champions = new List<Champion>();
            List<ChampionInBattle> cibs = new List<ChampionInBattle>();
            foreach (Teams.CreateChampion cc in Teams.Gunga.ChampionCreators())
            {
                Tuple<Champion, List<Constants.SkillId>, List<Constants.SkillId>> tuple = cc(clanBossLevel);
                Champion c = tuple.Item1;
                champions.Add(c);
                if (c.Name == "Frozen Banshee")
                {
                    Console.WriteLine("{0}'s effective speed: {0}, Turn Meter Increment: {1}", c.Name, c.EffectiveSpeed, Constants.TurnMeter.DeltaPerTurn(c.EffectiveSpeed));
                    c = c.Clone(-10, 0, 0);
                    Console.WriteLine("{0}'s new effective speed: {0}, Turn Meter Increment: {1}", c.Name, c.EffectiveSpeed, Constants.TurnMeter.DeltaPerTurn(c.EffectiveSpeed));
                }
                else if (c.Name == "Gravechill Killer")
                {
                    Console.WriteLine("{0}'s effective speed: {0}, Turn Meter Increment: {1}", c.Name, c.EffectiveSpeed, Constants.TurnMeter.DeltaPerTurn(c.EffectiveSpeed));
                    c = c.Clone(-9, 0, 0);
                    Console.WriteLine("{0}'s new effective speed: {0}, Turn Meter Increment: {1}", c.Name, c.EffectiveSpeed, Constants.TurnMeter.DeltaPerTurn(c.EffectiveSpeed));
                }
                cibs.Add(new ChampionInBattle(c, tuple.Item2, tuple.Item3));
            }

            ClanBossBattle battle = new ClanBossBattle(clanBossLevel, cibs);
            List<ClanBossBattleResult> results = battle.Run();

            int lastKillableTurn = ClanBossBattleResultsAnalysis.LastClanBossTurnThatHitKillableChampion(results, Utils.FindSlowestChampion(champions));
            if (lastKillableTurn > 4)
            {
                Console.WriteLine("Not unkillable!");
                return;
            }

            ClanBossBattleResultsAnalysis.PrintResults(results, false, true);

            for (int i = 0; i < results.Count; i++)
            {
                if (results[i].AttackDetails.ActorName == "Painkeeper" &&
                    results[i].AttackDetails.Skill == Constants.SkillId.A3)
                {
                    if (results[i-1].AttackDetails.ActorName == "Frozen Banshee" &&
                        results[i-1].AttackDetails.Skill == Constants.SkillId.A1)
                    {
                        if (results[i-2].AttackDetails.ActorName == Constants.Names.ClanBoss &&
                            results[i-2].AttackDetails.Skill == Constants.SkillId.A1)
                        {
                            Console.WriteLine("Got a hit after CB turn {0}!  PK TM on FB turn: {1}, FB TM: {2}", results[i].ClanBossTurn, results[i-1].BattleParticipants.Where(bp => bp.Name == "Painkeeper").First().TurnMeter, results[i-1].AttackDetails.ActorTurnMeter);
                        }
                    }
                }
            }

            Console.ReadLine();
        }

        static void Main(string[] args)
        {
            //RunUnkillableSearcher(ClanBoss.Level.Brutal, Teams.UnkillableBase.ChampionCreators());
            //SearchForUnkillableSpeeds(ClanBoss.Level.Brutal, Teams.Gunga.ChampionCreators());
            //SearchForUnkillableSpeeds(ClanBoss.Level.Nightmare, Teams.Gunga.ChampionCreators());
            //SearchForUnkillableSpeeds(ClanBoss.Level.UltraNightmare, Teams.Gunga.ChampionCreators());
            //TestClanBossRun(ClanBoss.Level.Nightmare, Teams.Gunga.ChampionCreators());
            //TestUnkillableClanBossRun(ClanBoss.Level.Nightmare, Teams.Gunga.ChampionCreators(), false);
            TestCounterattackTeam(ClanBoss.Level.Nightmare, Teams.ChilliNM.ChampionCreators(), Teams.ChilliNM.GetStunTarget);
        }

        static void TestCounterattackTeam(ClanBoss.Level clanBossLevel, List<Teams.CreateChampion> championCreators, ClanBossBattle.StunTargetExtractor getStunTarget)
        {
            List<Champion> champions = new List<Champion>();
            List<ChampionInBattle> cibs = new List<ChampionInBattle>();
            foreach (Teams.CreateChampion cc in championCreators)
            {
                Tuple<Champion, List<Constants.SkillId>, List<Constants.SkillId>> tuple = cc(clanBossLevel);
                cibs.Add(new ChampionInBattle(tuple.Item1, tuple.Item2, tuple.Item3));
            }

            ClanBossBattle battle = new ClanBossBattle(clanBossLevel, cibs);
            battle.GetStunTarget = getStunTarget;

            List<ClanBossBattleResult> results = battle.Run();
            ClanBossBattleResultsAnalysis.PrintResults(results, false, false);
            Console.ReadLine();
        }

        static void TestUnkillableClanBossRun(ClanBoss.Level clanBossLevel, List<Teams.CreateChampion> championCreators, bool startupSequenceSearch)
        {
            List<Champion> champions = new List<Champion>();
            Dictionary<Champion, Tuple<List<Constants.SkillId>, List<Constants.SkillId>>> skillPoliciesByChampion = new Dictionary<Champion, Tuple<List<Constants.SkillId>, List<Constants.SkillId>>>();
            foreach (Teams.CreateChampion cc in championCreators)
            {
                Tuple<Champion, List<Constants.SkillId>, List<Constants.SkillId>> tuple = cc(clanBossLevel);
                champions.Add(tuple.Item1);
                skillPoliciesByChampion[tuple.Item1] = new Tuple<List<Constants.SkillId>, List<Constants.SkillId>>(tuple.Item2, tuple.Item3);
            }

            ClanBossBattle baseline = new ClanBossBattle(clanBossLevel, skillPoliciesByChampion);
            List<ClanBossBattleResult> baselineResult = baseline.Run();

            ClanBossBattle battle = new ClanBossBattle(clanBossLevel, skillPoliciesByChampion);
            IEnumerable<List<ClanBossBattleResult>> resultSet;
            if (startupSequenceSearch)
            {
                resultSet = battle.FindUnkillableStartupSequences();
            }
            else
            {
                resultSet = new List<List<ClanBossBattleResult>>() { battle.Run() };
            }

            List<ClanBossBattleResult> optimalResults = null;
            int optimalAutoAfterCBTurn = int.MaxValue;
            foreach (List<ClanBossBattleResult> results in resultSet)
            {
                int autoAfterCBTurn = 0;
                Console.WriteLine();
                Console.WriteLine("Run is over!");
                ClanBossBattleResultsAnalysis.PrintResults(results, true, false);

                foreach (ClanBossBattleResult result in results)
                {
                    if (result.AttackDetails.ActorName != Constants.Names.ClanBoss)
                    {
                        if (result.AttackDetails.ExpectedAISkill != result.AttackDetails.Skill)
                        {
                            autoAfterCBTurn = result.ClanBossTurn;
                        }
                    }
                }

                int lastKillableTurn = ClanBossBattleResultsAnalysis.LastClanBossTurnThatHitKillableChampion(results, Utils.FindSlowestChampion(champions));

                Console.WriteLine("Last turn where there was a hit on a champion that wasn't unkillable:  {0}", lastKillableTurn);
                Console.WriteLine("This setup runs on auto after turn {0}", autoAfterCBTurn);
                Console.WriteLine();
                if (optimalAutoAfterCBTurn > autoAfterCBTurn)
                {
                    optimalAutoAfterCBTurn = autoAfterCBTurn;
                    optimalResults = results;
                }
            }

            if (optimalResults != null)
            {
                Console.WriteLine("Optimal Result:");
                ClanBossBattleResultsAnalysis.PrintResults(optimalResults, true, false);

                Console.WriteLine("This setup runs on auto after turn {0}", optimalAutoAfterCBTurn);

                Console.WriteLine();
                Console.WriteLine("Maneater setup");
                foreach (ClanBossBattleResult result in optimalResults.Where(r => r.AttackDetails.ActorName == "Maneater"))
                {
                    if (result.ClanBossTurn > optimalAutoAfterCBTurn)
                    {
                        break;
                    }

                    Console.WriteLine("Turn {0,2}, Skill {1} ({2})", result.AttackDetails.ActorTurn, result.AttackDetails.Skill, result.AttackDetails.SkillName);
                }

                Console.WriteLine();
                Console.WriteLine("Painkeeper setup");
                foreach (ClanBossBattleResult result in optimalResults.Where(r => r.AttackDetails.ActorName == "Painkeeper"))
                {
                    if (result.ClanBossTurn > optimalAutoAfterCBTurn)
                    {
                        break;
                    }

                    Console.WriteLine("Turn {0,2}, Skill {1} ({2})", result.AttackDetails.ActorTurn, result.AttackDetails.Skill, result.AttackDetails.SkillName);
                }
            }
            else
            {
                Console.WriteLine("No results!");
            }
            Console.ReadLine();
        }
    }
}
