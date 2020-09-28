using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaidLib.Simulator
{
    public class Clock
    {
        public delegate void TickHandler(object sender);

        public Clock()
        { }

        public event TickHandler OnTick;

        public void Tick()
        {
            this.OnTick(this);
        }
    }
}
