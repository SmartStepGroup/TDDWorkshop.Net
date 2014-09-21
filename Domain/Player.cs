using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    public class Player
    {
        private Game currentGame;
        private int chipsCount;
        public Guid Id;

        public Player()
        {
            Id = new Guid();
        }

        public void Enter(Game game)
        {
            if (currentGame != null) throw new InvalidOperationException("Можешь играть в одну игру ты только, юный падован");
            game.CheckMaxPlayers();

            currentGame = game;
            game.Join();
        }

        public bool IsIn(Game game)
        {
            return currentGame == game;
        }

        public void Exit()
        {
            if (currentGame == null) throw new InvalidOperationException("Не войдя не можешь ты выйти, юный падован");
            currentGame.Leave();
            currentGame = null;
        }

        public void BuyChips(int count)
        {
            chipsCount += count;
        }

        public bool CanDoBets()
        {
            return chipsCount > 0;
        }

        public bool HasBets()
        {
            List<Bet> bets = currentGame.GetBets();
            foreach (var bet in bets)
            {
                if (bet.GetOwner().Id == this.Id) return true;
            }
            return false;
        }
    }
}
