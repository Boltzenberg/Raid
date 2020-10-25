using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RaidLib.DataModel;
using RaidLib.DataModel.Champions;
using RaidLib.Simulator;

namespace RaidLibTests.Simulator
{
    [TestClass]
    public class ClanBossBattleTest
    {
        private static Champion CreateChampion(string name, int uiSpeed)
        {
            return new Champion(name, 0, uiSpeed, 0, new List<Skill>() { new Skill("A1", Constants.SkillId.A1, 0, TurnAction.AttackOneEnemy()) });
        }

        private static Dictionary<Champion, Tuple<List<Constants.SkillId>, List<Constants.SkillId>>> GetChampions()
        {
            Dictionary<Champion, Tuple<List<Constants.SkillId>, List<Constants.SkillId>>> results = new Dictionary<Champion, Tuple<List<Constants.SkillId>, List<Constants.SkillId>>>();
            results[Maneater.Create(227, 2)] = new Tuple<List<Constants.SkillId>, List<Constants.SkillId>>(Maneater.AISkills, new List<Constants.SkillId>());
            results[Painkeeper.Create(213, 2)] = new Tuple<List<Constants.SkillId>, List<Constants.SkillId>>(Painkeeper.AISkills, new List<Constants.SkillId>() { Constants.SkillId.A3, Constants.SkillId.A1 });
            results[CreateChampion("DPS1", 159)] = new Tuple<List<Constants.SkillId>, List<Constants.SkillId>>(new List<Constants.SkillId>() { Constants.SkillId.A1 }, new List<Constants.SkillId>());
            results[CreateChampion("DPS2", 159)] = new Tuple<List<Constants.SkillId>, List<Constants.SkillId>>(new List<Constants.SkillId>() { Constants.SkillId.A1 }, new List<Constants.SkillId>());
            results[CreateChampion("Slowboi", 106)] = new Tuple<List<Constants.SkillId>, List<Constants.SkillId>>(new List<Constants.SkillId>() { Constants.SkillId.A1 }, new List<Constants.SkillId>());
            return results;
        }

        [TestMethod]
        public void TestBrutalMatchesDeadwoodJedi()
        {
            List<Tuple<string, Constants.SkillId>> expected = new List<Tuple<string, Constants.SkillId>>()
            {
                // 0
                new Tuple<string, Constants.SkillId>("Maneater", Constants.SkillId.A3),
                new Tuple<string, Constants.SkillId>("Painkeeper", Constants.SkillId.A3),
                new Tuple<string, Constants.SkillId>("Clan Boss", Constants.SkillId.A1), // 1
                // 3
                new Tuple<string, Constants.SkillId>("DPS1", Constants.SkillId.A1),
                new Tuple<string, Constants.SkillId>("DPS2", Constants.SkillId.A1),
                new Tuple<string, Constants.SkillId>("Maneater", Constants.SkillId.A2),
                new Tuple<string, Constants.SkillId>("Slowboi", Constants.SkillId.A1),
                new Tuple<string, Constants.SkillId>("Painkeeper", Constants.SkillId.A1),
                new Tuple<string, Constants.SkillId>("Clan Boss", Constants.SkillId.A2), // 2
                // 9
                new Tuple<string, Constants.SkillId>("DPS1", Constants.SkillId.A1),
                new Tuple<string, Constants.SkillId>("DPS2", Constants.SkillId.A1),
                new Tuple<string, Constants.SkillId>("Maneater", Constants.SkillId.A1),
                new Tuple<string, Constants.SkillId>("Painkeeper", Constants.SkillId.A2),
                new Tuple<string, Constants.SkillId>("Clan Boss", Constants.SkillId.A3), // 3
                // 14
                new Tuple<string, Constants.SkillId>("Maneater", Constants.SkillId.A1),
                new Tuple<string, Constants.SkillId>("DPS1", Constants.SkillId.A1),
                new Tuple<string, Constants.SkillId>("DPS2", Constants.SkillId.A1),
                new Tuple<string, Constants.SkillId>("Painkeeper", Constants.SkillId.A1),
                new Tuple<string, Constants.SkillId>("Slowboi", Constants.SkillId.A1),
                new Tuple<string, Constants.SkillId>("Maneater", Constants.SkillId.A3),
                new Tuple<string, Constants.SkillId>("Clan Boss", Constants.SkillId.A1), // 4
                // 21
                new Tuple<string, Constants.SkillId>("Painkeeper", Constants.SkillId.A3),
                new Tuple<string, Constants.SkillId>("DPS1", Constants.SkillId.A1),
                new Tuple<string, Constants.SkillId>("DPS2", Constants.SkillId.A1),
                new Tuple<string, Constants.SkillId>("Maneater", Constants.SkillId.A2),
                new Tuple<string, Constants.SkillId>("Painkeeper", Constants.SkillId.A1),
                new Tuple<string, Constants.SkillId>("Clan Boss", Constants.SkillId.A2), // 5
                // 27
                new Tuple<string, Constants.SkillId>("Slowboi", Constants.SkillId.A1),
                new Tuple<string, Constants.SkillId>("DPS1", Constants.SkillId.A1),
                new Tuple<string, Constants.SkillId>("Maneater", Constants.SkillId.A1),
                new Tuple<string, Constants.SkillId>("DPS2", Constants.SkillId.A1),
                new Tuple<string, Constants.SkillId>("Painkeeper", Constants.SkillId.A2),
                new Tuple<string, Constants.SkillId>("Clan Boss", Constants.SkillId.A3), // 6
                // 33
                new Tuple<string, Constants.SkillId>("Maneater", Constants.SkillId.A1),
                new Tuple<string, Constants.SkillId>("DPS1", Constants.SkillId.A1),
                new Tuple<string, Constants.SkillId>("Painkeeper", Constants.SkillId.A1),
                new Tuple<string, Constants.SkillId>("DPS2", Constants.SkillId.A1),
                new Tuple<string, Constants.SkillId>("Slowboi", Constants.SkillId.A1),
                new Tuple<string, Constants.SkillId>("Maneater", Constants.SkillId.A3),
                new Tuple<string, Constants.SkillId>("Clan Boss", Constants.SkillId.A1), // 7
                // 40
            };

            ClanBossBattle battle = new ClanBossBattle(ClanBoss.Level.Brutal, GetChampions());
            List<ClanBossBattleResult> results = battle.Run();
            List<ClanBossBattleResult> actual = results.Where(r => !string.IsNullOrEmpty(r.AttackDetails.ActorName)).ToList();

            Assert.IsTrue(actual.Count > expected.Count);
            for (int i = 0; i < expected.Count; i++)
            {
                Assert.AreEqual(expected[i].Item1, actual[i].AttackDetails.ActorName, "Wrong champion went in result " + i);
                Assert.AreEqual(expected[i].Item2, actual[i].AttackDetails.Skill, "Wrong skill used by champion " + expected[i].Item1 + " in results " + i);
            }
        }

        [TestMethod]
        public void TestNightmareMatchesDeadwoodJedi()
        {
            List<Tuple<string, Constants.SkillId>> expected = new List<Tuple<string, Constants.SkillId>>()
            {
                // 0
                new Tuple<string, Constants.SkillId>("Maneater", Constants.SkillId.A3),
                new Tuple<string, Constants.SkillId>("Painkeeper", Constants.SkillId.A3),
                new Tuple<string, Constants.SkillId>("Clan Boss", Constants.SkillId.A1), // 1
                // 3
                new Tuple<string, Constants.SkillId>("DPS1", Constants.SkillId.A1),
                new Tuple<string, Constants.SkillId>("DPS2", Constants.SkillId.A1),
                new Tuple<string, Constants.SkillId>("Maneater", Constants.SkillId.A2),
                new Tuple<string, Constants.SkillId>("Slowboi", Constants.SkillId.A1),
                new Tuple<string, Constants.SkillId>("Painkeeper", Constants.SkillId.A1),
                new Tuple<string, Constants.SkillId>("Clan Boss", Constants.SkillId.A2), // 2
                // 9
                new Tuple<string, Constants.SkillId>("DPS1", Constants.SkillId.A1),
                new Tuple<string, Constants.SkillId>("DPS2", Constants.SkillId.A1),
                new Tuple<string, Constants.SkillId>("Maneater", Constants.SkillId.A1),
                new Tuple<string, Constants.SkillId>("Painkeeper", Constants.SkillId.A2),
                new Tuple<string, Constants.SkillId>("Clan Boss", Constants.SkillId.A3), // 3
                // 14
                new Tuple<string, Constants.SkillId>("Maneater", Constants.SkillId.A1),
                new Tuple<string, Constants.SkillId>("DPS1", Constants.SkillId.A1),
                new Tuple<string, Constants.SkillId>("DPS2", Constants.SkillId.A1),
                new Tuple<string, Constants.SkillId>("Painkeeper", Constants.SkillId.A1),
                new Tuple<string, Constants.SkillId>("Slowboi", Constants.SkillId.A1),
                new Tuple<string, Constants.SkillId>("Maneater", Constants.SkillId.A3),
                new Tuple<string, Constants.SkillId>("Clan Boss", Constants.SkillId.A1), // 4
                // 21
                new Tuple<string, Constants.SkillId>("Painkeeper", Constants.SkillId.A3),
                new Tuple<string, Constants.SkillId>("DPS1", Constants.SkillId.A1),
                new Tuple<string, Constants.SkillId>("DPS2", Constants.SkillId.A1),
                new Tuple<string, Constants.SkillId>("Maneater", Constants.SkillId.A2),
                new Tuple<string, Constants.SkillId>("Clan Boss", Constants.SkillId.A2), // 5
                // 26
                new Tuple<string, Constants.SkillId>("Painkeeper", Constants.SkillId.A1),
                new Tuple<string, Constants.SkillId>("Slowboi", Constants.SkillId.A1),
                new Tuple<string, Constants.SkillId>("DPS1", Constants.SkillId.A1),
                new Tuple<string, Constants.SkillId>("Maneater", Constants.SkillId.A1),
                new Tuple<string, Constants.SkillId>("DPS2", Constants.SkillId.A1),
                new Tuple<string, Constants.SkillId>("Painkeeper", Constants.SkillId.A2),
                new Tuple<string, Constants.SkillId>("Clan Boss", Constants.SkillId.A3), // 6
                // 33
                new Tuple<string, Constants.SkillId>("Maneater", Constants.SkillId.A1),
                new Tuple<string, Constants.SkillId>("DPS1", Constants.SkillId.A1),
                new Tuple<string, Constants.SkillId>("DPS2", Constants.SkillId.A1),
                new Tuple<string, Constants.SkillId>("Painkeeper", Constants.SkillId.A1),
                new Tuple<string, Constants.SkillId>("Slowboi", Constants.SkillId.A1),
                new Tuple<string, Constants.SkillId>("Maneater", Constants.SkillId.A3),
                new Tuple<string, Constants.SkillId>("Clan Boss", Constants.SkillId.A1), // 7
                // 40
            };

            ClanBossBattle battle = new ClanBossBattle(ClanBoss.Level.Nightmare, GetChampions());
            List<ClanBossBattleResult> results = battle.Run();
            List<ClanBossBattleResult> actual = results.Where(r => !string.IsNullOrEmpty(r.AttackDetails.ActorName)).ToList();

            Assert.IsTrue(actual.Count > expected.Count);
            for (int i = 0; i < expected.Count; i++)
            {
                Assert.AreEqual(expected[i].Item1, actual[i].AttackDetails.ActorName, "Wrong champion went in result " + i);
                Assert.AreEqual(expected[i].Item2, actual[i].AttackDetails.Skill, "Wrong skill used by champion " + expected[i].Item1 + " in results " + i);
            }
        }
    }
}
