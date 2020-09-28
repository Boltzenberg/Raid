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

        public void Run()
        {
            int clanBossTurn = 0;
            while (clanBossTurn < MaxClanBossTurns)
            {
                this.clock.Tick();
                this.champions.Sort((a, b) => b.TurnMeter.CompareTo(a.TurnMeter));
                
                ChampionInBattle maxTMChamp = this.champions.First();
                if (maxTMChamp.TurnMeter > this.clanBoss.TurnMeter)
                {
                    if (maxTMChamp.TurnMeter >= Constants.TurnMeter.Full)
                    {
                        //this.PrintTurnMeters();
                        Constants.Effect effect = maxTMChamp.TakeTurn();
                        if (Constants.Effects.ImpactsCurrentChamp.Contains(effect))
                        {
                            maxTMChamp.ApplyEffect(effect);
                        }
                        else if (Constants.Effects.ImpactsAllies.Contains(effect))
                        {
                            foreach (ChampionInBattle cib in this.champions)
                            {
                                if (cib != maxTMChamp)
                                {
                                    cib.ApplyEffect(effect);
                                }
                            }
                        }
                        else if (Constants.Effects.ImpactsTeam.Contains(effect))
                        {
                            foreach (ChampionInBattle cib in this.champions)
                            {
                                cib.ApplyEffect(effect);
                            }
                        }
                    }
                }
                else
                {
                    if (this.clanBoss.TurnMeter >= Constants.TurnMeter.Full)
                    {
                        //this.PrintTurnMeters();
                        this.clanBoss.TakeTurn();
                        clanBossTurn++;
                    }
                }
            }
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
