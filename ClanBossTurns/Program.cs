using RaidLib.DataModel;
using RaidLib.Simulator;
using RaidLib.Utilities;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ClanBossTurns
{
    class Program
    {
        static void Main(string[] args)
        {
            ClanBoss.Level clanBossLevel = ClanBoss.Level.UltraNightmare;

            List<Teams.CreateChampion> championCreators = Teams.DeadwoodJedi.ChampionCreators();

            List<Champion> champions = new List<Champion>();
            Dictionary<Champion, List<SkillPolicy>> skillPoliciesByChampion = new Dictionary<Champion, List<SkillPolicy>>();
            foreach (Teams.CreateChampion cc in championCreators)
            {
                Tuple<Champion, List<SkillPolicy>> tuple = cc(clanBossLevel);
                champions.Add(tuple.Item1);
                skillPoliciesByChampion[tuple.Item1] = tuple.Item2;
            }

            ClanBossBattle battle = new ClanBossBattle(clanBossLevel, champions, skillPoliciesByChampion);
            List<ClanBossBattleResult> results = battle.Run();
            Console.WriteLine();
            Console.WriteLine("Run is over!");
            using (StreamWriter csv = new StreamWriter(File.OpenWrite("results.csv")))
            {
                csv.WriteLine("ClockTick,Clan Boss Turn,Actor,SkillId,SkillName,{0} Turn Meter,{1} Turn Meter,{2} Turn Meter,{3} Turn Meter,{4} Turn Meter,Clan Boss Turn Meter", champions[0].Name, champions[1].Name, champions[2].Name, champions[3].Name, champions[4].Name);
                foreach (ClanBossBattleResult result in results)
                {
                    csv.WriteLine("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10}",
                        result.ClockTick,
                        result.ClanBossTurn,
                        result.ActorName,
                        result.Skill != Constants.SkillId.None ? result.Skill.ToString() : string.Empty,
                        result.SkillName,
                        result.Champions.Where(c => c.Champion.Name == champions[0].Name).First().TurnMeter,
                        result.Champions.Where(c => c.Champion.Name == champions[1].Name).First().TurnMeter,
                        result.Champions.Where(c => c.Champion.Name == champions[2].Name).First().TurnMeter,
                        result.Champions.Where(c => c.Champion.Name == champions[3].Name).First().TurnMeter,
                        result.Champions.Where(c => c.Champion.Name == champions[4].Name).First().TurnMeter,
                        result.ClanBoss.TurnMeter);
                }
            }

            int lastKillableTurn = ClanBossBattleResultsAnalysis.LastClanBossTurnThatHitKillableChampion(results, Utils.FindSlowestChampion(champions));
            Console.WriteLine("Last turn where there was a hit on a champion that wasn't unkillable:  {0}", lastKillableTurn);
            Console.ReadLine();
        }
    }
}
