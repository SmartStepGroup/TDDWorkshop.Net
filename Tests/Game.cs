using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    public class Game
    {
        private int playerCount = 0;
        private List<Bet> bets = new List<Bet>();

        public void Join()
        {
            playerCount++;
        }

        public void Leave()
        {
            playerCount--;
        }

        public void CheckMaxPlayers()
        {
            if (playerCount == 6) throw new InvalidOperationException("В игре число игроков максимальное, юный падован"); ;
        }

        public void DoBet(Bet bet)
        {
            if (!bet.GetOwner().IsIn(this)) throw new InvalidOperationException("Нельзя делать ставки не находясь в игре");
            bets.Add(bet);
        }

        public List<Bet> GetBets()
        {
            return bets;
        }
    }
}
