using RaidBattleSimulator;
using RaidBattleSimulator.DataModel.Champions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Scratch
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<ChampionBase> team = new List<ChampionBase>();
            team.Add(Dracomorph.Create(219, 0, 1, 0.0d));
            team.Add(Seeker.Create(248, 0, 0, 0.0d));
            team.Add(Maneater.Create("Fast Maneater", 265, 1, 0, 0.0d, new int[] { 0, 0, 0 }));
            team.Add(Maneater.Create("Slow Maneater", 239, 0, 0, 0.0d, new int[] { 0, 0, 1 }));
            team.Add(Painkeeper.Create(241, 0, 0, 0.0d));

            List<ChampionBase> enemies = new List<ChampionBase>();
            enemies.Add(DemonLord.CreateUltraNightmare(false));

            Battle battle = new Battle(team, enemies);

            int clanBossTurn = 0;
            while (clanBossTurn < 50)
            {
                TickResult result = battle.Tick();
                if (result.ChampionThatTookATurn != null)
                {
                    //Console.WriteLine(result);
                    foreach (TurnResult turnResult in result.TurnResults)
                    {
                        Console.WriteLine("{0} ({1})", result.ChampionThatTookATurn.Name, turnResult.SkillUsed);
                    }
                    if (result.ChampionThatTookATurn == enemies.First())
                    {
                        clanBossTurn++;
                        Console.WriteLine();
                    }
                }
            }

            Console.ReadLine();
        }
    }
}
