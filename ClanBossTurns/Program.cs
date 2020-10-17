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
                                    txt.WriteLine("{0}:", c.Name);
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

        static void Main(string[] args)
        {
            SearchForUnkillableSpeeds(ClanBoss.Level.Brutal, Teams.Gunga.ChampionCreators());
            SearchForUnkillableSpeeds(ClanBoss.Level.Nightmare, Teams.Gunga.ChampionCreators());
            SearchForUnkillableSpeeds(ClanBoss.Level.UltraNightmare, Teams.Gunga.ChampionCreators());
            //TestClanBossRun(ClanBoss.Level.Nightmare, Teams.Gunga.ChampionCreators());
        }

        static void TestClanBossRun(ClanBoss.Level clanBossLevel, List<Teams.CreateChampion> championCreators)
        {
            List<Champion> champions = new List<Champion>();
            Dictionary<Champion, Tuple<List<Constants.SkillId>, List<Constants.SkillId>>> skillPoliciesByChampion = new Dictionary<Champion, Tuple<List<Constants.SkillId>, List<Constants.SkillId>>>();
            foreach (Teams.CreateChampion cc in championCreators)
            {
                Tuple<Champion, List<Constants.SkillId>, List<Constants.SkillId>> tuple = cc(clanBossLevel);
                champions.Add(tuple.Item1);
                skillPoliciesByChampion[tuple.Item1] = new Tuple<List<Constants.SkillId>, List<Constants.SkillId>>(tuple.Item2, tuple.Item3);
            }

            ClanBossBattle battle = new ClanBossBattle(clanBossLevel, skillPoliciesByChampion);
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
