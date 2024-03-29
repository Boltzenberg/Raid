﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaidBattleSimulator.DataModel
{
    public class Skill
    {
        public Skill(string name, Constants.SkillId id, int cooldown, TurnAction turnAction, int initialDelay = 0)
        {
            this.Name = name;
            this.Id = id;
            this.Cooldown = cooldown;
            this.TurnAction = turnAction;
            this.InitialDelay = initialDelay;
        }

        public string Name { get; private set; }
        public Constants.SkillId Id { get; private set; }
        public int Cooldown { get; private set; }
        public TurnAction TurnAction { get; private set; }
        public int InitialDelay { get; private set; }
        public bool IsPassive
        {
            get
            {
                return this.Id == Constants.SkillId.P1;
            }
        }
        public bool AllowSkillCooldowns
        {
            get
            {
                return this.Id != Constants.SkillId.Recovery;
            }
        }

        public static Skill StunRecovery
        {
            get
            {
                return new Skill("Stun Recovery", Constants.SkillId.Recovery, 0, new TurnAction(0, Constants.Target.None, null, null, null));
            }
        }
    }
}
