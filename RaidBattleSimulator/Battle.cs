using RaidBattleSimulator.DataModel;
using RaidBattleSimulator.DataModel.Champions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaidBattleSimulator
{
    public class Battle
    {
        public List<ChampionBase> Team { get; private set; }
        public List<ChampionBase> Enemies { get; private set; }

        private Dictionary<ChampionBase, double> championToCurrenTurnMeter = new Dictionary<ChampionBase, double>();

        public Battle(List<ChampionBase> team, List<ChampionBase> enemies)
        {
            this.Team = team;
            this.Enemies = enemies;

            foreach (var champion in team)
            {
                this.championToCurrenTurnMeter[champion] = 0.0d;
            }

            foreach (var enemy in enemies)
            {
                this.championToCurrenTurnMeter[enemy] = 0.0d;
            }
        }

        public IEnumerable<ChampionBase> AllChampions
        {
            get
            {
                return this.Team.Union(this.Enemies);
            }
        }

        public Dictionary<ChampionBase, double> GetCurrentTurnMeters()
        {
            return new Dictionary<ChampionBase, double>(this.championToCurrenTurnMeter);
        }

        public void UpdateTurnMeter(ChampionBase champ, double tmUpdate)
        {
            this.championToCurrenTurnMeter[champ] += tmUpdate;
        }

        public TickResult Tick()
        {
            Dictionary<ChampionBase, double> turnMetersBeforeTick = this.GetCurrentTurnMeters();

            foreach (var champion in this.AllChampions)
            {
                this.championToCurrenTurnMeter[champion] += champion.GetTurnMeterDeltaForTick(1.0d);
            }

            Dictionary<ChampionBase, double> turnMetersAfterTick = this.GetCurrentTurnMeters();

            ChampionBase championWithMaxTurnMeter = this.Team.First();
            foreach (var champion in this.Team)
            {
                if (this.championToCurrenTurnMeter[championWithMaxTurnMeter] < this.championToCurrenTurnMeter[champion])
                {
                    championWithMaxTurnMeter = champion;
                }
            }

            foreach (var enemy in this.Enemies)
            {
                if (this.championToCurrenTurnMeter[championWithMaxTurnMeter] < this.championToCurrenTurnMeter[enemy])
                {
                    championWithMaxTurnMeter = enemy;
                }
            }

            ChampionBase championThatTookATurn = null;
            List<TurnResult> turnResults = null;
            
            if (this.championToCurrenTurnMeter[championWithMaxTurnMeter] >= 1.0d)
            {
                championThatTookATurn = championWithMaxTurnMeter;

                TurnResult turnResult = null;
                turnResults = new List<TurnResult>();
                do
                {
                    turnResult = championThatTookATurn.TakeTurn(this);
                    this.championToCurrenTurnMeter[championThatTookATurn] = 0.0d;
                    turnResults.Add(turnResult);
                } while (turnResult.GrantedExtraTurn);
            }

            return new TickResult(turnMetersBeforeTick, turnMetersAfterTick, championThatTookATurn, turnResults);
        }
    }
}
