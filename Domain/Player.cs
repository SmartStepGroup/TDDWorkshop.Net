using System;
using System.Collections.Generic;

namespace Domain
{
    public class Player
    {
        private Game _activeGame;
        private int _balance = 0;
        private List<Bet> _activeBet = new List<Bet>();

        public void EnterTo(Game game)
        {
            ValidateBeforeEnter(game);
            game.AddPlayer(this);
            _activeGame = game;
            _activeGame.Join(this);
        }

        public bool HasEntered(Game game)
        {
            return _activeGame != null;
        }

        public void ExitFromGame()
        {
            if (_activeGame == null)
                throw new Exception("Выйти из игры не войдя, может только джедай");

            _activeGame = null;
        }

        private void ValidateBeforeEnter(Game game)
        {
            if (_activeGame != null)
                throw new Exception("Войти в игру, будучи уже в игре, может только джедай");
            if (game.playerCount >= 6)
                throw new Exception("Этот стол заполнен. Найдите себе другой.");
        }

        public int GetBalance()
        {
            return _balance;
        }

        public void BuyChips(int count)
        {
            _balance += count;
        }

        public void Do(Bet bet)
        {
            ValidateDoBet(bet);
            _activeBet.Add(bet);
            _balance -= bet.GetSize();
        }

        private void ValidateDoBet(Bet bet)
        {
            if (bet.GetSize() > _balance)
                throw new InvalidOperationException("Баланс недостаточен.");
            if (bet.GetScore() < 1)
                throw new InvalidOperationException("Ставка не может быть меньше 1");
            if (bet.GetScore() > 6)
                throw new InvalidOperationException("Ставка не может быть больше 6");
        }

        public Game GetGame()
        {
            return _activeGame;
        }

        public void DeleteBet()
        {
            _activeBet = null;
        }

        public List<Bet> GetListBet()
        {
            return _activeBet;
        }

        public void AddChips(int size)
        {
            BuyChips(size);
        }
    }
}