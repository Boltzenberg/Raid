using RaidLib.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace RaidLib.Simulator
{
    public class ClanBossBattle
    {
        private Clock clock;
        private ClanBossInBattle clanBoss;
        private List<ChampionInBattle> champions;
        private const int MaxClanBossTurns = 50;

        public ClanBossBattle(ClanBoss.Level level, List<Champion> champs, Dictionary<Champion, List<SkillPolicy>> skillPoliciesByChampion)
        {
            this.clock = new Clock();
            ClanBoss cb = ClanBoss.Get(level);
            this.clanBoss = new ClanBossInBattle(cb, this.clock);
            this.champions = new List<ChampionInBattle>();
            foreach (Champion champ in champs)
            {
                List<SkillPolicy> policies = null;
                skillPoliciesByChampion.TryGetValue(champ, out policies);
                this.champions.Add(new ChampionInBattle(champ, policies, this.clock));
            }
        }

        public List<ClanBossBattleResult> Run()
        {
            List<ClanBossBattleResult> results = new List<ClanBossBattleResult>();

            int clockTick = 0;
            int clanBossTurn = 0;
            while (clanBossTurn < MaxClanBossTurns)
            {
                clockTick++;
                this.clock.Tick();
                this.champions.Sort((a, b) => b.TurnMeter.CompareTo(a.TurnMeter));
                
                ChampionInBattle maxTMChamp = this.champions.First();
                string actorName = string.Empty;
                Constants.SkillId skillIdUsed = Constants.SkillId.None;
                string skillNameUsed = string.Empty;
                if (maxTMChamp.TurnMeter > this.clanBoss.TurnMeter)
                {
                    if (maxTMChamp.TurnMeter >= Constants.TurnMeter.Full)
                    {
                        actorName = maxTMChamp.Champ.Name;
                        //this.PrintTurnMeters();
                        Skill skillUsed = maxTMChamp.TakeTurn();
                        skillIdUsed = skillUsed.Id;
                        skillNameUsed = skillUsed.Name;
                        TurnAction action = skillUsed.TurnAction;
                        if (action.BuffsToApply != null)
                        {
                            foreach (BuffToApply buff in action.BuffsToApply)
                            {
                                if (buff.Target == Constants.Target.Self)
                                {
                                    maxTMChamp.ApplyBuff(buff);
                                }
                                else if (buff.Target == Constants.Target.AllAllies)
                                {
                                    foreach (ChampionInBattle cib in this.champions)
                                    {
                                        if (cib != maxTMChamp)
                                        {
                                            cib.ApplyBuff(buff);
                                        }
                                    }
                                }
                                else if (buff.Target == Constants.Target.FullTeam)
                                {
                                    foreach (ChampionInBattle cib in this.champions)
                                    {
                                        cib.ApplyBuff(buff);
                                    }
                                }
                            }
                        }

                        if (action.DebuffsToApply != null)
                        {
                            foreach (DebuffToApply debuff in action.DebuffsToApply)
                            {
                                clanBoss.ApplyDebuff(debuff);
                            }
                        }

                        if (action.EffectsToApply != null)
                        {
                            foreach (EffectToApply effect in action.EffectsToApply)
                            {
                                if (effect.Target == Constants.Target.Self)
                                {
                                    maxTMChamp.ApplyEffect(effect.Effect);
                                }
                                else if (effect.Target == Constants.Target.AllAllies)
                                {
                                    foreach (ChampionInBattle cib in this.champions)
                                    {
                                        if (cib != maxTMChamp)
                                        {
                                            cib.ApplyEffect(effect.Effect);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    if (this.clanBoss.TurnMeter >= Constants.TurnMeter.Full)
                    {
                        actorName = Constants.Names.ClanBoss;
                        //this.PrintTurnMeters();
                        Skill skillUsed = this.clanBoss.TakeTurn();
                        skillIdUsed = skillUsed.Id;
                        skillNameUsed = skillUsed.Name;
                        TurnAction action = skillUsed.TurnAction;
                        clanBossTurn++;

                        if (action.AttackTarget == Constants.Target.AllEnemies)
                        {
                            foreach (ChampionInBattle cib in this.champions)
                            {
                                cib.GetAttacked(action.AttackCount);
                                if (!cib.IsUnkillable)
                                {
                                    Console.WriteLine("!!!!!!!!!! {0} attacked but not unkillable !!!!!!!!", cib.Champ.Name);
                                }
                            }
                        }
                        else if (action.AttackTarget == Constants.Target.OneEnemy)
                        {
                            // TODO:  This is the stun.  Apply it to the slowest champion.
                            ChampionInBattle slowboi = this.champions.First();
                            foreach (ChampionInBattle cib in this.champions)
                            {
                                if (slowboi.Champ.EffectiveSpeed > cib.Champ.EffectiveSpeed)
                                {
                                    slowboi = cib;
                                }
                            }

                            slowboi.GetAttacked(action.AttackCount);
                            if (!slowboi.IsUnkillable)
                            {
                                Console.WriteLine("!!!!!!!!!! {0} attacked but not unkillable !!!!!!!!", slowboi.Champ.Name);
                            }

                            if (action.DebuffsToApply != null)
                            {
                                slowboi.ApplyDebuff(action.DebuffsToApply.First());
                            }
                        }
                    }
                }

                List<ClanBossBattleResult.ChampionStats> champStats = new List<ClanBossBattleResult.ChampionStats>();
                foreach (ChampionInBattle cib in this.champions)
                {
                    ClanBossBattleResult.ChampionStats champStat = new ClanBossBattleResult.ChampionStats(cib.Champ, cib.TurnMeter, new Dictionary<Constants.Buff, int>(cib.ActiveBuffs), new Dictionary<Constants.Debuff, int>(cib.ActiveDebuffs), new Dictionary<Constants.SkillId, int>(cib.SkillCooldowns));
                    champStats.Add(champStat);
                }

                ClanBossBattleResult.ClanBossStats cbStats = new ClanBossBattleResult.ClanBossStats(this.clanBoss.TurnMeter, new Dictionary<Constants.Buff, int>(this.clanBoss.ActiveBuffs), new Dictionary<Constants.Debuff, int>(this.clanBoss.ActiveDebuffs));

                ClanBossBattleResult result = new ClanBossBattleResult(clockTick, clanBossTurn, actorName, skillIdUsed, skillNameUsed, champStats, cbStats);
                results.Add(result);
            }

            return results;
        }

        private void PrintTurnMeters()
        {
            foreach (ChampionInBattle cib in this.champions)
            {
                Console.WriteLine("  Champion {0} turn meter {1}", cib.Champ.Name, cib.TurnMeter);
            }
            Console.WriteLine("  Clan Boss turn meter {0}", this.clanBoss.TurnMeter);
        }
    }
}
