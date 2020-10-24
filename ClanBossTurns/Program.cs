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
        private static class Combinations
        {
            private static void RotateLeft<T>(IList<T> sequence, int start, int count)
            {
                T tmp = sequence[start];
                sequence.RemoveAt(start);
                sequence.Insert(start + count - 1, tmp);
            }

            public static IEnumerable<IList<T>> Get<T>(IList<T> sequence, int choose)
            {
                return Get(sequence, 0, sequence.Count, choose);
            }

            public static IEnumerable<IList<T>> Get<T>(IList<T> sequence, int start, int count, int choose)
            {
                if (choose == 0)
                {
                    yield return sequence;
                }
                else
                {
                    for (int i = 0; i < count; i++)
                    {
                        foreach (var perm in Get(sequence, start + 1, count - 1 - i, choose - 1))
                        {
                            yield return perm;
                        }

                        RotateLeft(sequence, start, count);
                    }
                }
            }
        }

        static void SearchForUnkillableSpeeds(ClanBoss.Level clanBossLevel, List<Teams.CreateChampion> championCreators)
        {
            string filename = string.Format("{0}.txt", clanBossLevel);
            using (StreamWriter txt = new StreamWriter(File.OpenWrite(filename)))
            {
                List<Champion> champions = new List<Champion>();
                Dictionary<Champion, Tuple<List<Constants.SkillId>, List<Constants.SkillId>>> skillPoliciesByChampionBase = new Dictionary<Champion, Tuple<List<Constants.SkillId>, List<Constants.SkillId>>>();
                foreach (Teams.CreateChampion cc in championCreators)
                {
                    Tuple<Champion, List<Constants.SkillId>, List<Constants.SkillId>> tuple = cc(clanBossLevel);
                    champions.Add(tuple.Item1);
                    skillPoliciesByChampionBase[tuple.Item1] = new Tuple<List<Constants.SkillId>, List<Constants.SkillId>>(tuple.Item2, tuple.Item3);
                }

                // Build up the ranges of deltas to apply to the champions
                List<Tuple<int, int>> deltasToApply = new List<Tuple<int, int>>();
                for (int speedDelta = -20; speedDelta <= 20; speedDelta++)
                {
                    for (int speedSets = 0; speedSets <= 3; speedSets++)
                    {
                        deltasToApply.Add(new Tuple<int, int>(speedDelta, speedSets));
                    }
                }

                for (int choose = 1; choose <= championCreators.Count; choose++)
                {
                    foreach (IList<Champion> champsPerIteration in Combinations.Get(champions, choose))
                    {
                        foreach (Tuple<int, int> deltaToApply in deltasToApply)
                        {
                            Dictionary<Champion, Tuple<List<Constants.SkillId>, List<Constants.SkillId>>> skillPoliciesByChampion = new Dictionary<Champion, Tuple<List<Constants.SkillId>, List<Constants.SkillId>>>();
                            for (int i = 0; i < choose; i++)
                            {
                                skillPoliciesByChampion[champsPerIteration[i].Clone(deltaToApply.Item1, deltaToApply.Item2)] = skillPoliciesByChampionBase[champsPerIteration[i]];
                            }

                            foreach (Champion c in skillPoliciesByChampionBase.Keys)
                            {
                                if (skillPoliciesByChampion.Keys.FirstOrDefault(k => k.Name == c.Name) == null)
                                {
                                    skillPoliciesByChampion[c] = skillPoliciesByChampionBase[c];
                                }
                            }

                            if (skillPoliciesByChampion.Count != championCreators.Count)
                            {
                                throw new Exception("Uh oh!");
                            }

                            ClanBossBattle battle = new ClanBossBattle(clanBossLevel, skillPoliciesByChampion);
                            List<ClanBossBattleResult> results = battle.Run();
                            int lastKillableTurn = ClanBossBattleResultsAnalysis.LastClanBossTurnThatHitKillableChampion(results, Utils.FindSlowestChampion(skillPoliciesByChampion.Keys));
                            if (lastKillableTurn < 10)
                            {
                                txt.WriteLine("Choose: {0}", choose);
                                foreach (Champion c in skillPoliciesByChampion.Keys)
                                {
                                    txt.WriteLine("{0}{1}:", c.Name, skillPoliciesByChampion.Keys.First(k => k.Name == c.Name) == skillPoliciesByChampionBase.Keys.First(k => k.Name == c.Name) ? "" : " (UPDATED)");
                                    txt.WriteLine("  Base Speed: {0}", c.BaseSpeed);
                                    txt.WriteLine("  UI Speed: {0}", c.UISpeed);
                                    txt.WriteLine("  Speed Sets: {0}", c.SpeedSets);
                                    txt.WriteLine("  Effective Speed: {0}", c.EffectiveSpeed);
                                }
                                txt.WriteLine();
                            }
                        }
                    }
                }
            }

            System.Diagnostics.Process.Start(filename);
        }

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

        static void Main(string[] args)
        {
            //RunUnkillableSearcher(ClanBoss.Level.Brutal, Teams.UnkillableBase.ChampionCreators());
            //SearchForUnkillableSpeeds(ClanBoss.Level.Brutal, Teams.Gunga.ChampionCreators());
            //SearchForUnkillableSpeeds(ClanBoss.Level.Nightmare, Teams.Gunga.ChampionCreators());
            //SearchForUnkillableSpeeds(ClanBoss.Level.UltraNightmare, Teams.Gunga.ChampionCreators());
            //TestClanBossRun(ClanBoss.Level.Nightmare, Teams.Gunga.ChampionCreators());
            //TestUnkillableClanBossRun(ClanBoss.Level.UltraNightmare, Teams.Rust.ChampionCreators(), true);
            TestCounterattackTeam(ClanBoss.Level.Nightmare, Teams.Chilli.ChampionCreators());
        }

        static void TestCounterattackTeam(ClanBoss.Level clanBossLevel, List<Teams.CreateChampion> championCreators)
        {
            List<Champion> champions = new List<Champion>();
            Dictionary<Champion, Tuple<List<Constants.SkillId>, List<Constants.SkillId>>> skillPoliciesByChampion = new Dictionary<Champion, Tuple<List<Constants.SkillId>, List<Constants.SkillId>>>();
            foreach (Teams.CreateChampion cc in championCreators)
            {
                Tuple<Champion, List<Constants.SkillId>, List<Constants.SkillId>> tuple = cc(clanBossLevel);
                champions.Add(tuple.Item1);
                skillPoliciesByChampion[tuple.Item1] = new Tuple<List<Constants.SkillId>, List<Constants.SkillId>>(tuple.Item2, tuple.Item3);
            }

            UnkillableClanBossBattle baseline = new UnkillableClanBossBattle(clanBossLevel, skillPoliciesByChampion);
            List<ClanBossBattleResult> results = baseline.Run();

            foreach (ClanBossBattleResult result in results)
            {
                Console.WriteLine("{0,2}: {1,20} turn {2,2} use skill {3} ({4,20})", result.ClanBossTurn, result.AttackDetails.ActorName, result.AttackDetails.ActorTurn, result.AttackDetails.Skill, result.AttackDetails.SkillName);
                if (result.Counterattacks != null)
                {
                    foreach (ClanBossBattleResult.Attack ca in result.Counterattacks)
                    {
                        Console.WriteLine("    {0,20} counterattacks for turn {1,2}", ca.ActorName, ca.ActorTurn);
                    }
                }
            }

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

            UnkillableClanBossBattle baseline = new UnkillableClanBossBattle(clanBossLevel, skillPoliciesByChampion);
            List<ClanBossBattleResult> baselineResult = baseline.Run();

            UnkillableClanBossBattle battle = new UnkillableClanBossBattle(clanBossLevel, skillPoliciesByChampion);
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
                foreach (ClanBossBattleResult result in results)
                {
                    Console.WriteLine("{0,2}: {1,20} turn {2,2} use skill {3} ({4})", result.ClanBossTurn, result.AttackDetails.ActorName, result.AttackDetails.ActorTurn, result.AttackDetails.Skill, result.AttackDetails.SkillName);
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
                //Console.ReadLine();
                if (optimalAutoAfterCBTurn > autoAfterCBTurn)
                {
                    optimalAutoAfterCBTurn = autoAfterCBTurn;
                    optimalResults = results;
                }
            }

            if (optimalResults != null)
            {
                Console.WriteLine("Optimal Result:");
                foreach (ClanBossBattleResult result in optimalResults)
                {
                    Console.WriteLine("{0,2}: {1,20} turn {2,2} use skill {3} ({4,20}) - Unkillable Champs: {5}", result.ClanBossTurn, result.AttackDetails.ActorName, result.AttackDetails.ActorTurn, result.AttackDetails.Skill, result.AttackDetails.SkillName, string.Join(", ", result.BattleParticipants.Where(p => !p.IsClanBoss && p.ActiveBuffs.ContainsKey(Constants.Buff.Unkillable)).Select(p => p.Name)));
                }
                int lastKillableTurn = ClanBossBattleResultsAnalysis.LastClanBossTurnThatHitKillableChampion(optimalResults, Utils.FindSlowestChampion(champions));

                Console.WriteLine("Last turn where there was a hit on a champion that wasn't unkillable:  {0}", lastKillableTurn);
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
