using RaidLib.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace RaidLib.Simulator
{
    public class UnkillableClanBossBattle
    {
        private class UCBBState
        {
            public List<IBattleParticipant> BattleParticipants { get; private set; }
            public List<ClanBossBattleResult> Results { get; private set; }

            public UCBBState(List<ChampionInBattle> champions, ClanBossInBattle clanBoss)
            {
                this.BattleParticipants = new List<IBattleParticipant>();
                foreach (ChampionInBattle cib in champions)
                {
                    this.BattleParticipants.Add(cib);
                }
                this.BattleParticipants.Add(clanBoss);

                this.Results = new List<ClanBossBattleResult>();
            }

            public UCBBState(UCBBState other)
            {
                this.BattleParticipants = new List<IBattleParticipant>();
                foreach (IBattleParticipant bp in other.BattleParticipants)
                {
                    this.BattleParticipants.Add(bp.Clone());
                }
                this.Results = new List<ClanBossBattleResult>(other.Results);
            }
        }

        private const int MaxClanBossTurns = 50;
        private const int LastKillableTurn = 7;
        private const int AutoAfterClanBossTurn = 7;
        private UCBBState initialState;

        public UnkillableClanBossBattle(ClanBoss.Level level, Dictionary<Champion, Tuple<List<Constants.SkillId>, List<Constants.SkillId>>> skillPoliciesByChampion)
        {
            ClanBossInBattle clanBoss = new ClanBossInBattle(ClanBoss.Get(level));
            List<ChampionInBattle> champions = new List<ChampionInBattle>();
            foreach (Champion champ in skillPoliciesByChampion.Keys)
            {
                Tuple<List<Constants.SkillId>, List<Constants.SkillId>> policies = skillPoliciesByChampion[champ];
                champions.Add(new ChampionInBattle(champ, policies.Item1, policies.Item2));
            }

            this.initialState = new UCBBState(champions, clanBoss);
        }

        private static double GetMaxTurnMeter(List<ChampionInBattle> champs, ClanBossInBattle cb)
        {
            double maxTM = double.MinValue;
            foreach (ChampionInBattle champ in champs)
            {
                maxTM = Math.Max(maxTM, champ.TurnMeter);
            }

            maxTM = Math.Max(maxTM, cb.TurnMeter);

            return maxTM;
        }

        public List<ClanBossBattleResult> Run()
        {
            return this.Run(false, false).First();
        }

        public IEnumerable<List<ClanBossBattleResult>> FindUnkillableStartupSequences()
        {
            return this.Run(true, true);
        }

        private IEnumerable<List<ClanBossBattleResult>> Run(bool exploreAllSequences, bool failOnKill)
        { 
            Queue<UCBBState> battleStates = new Queue<UCBBState>();
            battleStates.Enqueue(this.initialState);

            // While the queue isn't empty
            // run all possible next turns for the head of the queue state and enqueue those states
            // If someone is killed after the last unkillable turn, the run fails (no more enqueues)
            // If clan boss has turn 50, the run succeeds (return the result)
            while (battleStates.Count > 0)
            {
                UCBBState state = battleStates.Dequeue();
                bool returnResults = false;

                // Advance turn meter for each battle participant
                foreach (IBattleParticipant participant in state.BattleParticipants)
                {
                    participant.ClockTick();
                }

                // See who has the most turn meter
                double maxTurnMeter = double.MinValue;
                foreach (IBattleParticipant participant in state.BattleParticipants)
                {
                    maxTurnMeter = Math.Max(maxTurnMeter, participant.TurnMeter);
                }

                // See if anybody has a full turn meter
                if (maxTurnMeter <= Constants.TurnMeter.Full)
                {
                    // Nothing to do this time, re-enqueue this state.
                    battleStates.Enqueue(state);
                }
                else
                {
                    // Champion with the fullest turn meter takes a turn!
                    IBattleParticipant maxTMChamp = state.BattleParticipants.First(bp => bp.TurnMeter == maxTurnMeter);

                    IEnumerable<Skill> skillsToRun;
                    Skill nextAISkill = maxTMChamp.NextAISkill();
                    if (exploreAllSequences && state.BattleParticipants.Where(bp => bp.IsClanBoss).First().TurnCount < AutoAfterClanBossTurn)
                    {
                        skillsToRun = maxTMChamp.AllAvailableSkills();
                    }
                    else
                    {
                        skillsToRun = new List<Skill>() { maxTMChamp.NextAISkill() };
                    }

                    UCBBState currentState = state;
                    foreach (Skill skill in skillsToRun)
                    {
                        state = new UCBBState(currentState);
                        IBattleParticipant champ = state.BattleParticipants.Where(bp => bp.Name == maxTMChamp.Name).First();
                        List<IBattleParticipant> counterAttackers = new List<IBattleParticipant>();

                        champ.TakeTurn(skill);

                        if (!champ.IsClanBoss)
                        {
                            TurnAction action = skill.TurnAction;
                            if (action.BuffsToApply != null)
                            {
                                foreach (BuffToApply buff in action.BuffsToApply)
                                {
                                    if (buff.Target == Constants.Target.Self)
                                    {
                                        champ.ApplyBuff(buff);
                                    }
                                    else if (buff.Target == Constants.Target.AllAllies)
                                    {
                                        foreach (IBattleParticipant bp in state.BattleParticipants.Where(p => !p.IsClanBoss && p != champ))
                                        {
                                            bp.ApplyBuff(buff);
                                        }
                                    }
                                    else if (buff.Target == Constants.Target.FullTeam)
                                    {
                                        foreach (IBattleParticipant bp in state.BattleParticipants.Where(p => !p.IsClanBoss))
                                        {
                                            bp.ApplyBuff(buff);
                                        }
                                    }
                                }
                            }

                            if (action.EffectsToApply != null)
                            {
                                foreach (EffectToApply effect in action.EffectsToApply)
                                {
                                    if (effect.Target == Constants.Target.Self)
                                    {
                                        champ.ApplyEffect(effect.Effect);
                                    }
                                    else if (effect.Target == Constants.Target.AllAllies)
                                    {
                                        foreach (IBattleParticipant bp in state.BattleParticipants.Where(p => !p.IsClanBoss && p != champ))
                                        {
                                            bp.ApplyEffect(effect.Effect);
                                        }
                                    }
                                }
                            }

                            battleStates.Enqueue(state);
                        }
                        else
                        {
                            // Clan boss turn!
                            TurnAction action = skill.TurnAction;
                            bool enqueueNewState = true;

                            if (action.AttackTarget == Constants.Target.AllEnemies)
                            {
                                foreach (IBattleParticipant bp in state.BattleParticipants.Where(p => !p.IsClanBoss))
                                {
                                    bp.GetAttacked(action.AttackCount);
                                    
                                    if (bp.ActiveBuffs.ContainsKey(Constants.Buff.Counterattack) &&
                                        !bp.ActiveDebuffs.ContainsKey(Constants.Debuff.Stun))
                                    {
                                        counterAttackers.Add(bp);
                                    }

                                    if (champ.TurnCount > LastKillableTurn && !bp.ActiveBuffs.ContainsKey(Constants.Buff.Unkillable))
                                    {
                                        // run failed!
                                        enqueueNewState = !failOnKill;
                                        //Console.WriteLine("!!!!!!!!!! {0} attacked but not unkillable !!!!!!!!", bp.Name);
                                    }
                                }
                            }
                            else if (action.AttackTarget == Constants.Target.OneEnemy)
                            {
                                // TODO:  This is the stun.  Apply it to the slowest champion.
                                IBattleParticipant slowboi = state.BattleParticipants.First();
                                foreach (IBattleParticipant bp in state.BattleParticipants)
                                {
                                    if (slowboi.TurnMeterIncreaseOnClockTick > bp.TurnMeterIncreaseOnClockTick)
                                    {
                                        slowboi = bp;
                                    }
                                }

                                slowboi.GetAttacked(action.AttackCount);

                                if (champ.TurnCount > LastKillableTurn && !slowboi.ActiveBuffs.ContainsKey(Constants.Buff.Unkillable))
                                {
                                    // run failed
                                    enqueueNewState = !failOnKill;
                                    //Console.WriteLine("!!!!!!!!!! {0} attacked but not unkillable !!!!!!!!", slowboi.Name);
                                }

                                if (action.DebuffsToApply != null)
                                {
                                    slowboi.ApplyDebuff(action.DebuffsToApply.First());
                                }

                                if (slowboi.ActiveBuffs.ContainsKey(Constants.Buff.Counterattack) &&
                                    !slowboi.ActiveDebuffs.ContainsKey(Constants.Debuff.Stun))
                                {
                                    counterAttackers.Add(slowboi);
                                }
                            }

                            foreach (IBattleParticipant bp in counterAttackers)
                            {
                                bp.TakeTurn(bp.GetA1());
                                // Track this!
                            }

                            if (champ.TurnCount == MaxClanBossTurns)
                            {
                                // End of the run!
                                enqueueNewState = false;
                                returnResults = true;
                            }

                            if (enqueueNewState)
                            {
                                battleStates.Enqueue(state);
                            }
                        }

                        List<ClanBossBattleResult.BattleParticipantStats> bpStats = new List<ClanBossBattleResult.BattleParticipantStats>();
                        foreach (IBattleParticipant bp in state.BattleParticipants)
                        {
                            ClanBossBattleResult.BattleParticipantStats bpStat = new ClanBossBattleResult.BattleParticipantStats(bp.Name, bp.IsClanBoss, bp.TurnMeter, new Dictionary<Constants.Buff, int>(bp.ActiveBuffs));
                            bpStats.Add(bpStat);
                        }

                        ClanBossBattleResult.Attack attackDetails = new ClanBossBattleResult.Attack(champ.Name, champ.TurnCount, skill.Id, skill.Name, nextAISkill.Id);
                        List<ClanBossBattleResult.Attack> counterattacks = new List<ClanBossBattleResult.Attack>();
                        foreach (IBattleParticipant bp in counterAttackers)
                        {
                            counterattacks.Add(new ClanBossBattleResult.Attack(bp.Name, bp.TurnCount, Constants.SkillId.A1, bp.GetA1().Name, Constants.SkillId.A1));
                        }

                        ClanBossBattleResult result = new ClanBossBattleResult(state.BattleParticipants.First(p => p.IsClanBoss).TurnCount, attackDetails, bpStats, counterattacks);
                        state.Results.Add(result);

                        if (returnResults)
                        {
                            yield return state.Results;
                        }
                    }
                }
            }
        }

        private void PrintTurnMeters(UCBBState state)
        {
            foreach (IBattleParticipant bp in state.BattleParticipants)
            {
                Console.WriteLine("  {0} turn meter {1}", bp.Name, bp.TurnMeter);
            }
        }
    }
}
