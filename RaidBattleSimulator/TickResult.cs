using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaidBattleSimulator
{
    public class TickResult
    {
        public ChampionBase ChampionThatTookATurn { get; private set; }
        public IReadOnlyDictionary<ChampionBase, double> TurnMeters { get; private set; }

        public TickResult(ChampionBase championThatTookATurn, Dictionary<ChampionBase, double> turnMeters)
        {
            this.ChampionThatTookATurn = championThatTookATurn;
            this.TurnMeters = new Dictionary<ChampionBase, double>(turnMeters);
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder("Champion that took a turn: ");
            if (this.ChampionThatTookATurn != null)
            {
                sb.Append(this.ChampionThatTookATurn.Name);
            }

            sb.Append("  ");
            foreach (var champion in this.TurnMeters.Keys)
            {
                sb.Append(champion.Name);
                sb.Append(" ");
                sb.Append(this.TurnMeters[champion]);
                sb.Append(", ");
            }
            sb.Length -= 2;

            return sb.ToString();
        }
    }
}
