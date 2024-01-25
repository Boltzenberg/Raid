using RaidBattleSimulator;
using RaidBattleSimulator.DataModel.Champions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScratchUI
{
    public partial class Form1 : Form
    {
        private Battle battle = null;

        public Form1()
        {
            InitializeComponent();

            List<ChampionBase> team = new List<ChampionBase>();
            team.Add(Dracomorph.Create(219, 0, 1, 0.0d));
            team.Add(Seeker.Create(248, 0, 0, 0.0d));
            team.Add(Maneater.Create("Fast Maneater", 265, 1, 0, 0.0d, new int[] { 0, 0, 0 }));
            team.Add(Maneater.Create("Slow Maneater", 239, 0, 0, 0.0d, new int[] { 0, 0, 1 }));
            team.Add(Painkeeper.Create(241, 0, 0, 0.0d));

            List<ChampionBase> enemies = new List<ChampionBase>();
            enemies.Add(DemonLord.CreateUltraNightmare(false));

            this.battle = new Battle(team, enemies);
        }

        private void OnLoad(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            StringBuilder sbJustTurns = new StringBuilder();
            sb.Append("<HTML><BODY><TABLE BORDER=1><TR><TH>Note</TH>");
            sbJustTurns.Append("<TABLE BORDER=1><TR><TH>Note</TH><TH>Champion</TH></TR>");

            foreach (ChampionBase champ in this.battle.AllChampions)
            {
                sb.AppendFormat("<TH>{0}</TH>", champ.Name);
            }

            sb.AppendLine("</TR>");

            int clanBossTurn = 0;
            while (clanBossTurn < 50)
            {
                TickResult result = battle.Tick();
                sb.Append("<TR><TD>Turn Meters Before Tick</TD>");
                foreach (ChampionBase champ in this.battle.AllChampions)
                {
                    sb.AppendFormat("<TD>{0}</TD>", result.TurnMetersBeforeTick[champ]);
                }
                sb.AppendLine("</TR>");
                sb.Append("<TR><TD>Turn Meters After Tick</TD>");
                foreach (ChampionBase champ in this.battle.AllChampions)
                {
                    sb.AppendFormat("<TD>{0}</TD>", result.TurnMetersAfterTick[champ]);
                }
                sb.AppendLine("</TR>");
                if (result.ChampionThatTookATurn != null)
                {
                    foreach (TurnResult turnResult in result.TurnResults)
                    {
                        sbJustTurns.AppendFormat("<TR><TD>CB Turn {0}</TD><TD>{1} {2}</TD></TR>", clanBossTurn, result.ChampionThatTookATurn.Name, turnResult.SkillUsed);
                        sb.AppendFormat("<TR><TD>CB Turn {0}</TD>", clanBossTurn);
                        ListViewItem item = new ListViewItem();
                        foreach (ChampionBase champ in this.battle.Team)
                        {
                            sb.AppendFormat("<TD{0}>{1}</TD>", champ == result.ChampionThatTookATurn ? " bgcolor='green'" : string.Empty, turnResult.TurnMetersBeforeTurn[champ]);
                        }

                        foreach (ChampionBase enemy in this.battle.Enemies)
                        {
                            sb.AppendFormat("<TD{0}>{1}</TD>", enemy == result.ChampionThatTookATurn ? " bgcolor='green'" : string.Empty, turnResult.TurnMetersBeforeTurn[enemy]);
                        }
                        sb.AppendLine("</TR>");
                    }
                    if (result.ChampionThatTookATurn == battle.Enemies.First())
                    {
                        clanBossTurn++;
                    }
                }
            }
            sbJustTurns.Append("</TABLE>");
            sb.AppendFormat("</TABLE>{0}</BODY></HTML>", sbJustTurns);
            this.wb.DocumentText = sb.ToString();
        }
    }
}
