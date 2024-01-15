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

        private IEnumerable<ChampionBase> AllChampions
        {
            get
            {
                return this.Team.Union(this.Enemies);
            }
        }

        public TickResult Tick()
        {
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
            if (this.championToCurrenTurnMeter[championWithMaxTurnMeter] >= 1.0d)
            {
                championThatTookATurn = championWithMaxTurnMeter;
                this.championToCurrenTurnMeter[championThatTookATurn] = championThatTookATurn.GetTurnMeterDeltaForTick(1.0d, 0.0d);
            }

            foreach (var champion in this.AllChampions)
            {
                if (champion != championThatTookATurn)
                {
                    this.championToCurrenTurnMeter[champion] += champion.GetTurnMeterDeltaForTick(1.0d, 0.0d);
                }
            }

            return new TickResult(championThatTookATurn, this.championToCurrenTurnMeter);
        }
    }
}
