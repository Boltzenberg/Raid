using RaidBattleSimulator.DataModel.Champions;
using System.Collections.Generic;
using System.Text;

namespace RaidBattleSimulator
{
    public class TickResult
    {
        public IReadOnlyDictionary<ChampionBase, double> TurnMetersBeforeTick { get; private set; }
        public IReadOnlyDictionary<ChampionBase, double> TurnMetersAfterTick { get; private set; }
        public ChampionBase ChampionThatTookATurn { get; private set; }
        public List<TurnResult> TurnResults { get; private set; }

        public TickResult(Dictionary<ChampionBase, double> turnMetersBeforeTick, Dictionary<ChampionBase, double> turnMetersAfterTick, ChampionBase championThatTookATurn, List<TurnResult> turnResults)
        {
            this.TurnMetersBeforeTick = turnMetersBeforeTick;
            this.TurnMetersAfterTick = turnMetersAfterTick;
            this.ChampionThatTookATurn = championThatTookATurn;
            this.TurnResults = turnResults;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            if (this.ChampionThatTookATurn != null)
            {
                foreach (TurnResult turnResult in this.TurnResults)
                {
                    sb.AppendFormat("{0} {1}:", this.ChampionThatTookATurn.Name, turnResult.SkillUsed);
                    sb.Append("  ");
                    foreach (var champion in turnResult.TurnMetersBeforeTurn.Keys)
                    {
                        sb.Append(champion.Name);
                        sb.Append(" ");
                        sb.Append(turnResult.TurnMetersBeforeTurn[champion]);
                        sb.Append(", ");
                    }
                    sb.Length -= 2;
                    sb.AppendLine();
                }
                sb.Length -= 2;
            }
            return sb.ToString();
        }
    }
}
