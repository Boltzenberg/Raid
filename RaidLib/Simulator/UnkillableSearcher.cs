using RaidLib.DataModel;
using RaidLib.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaidLib.Simulator
{
    public class UnkillableSearcher
    {
        private class Delta
        {
            public int Speed { get; private set; }
            public int SpeedSets { get; private set; }
            public int PerceptionSets { get; private set; }

            public Delta(int speed, int speedSets, int perceptionSets)
            {
                this.Speed = speed;
                this.SpeedSets = speedSets;
                this.PerceptionSets = perceptionSets;
            }
        }

        private List<Delta> deltasToApply;
        private List<Champion> champions;
        private Dictionary<string, Tuple<List<Constants.SkillId>, List<Constants.SkillId>>> skillPoliciesByChampionName;

        public UnkillableSearcher(Dictionary<Champion, Tuple<List<Constants.SkillId>, List<Constants.SkillId>>> skillPoliciesByChampionBase)
        {
            this.champions = new List<Champion>(skillPoliciesByChampionBase.Keys);
            this.skillPoliciesByChampionName = new Dictionary<string, Tuple<List<Constants.SkillId>, List<Constants.SkillId>>>();
            foreach (Champion c in this.champions)
            {
                this.skillPoliciesByChampionName[c.Name] = skillPoliciesByChampionBase[c];
            }

            this.deltasToApply = new List<Delta>();
            for (int flip = -1, speedDelta = 0, i = 0; i <=20; i++)
            {
                flip = flip * -1;
                speedDelta += (flip * i);
                for (int speedSets = 0; speedSets <= 3; speedSets++)
                {
                    for (int perceptionSets = 0; perceptionSets <= 3 - speedSets; perceptionSets++)
                    {
                        deltasToApply.Add(new Delta(speedDelta, speedSets, perceptionSets));
                    }
                }
            }
        }

        public IEnumerable<List<Champion>> Search(ClanBoss.Level level)
        {
            foreach (List<Champion> cs in this.GetChampionLists())
            {
                Dictionary<Champion, Tuple<List<Constants.SkillId>, List<Constants.SkillId>>> skillPoliciesByChampion = new Dictionary<Champion, Tuple<List<Constants.SkillId>, List<Constants.SkillId>>>();
                foreach (Champion c in cs)
                {
                    skillPoliciesByChampion[c] = this.skillPoliciesByChampionName[c.Name];
                }

                ClanBossBattle battle = new ClanBossBattle(level, skillPoliciesByChampion);
                List<ClanBossBattleResult> results = battle.Run();
                int lastKillableTurn = ClanBossBattleResultsAnalysis.LastClanBossTurnThatHitKillableChampion(results, Utils.FindSlowestChampion(cs));
                if (lastKillableTurn < 10)
                {
                    yield return cs;
                }
            }
        }

        public IEnumerable<List<Champion>> GetChampionLists()
        {
            return this.GetChampionLists(new List<Champion>(this.champions));
        }

        private IEnumerable<List<Champion>> GetChampionLists(List<Champion> src)
        {
            if (src.Count == 0)
            {
                yield return new List<Champion>();
                yield break;
            }

            Champion c = src[0];
            src.RemoveAt(0);
            foreach (List<Champion> result in this.GetChampionLists(src))
            {
                // Apply every version of c
                foreach (Delta delta in this.deltasToApply)
                {
                    Champion clone = c.Clone(delta.Speed, delta.SpeedSets, delta.PerceptionSets);
                    if (clone.EffectiveSpeed < clone.BaseSpeed)
                    {
                        continue;
                    }

                    result.Add(clone);
                    yield return result;
                    result.Remove(clone);
                }
            }
        }
    }
}
