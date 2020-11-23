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
        public delegate IBattleParticipant StunTargetExtractor(List<IBattleParticipant> bps);

        private static IBattleParticipant FindSlowBoi(List<IBattleParticipant> bps)
        {
            IBattleParticipant slowboi = bps.First();
            foreach (IBattleParticipant bp in bps)
            {
                if (slowboi.TurnMeterIncreaseOnClockTick > bp.TurnMeterIncreaseOnClockTick)
                {
                    slowboi = bp;
                }
            }

            return slowboi;
        }

        private class CBBState
        {
            public List<IBattleParticipant> BattleParticipants { get; private set; }
            public List<ClanBossBattleResult> Results { get; private set; }

            public CBBState(List<ChampionInBattle> champions, ClanBossInBattle clanBoss)
            {
                this.BattleParticipants = new List<IBattleParticipant>();
                foreach (ChampionInBattle cib in champions)
                {
                    this.BattleParticipants.Add(cib);
                }
                this.BattleParticipants.Add(clanBoss);

                this.Results = new List<ClanBossBattleResult>();
            }

            public CBBState(CBBState other)
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
        private CBBState initialState;

        public StunTargetExtractor GetStunTarget { get; set; }

        public ClanBossBattle(ClanBoss.Level level, List<ChampionInBattle> championsInBattle)
        {
            ClanBossInBattle clanBoss = new ClanBossInBattle(ClanBoss.Get(level));
            this.initialState = new CBBState(new List<ChampionInBattle>(championsInBattle), clanBoss);
            this.GetStunTarget = FindSlowBoi;
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
            Queue<CBBState> battleStates = new Queue<CBBState>();
            battleStates.Enqueue(this.initialState);

            // While the queue isn't empty
            // run all possible next turns for the head of the queue state and enqueue those states
            // If someone is killed after the last unkillable turn, the run fails (no more enqueues)
            // If clan boss has turn 50, the run succeeds (return the result)
            while (battleStates.Count > 0)
            {
                CBBState state = battleStates.Dequeue();
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

                    foreach (Skill passive in maxTMChamp.GetPassiveSkills())
                    {
                        if (passive.TurnAction.EffectsToApply != null)
                        {
                            foreach (EffectToApply effect in passive.TurnAction.EffectsToApply)
                            {
                                if (effect.WhenToApply == Constants.TimeInTurn.Beginning)
                                {
                                    if (effect.Target == Constants.Target.Self)
                                    {
                                        maxTMChamp.ApplyEffect(effect.Effect);
                                    }
                                    else if (effect.Target == Constants.Target.AllAllies)
                                    {
                                        foreach (IBattleParticipant bp in state.BattleParticipants.Where(p => !p.IsClanBoss && p != maxTMChamp))
                                        {
                                            bp.ApplyEffect(effect.Effect);
                                        }
                                    }
                                }
                            }
                        }
                    }

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

                    CBBState currentState = state;
                    foreach (Skill skill in skillsToRun)
                    {
                        state = new CBBState(currentState);
                        IBattleParticipant champ = state.BattleParticipants.Where(bp => bp.Name == maxTMChamp.Name).First();
                        List<ClanBossBattleResult.Attack> additionalAttacks = new List<ClanBossBattleResult.Attack>();

                        champ.TakeTurn(skill);

                        if (!champ.IsClanBoss)
                        {
                            TurnAction action = skill.TurnAction;
                            additionalAttacks = ApplyTurnActions(
                                skill,
                                champ, 
                                state.BattleParticipants.Where(p => !p.IsClanBoss && p != champ),
                                state.BattleParticipants.Where(p => !p.IsClanBoss),
                                state.BattleParticipants.Where(p => p.IsClanBoss),
                                state.BattleParticipants.Where(p => p.IsClanBoss).First()
                                );

                            battleStates.Enqueue(state);
                        }
                        else
                        {
                            // Clan boss turn!
                            TurnAction action = skill.TurnAction;
                            bool enqueueNewState = true;
                            List<IBattleParticipant> additionalAttackers = new List<IBattleParticipant>();

                            if (action.AttackTarget == Constants.Target.AllEnemies)
                            {
                                foreach (IBattleParticipant bp in state.BattleParticipants.Where(p => !p.IsClanBoss))
                                {
                                    bp.GetAttacked(action.AttackCount);
                                    
                                    if (bp.ActiveBuffs.ContainsKey(Constants.Buff.Counterattack) &&
                                        !bp.ActiveDebuffs.ContainsKey(Constants.Debuff.Stun))
                                    {
                                        additionalAttackers.Add(bp);
                                    }

                                    if (champ.TurnCount > LastKillableTurn && !bp.ActiveBuffs.ContainsKey(Constants.Buff.Unkillable))
                                    {
                                        enqueueNewState = !failOnKill;
                                    }
                                }
                            }
                            else if (action.AttackTarget == Constants.Target.OneEnemy)
                            {
                                IBattleParticipant stunTarget = this.GetStunTarget(state.BattleParticipants);

                                stunTarget.GetAttacked(action.AttackCount);

                                if (champ.TurnCount > LastKillableTurn && !stunTarget.ActiveBuffs.ContainsKey(Constants.Buff.Unkillable))
                                {
                                    enqueueNewState = !failOnKill;
                                }

                                if (action.DebuffsToApply != null)
                                {
                                    stunTarget.ApplyDebuff(action.DebuffsToApply.First());
                                }

                                if (stunTarget.ActiveBuffs.ContainsKey(Constants.Buff.Counterattack) &&
                                    !stunTarget.ActiveDebuffs.ContainsKey(Constants.Debuff.Stun))
                                {
                                    additionalAttackers.Add(stunTarget);
                                }
                            }

                            foreach (IBattleParticipant bp in additionalAttackers)
                            {
                                bp.AdditionalAttack();
                                additionalAttacks.Add(new ClanBossBattleResult.Attack(bp.Name, bp.TurnCount, bp.TurnMeter, Constants.SkillId.A1, bp.GetA1().Name, Constants.SkillId.A1));
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
                            ClanBossBattleResult.BattleParticipantStats bpStat = new ClanBossBattleResult.BattleParticipantStats(bp.Name, bp.IsClanBoss, bp.TurnMeter, new Dictionary<Constants.Buff, int>(bp.ActiveBuffs), bp.GetSkillToCooldownMap());
                            bpStats.Add(bpStat);
                        }

                        ClanBossBattleResult.Attack attackDetails = new ClanBossBattleResult.Attack(champ.Name, champ.TurnCount, maxTurnMeter, skill.Id, skill.Name, nextAISkill.Id);
                        ClanBossBattleResult result = new ClanBossBattleResult(state.BattleParticipants.First(p => p.IsClanBoss).TurnCount, attackDetails, bpStats, additionalAttacks);
                        state.Results.Add(result);

                        if (returnResults)
                        {
                            yield return state.Results;
                        }
                    }
                }
            }
        }

        private static List<ClanBossBattleResult.Attack> ApplyTurnActions(
            Skill skill, 
            IBattleParticipant active, 
            IEnumerable<IBattleParticipant> allies, 
            IEnumerable<IBattleParticipant> fullTeam,
            IEnumerable<IBattleParticipant> enemies,
            IBattleParticipant singleTargetEnemy)
        {
            List<ClanBossBattleResult.Attack> attacks = new List<ClanBossBattleResult.Attack>();

            TurnAction action = skill.TurnAction;
            if (action.BuffsToApply != null)
            {
                foreach (BuffToApply buff in action.BuffsToApply)
                {
                    if (buff.Target == Constants.Target.Self)
                    {
                        active.ApplyBuff(buff);
                    }
                    else if (buff.Target == Constants.Target.AllAllies)
                    {
                        foreach (IBattleParticipant bp in allies)
                        {
                            bp.ApplyBuff(buff);
                        }
                    }
                    else if (buff.Target == Constants.Target.FullTeam)
                    {
                        foreach (IBattleParticipant bp in fullTeam)
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
                    if (effect.Effect == Constants.Effect.AllyAttack)
                    {
                        foreach (IBattleParticipant bp in allies)
                        {
                            if (effect.Target == Constants.Target.ThreeRandomAllies && bp.LeaveOutOfAllyAttack)
                            {
                                continue;
                            }

                            bp.AdditionalAttack();
                            Skill a1 = bp.GetA1();
                            attacks.Add(new ClanBossBattleResult.Attack(bp.Name, bp.TurnCount, bp.TurnMeter, a1.Id, a1.Name, Constants.SkillId.A1));
                            attacks.AddRange(ApplyTurnActions(
                                a1,
                                bp,
                                fullTeam.Where(p => p != bp),
                                fullTeam,
                                enemies,
                                singleTargetEnemy));
                        }
                    }
                    else
                    {
                        if (effect.Target == Constants.Target.Self)
                        {
                            active.ApplyEffect(effect.Effect);
                        }
                        else if (effect.Target == Constants.Target.AllAllies)
                        {
                            foreach (IBattleParticipant bp in allies)
                            {
                                bp.ApplyEffect(effect.Effect);
                            }
                        }
                    }
                }
            }

            foreach (Skill passive in active.GetPassiveSkills())
            {
                if (passive.TurnAction.EffectsToApply != null)
                {
                    foreach (EffectToApply effect in passive.TurnAction.EffectsToApply)
                    {
                        if (effect.WhenToApply == Constants.TimeInTurn.End)
                        {
                            if (effect.Target == Constants.Target.Self)
                            {
                                active.ApplyEffect(effect.Effect);
                            }
                            else if (effect.Target == Constants.Target.AllAllies)
                            {
                                foreach (IBattleParticipant bp in allies)
                                {
                                    bp.ApplyEffect(effect.Effect);
                                }
                            }
                        }
                    }
                }
            }

            return attacks;
        }

        private void PrintTurnMeters(CBBState state)
        {
            foreach (IBattleParticipant bp in state.BattleParticipants)
            {
                Console.WriteLine("  {0} turn meter {1}", bp.Name, bp.TurnMeter);
            }
        }
    }
}
