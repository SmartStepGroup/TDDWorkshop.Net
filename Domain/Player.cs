using System;

namespace Domain
{
    public class Player
    {
        private Game activeGame;

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
    }
}