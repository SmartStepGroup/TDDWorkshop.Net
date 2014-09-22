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

        public Bet()
        {
            
        }
        public Bet(int diceValue, int chipsCount)
        {
            DiceValue = diceValue;
            ChipsCount = chipsCount;
        }
    }
}
