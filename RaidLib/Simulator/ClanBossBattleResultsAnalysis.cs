using RaidLib.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaidLib.Simulator
{
    public static class ClanBossBattleResultsAnalysis
    {
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
