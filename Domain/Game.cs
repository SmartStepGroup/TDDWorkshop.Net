using System;

namespace Domain
{
    public class Game
    {
        public virtual int play(int bet, int face)
        {      
            Random random = new Random(6);
            var gameFace = random.Next();
            if (gameFace == face)
                return 6*bet;
            else            
                return 0;
        }
    }
}