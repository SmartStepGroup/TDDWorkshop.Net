using System;

namespace Domain
{
    public class Player
    {
        private Game activeGame;
        public int AvailableChips { get; private set; }
        public bool HasBets { get; private set; }

        public void Enter(Game game)
        {
            this.activeGame = game;
        }

        public bool IsIn(Game game)
        {
            return game == activeGame;
        }

        public void Exit()
        {
            if (activeGame == null) throw new InvalidOperationException("Нельзя выйти из игры, не войдя");
            activeGame = null;
        }

        public void BuyChips(int chips) {
            AvailableChips += chips;
        }

        public void Bet(int betAmount) {
            if (activeGame == null) throw new InvalidOperationException("Please join the game before making a bet");
            HasBets = true;
        }
    }
}