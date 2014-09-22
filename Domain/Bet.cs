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
        public int DiceValue;
        public int ChipsCount;
        public Guid betId;

        public Bet()
        {
            betId = new Guid();
        }
        public Bet(int diceValue, int chipsCount)
        {
            betId = new Guid();
            DiceValue = diceValue;
            ChipsCount = chipsCount;
        }
    }
}
