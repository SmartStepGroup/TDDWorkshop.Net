using System;

namespace Domain
{
    public class Player
    {
        private Game _activeGame;
        private int _balance = 0;

        public void EnterTo(Game game)
        {
            ValidateBeforeEnter(game);

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
    }
}