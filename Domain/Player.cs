using System;

namespace Domain
{
    public class Player
    {
        private Game _activeGame;

        public void EnterTo(Game game)
        {
            ValidateBeforeEnter(game);

            _activeGame = game;
            _activeGame.Join(this);
        }

        private void ValidateBeforeEnter(Game game)
        {
            if (_activeGame != null)
                throw new Exception("Войти в игру, будучи уже в игре, может только джедай");
            if (game.playerCount >= 6)
                throw new Exception("Этот стол заполнен. Найдите себе другой.");
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
    }
}