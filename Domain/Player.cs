using System;

namespace Domain
{
    public class Player
    {
        public int AvailableChips { get; private set; }
        public bool HasBets {
            get { return CurrentBet != null; }
        }
        public Game ActiveGame { get; private set; }
        public Bet CurrentBet { get; private set; }

        public void Enter(Game game)
        {
            ActiveGame = game;
        }

        public bool IsIn(Game game)
        {
            return game == ActiveGame;
        }

        public void Exit()
        {
            if (ActiveGame == null) throw new InvalidOperationException("Нельзя выйти из игры, не войдя");
            ActiveGame = null;
        }

        public void BuyChips(int chips) {
            AvailableChips += chips;
        }

        public void Bet(Bet bet) {
            if (ActiveGame == null) throw new InvalidOperationException("Please join the game before making a bet");
            if (bet.Score < 1 || bet.Score > 6) throw new InvalidOperationException("Please bet to a number from 1 to 6");
            if (bet.Chips > AvailableChips) throw new InvalidOperationException("You are out of money, looser");

            CurrentBet = bet;
        }

        public void ReplaceBet(Bet anotherBet) {
            Bet(anotherBet);
        }

        public void OneMoreBet(Bet bet) {
            if (ActiveGame == null) throw new InvalidOperationException("Please join the game before making a bet");
            if (bet.Score < 1 || bet.Score > 6) throw new InvalidOperationException("Please bet to a number from 1 to 6");
            if (bet.Chips > AvailableChips) throw new InvalidOperationException("You are out of money, looser");

            CurrentBet = bet;
        }

    }
}