using System;

namespace Domain
{
    public class Player
    {
        private Game activeGame;
        
        public void Enter(Game game)
        {
            if (activeGame == game)
                throw new InvalidOperationException("Ты уже в этой игре!");
            if (activeGame != null)
                throw new InvalidOperationException("Нельзя войти во вторую игру");
           activeGame = game;
           game.addNewPlayer();
        }

        public bool IsIn(Game game)
        {
            return game == activeGame;
        }

        public void Exit()
        {
            if(activeGame==null) throw new InvalidOperationException("Нельзя просто так выйти из игры не войдя");
            activeGame = null;
        }
        
    }
}
