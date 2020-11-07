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
        private static void RunUnkillableSearcher(ClanBoss.Level clanBossLevel, List<Champion.CreateChampion> championCreators, bool multiLevel)
        {
            Dictionary<string, Champion> initialChampions = new Dictionary<string, Champion>();
            Dictionary<Champion, Tuple<List<Constants.SkillId>, List<Constants.SkillId>>> skillPoliciesByChampionBase = new Dictionary<Champion, Tuple<List<Constants.SkillId>, List<Constants.SkillId>>>();
            foreach (Champion.CreateChampion cc in championCreators)
            {
                Tuple<Champion, List<Constants.SkillId>, List<Constants.SkillId>> tuple = cc(clanBossLevel);
                skillPoliciesByChampionBase[tuple.Item1] = new Tuple<List<Constants.SkillId>, List<Constants.SkillId>>(tuple.Item2, tuple.Item3);
                initialChampions[tuple.Item1.Name] = tuple.Item1;
            }

            UnkillableSearcher searcher = new UnkillableSearcher(skillPoliciesByChampionBase);
            Dictionary<int, List<List<Champion>>> unkillableTeamsByChangedChampionCount = new Dictionary<int, List<List<Champion>>>();
            foreach (List<Champion> unkillableTeam in searcher.Search(clanBossLevel))
            {
                if (multiLevel)
                {
                    bool isMultiLevelUnkillable = true;

                    foreach (ClanBoss.Level cbl in new List<ClanBoss.Level>() { ClanBoss.Level.Brutal, ClanBoss.Level.Nightmare, ClanBoss.Level.UltraNightmare })
                    {
                        List<ChampionInBattle> cibs = new List<ChampionInBattle>();
                        foreach (Champion champ in unkillableTeam)
                        {
                            Tuple<List<Constants.SkillId>, List<Constants.SkillId>> tuple = skillPoliciesByChampionBase[initialChampions[champ.Name]];
                            cibs.Add(new ChampionInBattle(champ, tuple.Item1, tuple.Item2));
                        }

                        ClanBossBattle baseline = new ClanBossBattle(cbl, cibs);
                        List<ClanBossBattleResult> baselineResult = baseline.Run();
                        int lastKillableTurn = ClanBossBattleResultsAnalysis.LastClanBossTurnThatHitKillableChampion(baselineResult, Utils.FindSlowestChampion(unkillableTeam));
                        if (lastKillableTurn > 10)
                        {
                            isMultiLevelUnkillable = false;
                        }
                    }

                    if (!isMultiLevelUnkillable)
                    {
                        continue;
                    }
                }

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
                    Console.WriteLine("  Perception Sets: {0}", c.PerceptionSets);
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
                            writer.WriteLine("  Perception Sets: {0}", c.PerceptionSets);
                            writer.WriteLine("  Effective Speed: {0}", c.EffectiveSpeed);
                        }
                        writer.WriteLine();
                    }
                    writer.WriteLine("================");
                }
            }

            System.Diagnostics.Process.Start(filename);
        }

        static void PrintEffectiveSpeeds(List<Champion.CreateChampion> ccs, ClanBoss.Level level)
        {
            foreach (Champion.CreateChampion cc in ccs)
            {
                Champion c = cc(level).Item1;
                Console.WriteLine("{0}: {1}", c.Name, c.EffectiveSpeed);
            }
        }

        static void Main(string[] args)
        {
            //RunUnkillableSearcher(ClanBoss.Level.Brutal, RaidLib.Simulator.Teams.Gunga.MultiLevel.ChampionCreators());
            //TestUnkillableClanBossRun(ClanBoss.Level.UltraNightmare, RaidLib.Simulator.Teams.Gunga.MultiLevel.ChampionCreators(), false);
            //TestUnkillableClanBossRun(ClanBoss.Level.Brutal, RaidLib.Simulator.Teams.Gunga.MultiLevel.ChampionCreators(), false);
            //TestUnkillableClanBossRun(ClanBoss.Level.Brutal, RaidLib.Simulator.Teams.DeadwoodJedi.ChampionCreators(), false);
            //TestCounterattackTeam(ClanBoss.Level.Nightmare, Teams.ChilliNM.ChampionCreators(), Teams.ChilliNM.GetStunTarget);

            //RunUnkillableSearcher(ClanBoss.Level.Brutal, RaidLib.Simulator.Teams.Gunga.MultiLevel.ChampionCreators(), true);

            Console.WriteLine("MultiLevelUnkillable UNM:");
            PrintEffectiveSpeeds(RaidLib.Simulator.Teams.MultiLevelUnkillable.ChampionCreators(), ClanBoss.Level.UltraNightmare);
            Console.WriteLine();
            Console.WriteLine("MultiLevelUnkillable NM:");
            PrintEffectiveSpeeds(RaidLib.Simulator.Teams.MultiLevelUnkillable.ChampionCreators(), ClanBoss.Level.Nightmare);
            Console.WriteLine();
            TestMultiLevelUnkillableClanBossRun(RaidLib.Simulator.Teams.Gunga.MultiLevel.ChampionCreators());
            Console.ReadLine();

            /*
            TestUnkillableClanBossRun(ClanBoss.Level.UltraNightmare, RaidLib.Simulator.Teams.AllyAttackUnkillable.ChampionCreators(), false);
            Console.ReadLine();

            Console.WriteLine("DWJ:");
            TestMultiLevelUnkillableClanBossRun(RaidLib.Simulator.Teams.DeadwoodJedi.ChampionCreators());
            Console.WriteLine();

            Console.WriteLine("Gunga:");
            TestMultiLevelUnkillableClanBossRun(RaidLib.Simulator.Teams.Gunga.MultiLevel.ChampionCreators());
            Console.ReadLine();

            TestUnkillableClanBossRun(ClanBoss.Level.Brutal, RaidLib.Simulator.Teams.Gunga.MultiLevel.ChampionCreators(), false);
            Console.ReadLine();
            */
        }

        static void TestCounterattackTeam(ClanBoss.Level clanBossLevel, List<Champion.CreateChampion> championCreators, ClanBossBattle.StunTargetExtractor getStunTarget)
        {
            List<ChampionInBattle> cibs = new List<ChampionInBattle>();
            foreach (Champion.CreateChampion cc in championCreators)
            {
                Tuple<Champion, List<Constants.SkillId>, List<Constants.SkillId>> tuple = cc(clanBossLevel);
                cibs.Add(new ChampionInBattle(tuple.Item1, tuple.Item2, tuple.Item3));
            }

            ClanBossBattle battle = new ClanBossBattle(clanBossLevel, cibs);
            battle.GetStunTarget = getStunTarget;

            List<ClanBossBattleResult> results = battle.Run();
            ClanBossBattleResultsAnalysis.PrintResults(results, false, false);
        }

        static void TestMultiLevelUnkillableClanBossRun(List<Champion.CreateChampion> championCreators)
        {
            foreach (ClanBoss.Level clanBossLevel in new List<ClanBoss.Level>() { ClanBoss.Level.Brutal, ClanBoss.Level.Nightmare, ClanBoss.Level.UltraNightmare })
            {
                Console.WriteLine("Speeds for {0}:", clanBossLevel);
                PrintEffectiveSpeeds(championCreators, clanBossLevel);

                List<Champion> champions = new List<Champion>();
                List<ChampionInBattle> cibs = new List<ChampionInBattle>();
                foreach (Champion.CreateChampion cc in championCreators)
                {
                    Tuple<Champion, List<Constants.SkillId>, List<Constants.SkillId>> tuple = cc(clanBossLevel);
                    champions.Add(tuple.Item1);
                    cibs.Add(new ChampionInBattle(tuple.Item1, tuple.Item2, tuple.Item3));
                }

                ClanBossBattle baseline = new ClanBossBattle(clanBossLevel, cibs);
                List<ClanBossBattleResult> baselineResult = baseline.Run();
                int lastKillableTurn = ClanBossBattleResultsAnalysis.LastClanBossTurnThatHitKillableChampion(baselineResult, Utils.FindSlowestChampion(champions));
                Console.WriteLine("{0}: Last turn where there was a hit on a champion that wasn't unkillable:  {1}", clanBossLevel, lastKillableTurn);
            }
        }

        static void TestUnkillableClanBossRun(ClanBoss.Level clanBossLevel, List<Champion.CreateChampion> championCreators, bool startupSequenceSearch)
        {
            List<Tuple<Champion, List<Constants.SkillId>, List<Constants.SkillId>>> champTuples = new List<Tuple<Champion, List<Constants.SkillId>, List<Constants.SkillId>>>();
            foreach (Champion.CreateChampion cc in championCreators)
            {
                champTuples.Add(cc(clanBossLevel));
            }

            List<Champion> champions = new List<Champion>();
            List<ChampionInBattle> cibs = new List<ChampionInBattle>();
            foreach (Tuple<Champion, List<Constants.SkillId>, List<Constants.SkillId>> tuple in champTuples)
            {
                champions.Add(tuple.Item1);
                cibs.Add(new ChampionInBattle(tuple.Item1, tuple.Item2, tuple.Item3));
            }
            ClanBossBattle baseline = new ClanBossBattle(clanBossLevel, cibs);
            List<ClanBossBattleResult> baselineResult = baseline.Run();
            Console.WriteLine("Baseline Results:");
            ClanBossBattleResultsAnalysis.PrintSummary(baselineResult, Utils.FindSlowestChampion(champions), true, true);

            if (startupSequenceSearch)
            {
                cibs = new List<ChampionInBattle>();
                foreach (Tuple<Champion, List<Constants.SkillId>, List<Constants.SkillId>> tuple in champTuples)
                {
                    cibs.Add(new ChampionInBattle(tuple.Item1, tuple.Item2, tuple.Item3));
                }

                ClanBossBattle battle = new ClanBossBattle(clanBossLevel, cibs);
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
            }
        }
    }
}
