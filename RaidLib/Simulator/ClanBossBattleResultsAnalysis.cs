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
                if (result.ActorName == Constants.Names.ClanBoss)
                {
                    if (result.Skill == Constants.SkillId.A1 || result.Skill == Constants.SkillId.A2)
                    {
                        // AOE hits, all champs must be unkillable
                        foreach (ClanBossBattleResult.ChampionStats championStat in result.Champions)
                        {
                            if (!championStat.ActiveBuffs.ContainsKey(Constants.Buff.Unkillable))
                            {
                                turn = result.ClanBossTurn;
                            }
                        }    
                    }
                    else if (result.Skill == Constants.SkillId.A3)
                    {
                        // Single target stun
                        ClanBossBattleResult.ChampionStats championStat = result.Champions.Where(cs => cs.Champion == stunTarget).First();
                        if (!championStat.ActiveBuffs.ContainsKey(Constants.Buff.Unkillable))
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
