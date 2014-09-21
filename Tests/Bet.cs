using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    public class Bet
    {
        private Player owner;

        public Bet(Player player)
        {
            owner = player;
        }

        public Player GetOwner()
        {
            return owner;
        }
    }
}
