using System;

namespace Domain
{
    public class Player
    {
        private Game activeGame;
        public void Enter(Game game)
        {
            activeGame = game;
        }

        public bool IsIn(Game game)
        {
            return activeGame == game;
        }

        public void Exit()
        {
            if (activeGame == null) throw new InvalidOperationException("Нельзя выйти из игры, не входя в неё");
            activeGame = null;
        }
    }
}