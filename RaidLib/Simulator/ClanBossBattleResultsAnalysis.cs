using RaidLib.DataModel;
using RaidLib.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaidLib.Simulator
{
    public static class ClanBossBattleResultsAnalysis
    {
        public static void PrintSummary(List<ClanBossBattleResult> results, Champion stunTarget, bool includeUnkillable, bool includeTM)
        {
            ClanBossBattleResultsAnalysis.PrintResults(results, includeUnkillable, includeTM);
            int lastKillableTurn = ClanBossBattleResultsAnalysis.LastClanBossTurnThatHitKillableChampion(results, stunTarget);
            Console.WriteLine("Last turn where there was a hit on a champion that wasn't unkillable:  {0}", lastKillableTurn);
            PrintAttacksPerChampion(results);
        }

        public static void PrintAttacksPerChampion(List<ClanBossBattleResult> results)
        {
            Dictionary<string, int> championToAttackCount = new Dictionary<string, int>();
            foreach (ClanBossBattleResult result in results)
            {
                championToAttackCount[result.AttackDetails.ActorName] = result.AttackDetails.ActorTurn;

                if (result.AdditionalAttacks != null)
                {
                    foreach (ClanBossBattleResult.Attack aa in result.AdditionalAttacks)
                    {
                        championToAttackCount[aa.ActorName] = aa.ActorTurn;
                    }
                }
            }

            foreach (string name in championToAttackCount.Keys)
            {
                Console.WriteLine("{0} had {1} attacks.", name, championToAttackCount[name]);
            }
        }

        public static void PrintResults(List<ClanBossBattleResult> results, bool includeUnkillable, bool includeTM)
        {
            foreach (ClanBossBattleResult result in results)
            {
                string suffix = string.Empty;
                if (includeUnkillable)
                {
                    suffix = string.Format(" - Unkillable Champs: {0}", string.Join(", ", result.BattleParticipants.Where(p => !p.IsClanBoss && p.ActiveBuffs.ContainsKey(Constants.Buff.Unkillable)).Select(p => p.Name)));
                }

                if (includeTM)
                {
                    suffix += string.Format(" - TM: {0}", string.Join(", ", result.BattleParticipants.Where(p => !p.IsClanBoss).Select(p => p.Name + ": " + p.TurnMeter)));
                }

                Console.WriteLine("{0,2}: {1,20} turn {2,2} use skill {3} ({4,20}){5}", result.ClanBossTurn, result.AttackDetails.ActorName, result.AttackDetails.ActorTurn, result.AttackDetails.Skill, result.AttackDetails.SkillName, suffix);

                if (result.AdditionalAttacks != null)
                {
                    foreach (ClanBossBattleResult.Attack ca in result.AdditionalAttacks)
                    {
                        Console.WriteLine("    {0,20} also attacks for turn {1,2}", ca.ActorName, ca.ActorTurn);
                    }
                }
            }
        }

        public static int LastClanBossTurnThatHitKillableChampion(List<ClanBossBattleResult> results, Champion stunTarget)
        {
            int turn = -1;

            foreach (ClanBossBattleResult result in results)
            {
                if (result.AttackDetails.ActorName == Constants.Names.ClanBoss)
                {
                    if (result.AttackDetails.Skill == Constants.SkillId.A1 || result.AttackDetails.Skill == Constants.SkillId.A2)
                    {
                        // AOE hits, all champs must be unkillable
                        foreach (ClanBossBattleResult.BattleParticipantStats championStat in result.BattleParticipants.Where(bp => !bp.IsClanBoss))
                        {
                            if (!championStat.ActiveBuffs.ContainsKey(Constants.Buff.Unkillable))
                            {
                                turn = result.ClanBossTurn;
                            }
                        }    
                    }
                    else if (result.AttackDetails.Skill == Constants.SkillId.A3)
                    {
                        // Single target stun
                        ClanBossBattleResult.BattleParticipantStats bpStats = result.BattleParticipants.Where(bp => bp.Name == stunTarget.Name).First();
                        if (!bpStats.ActiveBuffs.ContainsKey(Constants.Buff.Unkillable))
                        {
                            turn = result.ClanBossTurn;
                        }
                    }
                }
            }

            return turn;
        }
    }
}
