using RaidBattleSimulator;
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
            team.Add(new ChampionBase("Dracomorph", 98, 219, 0, 1, 0.0d));
            //team.Add(new ChampionBase("Seeker", 103, 248, 0, 0, 0.0d));
            team.Add(new ChampionBase("Fast Maneater", 98, 265, 1, 0, 0.0d));
            team.Add(new ChampionBase("Slow Maneater", 98, 239, 0, 0, 0.0d));
            //team.Add(new ChampionBase("Pain Keeper", 102, 241, 0, 0, 0.0d));

            List<ChampionBase> enemies = new List<ChampionBase>();
            enemies.Add(new ChampionBase("Demon Lord", 190, 190, 0, 0, 0.0d));

            Battle battle = new Battle(team, enemies);

            int clanBossTurn = 0;
            while (clanBossTurn < 50)
            {
                TickResult result = battle.Tick();
                if (result.ChampionThatTookATurn != null)
                {
                    Console.WriteLine("{0}: {1}", clanBossTurn, result.ChampionThatTookATurn.Name);
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
