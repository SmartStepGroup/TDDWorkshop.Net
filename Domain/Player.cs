using System;

namespace Domain
{
    public class Player
    {
        private Game _activeGame;

        public void EnterInGame(Game game)
        {
            if (_activeGame != null)
                throw new Exception("Войти в игру, будучи уже в игре, может только джедай");

            if (game._countPlayer >= 6)
                throw new Exception("Этот стол заполнен. Найдите себе другой.");
            this._activeGame = game;
               
            this._activeGame._countPlayer++;
            
            
        }

        public bool InGame(Game game)
        {
            return this._activeGame != null;
        }

        public void ExitFromGame() 
        {
            if (_activeGame == null)
                throw new Exception("Выйти из игры не войдя, может только джедай");
            this._activeGame = null;

        }
    }
}