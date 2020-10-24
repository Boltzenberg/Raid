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
        //private ClanBossInBattle clanBoss;
        //private List<ChampionInBattle> champions;
        List<IBattleParticipant> battleParticipants;
        private const int MaxClanBossTurns = 50;

        public ClanBossBattle(ClanBoss.Level level, Dictionary<Champion, Tuple<List<Constants.SkillId>, List<Constants.SkillId>>> skillPoliciesByChampion)
        {
            this.battleParticipants = new List<IBattleParticipant>();
            foreach (Champion champ in skillPoliciesByChampion.Keys)
            {
                Tuple<List<Constants.SkillId>, List<Constants.SkillId>> policies = skillPoliciesByChampion[champ];
                this.battleParticipants.Add(new ChampionInBattle(champ, policies.Item1, policies.Item2));
            }
            this.battleParticipants.Add(new ClanBossInBattle(ClanBoss.Get(level)));
        }

        public List<ClanBossBattleResult> Run()
        {
            /*
            List<ClanBossBattleResult> results = new List<ClanBossBattleResult>();

            bool continueBattle = true;
            while (continueBattle)
            {
                // Advance turn meter for each battle participant
                foreach (IBattleParticipant participant in this.battleParticipants)
                {
                    participant.ClockTick();
                }

                // See who has the most turn meter
                double maxTurnMeter = double.MinValue;
                foreach (IBattleParticipant participant in this.battleParticipants)
                {
                    maxTurnMeter = Math.Max(maxTurnMeter, participant.TurnMeter);
                }

                // See if anybody has a full turn meter
                if (maxTurnMeter > Constants.TurnMeter.Full)
                {
                    string actorName = string.Empty;
                    Constants.SkillId skillIdUsed = Constants.SkillId.None;
                    string skillNameUsed = string.Empty;

                    ChampionInBattle maxTMChamp = null;
                    foreach (ChampionInBattle champ in this.champions)
                    {
                        if (champ.TurnMeter == maxTurnMeter)
                        {
                            maxTMChamp = champ;
                            break;
                        }
                    }

                    if (maxTMChamp != null)
                    {
                        actorName = maxTMChamp.Champ.Name;
                        // this.PrintTurnMeters();
                        Skill skillUsed = maxTMChamp.NextAISkill();
                        maxTMChamp.TakeTurn(skillUsed);
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
                    else
                    {
                        actorName = Constants.Names.ClanBoss;
                        // this.PrintTurnMeters();
                        Skill skillUsed = this.clanBoss.NextAISkill();
                        this.clanBoss.TakeTurn(skillUsed);
                        skillIdUsed = skillUsed.Id;
                        skillNameUsed = skillUsed.Name;
                        TurnAction action = skillUsed.TurnAction;
                        clanBossTurn++;

                        if (action.AttackTarget == Constants.Target.AllEnemies)
                        {
                            foreach (ChampionInBattle cib in this.champions)
                            {
                                cib.GetAttacked(action.AttackCount);
                                if (!cib.ActiveBuffs.ContainsKey(Constants.Buff.Unkillable))
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
                            if (!slowboi.ActiveBuffs.ContainsKey(Constants.Buff.Unkillable))
                            {
                                Console.WriteLine("!!!!!!!!!! {0} attacked but not unkillable !!!!!!!!", slowboi.Champ.Name);
                            }

                            if (action.DebuffsToApply != null)
                            {
                                slowboi.ApplyDebuff(action.DebuffsToApply.First());
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

                    ClanBossBattleResult result = new ClanBossBattleResult(clockTick, clanBossTurn, actorName, skillIdUsed, skillNameUsed, champStats, cbStats, null);
                    results.Add(result);
                }
            }

            return results;*/
            return null;
        }
        /*
        private void PrintTurnMeters()
        {
            foreach (ChampionInBattle cib in this.champions)
            {
                Console.WriteLine("  Champion {0} turn meter {1}", cib.Champ.Name, cib.TurnMeter);
            }
            Console.WriteLine("  Clan Boss turn meter {0}", this.clanBoss.TurnMeter);
        }*/
    }
}
