using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    public class Bet
    {
        private Player owner;
        public int DiceValue;
        public int ChipsCount;
        public Guid Id;

        public Bet(Player player, int diceValue, int chipsCount)
        {
            Id = new Guid();
            owner = player;
            DiceValue = diceValue;
            ChipsCount = chipsCount;
        }

        public Player GetOwner()
        {
            return owner;
        }
    }
}
