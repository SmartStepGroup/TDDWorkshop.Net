using System;

namespace Tests
{
    public class Player
    {
        private Game activeGame;

        public bool isInGame()
        {
            return activeGame != null;
        }

        public void enterGame(Game game)
        {
            if (activeGame != null)
            {
                throw new InvalidOperationException("Можно играть только в одну игру");
            }
                
            activeGame = game;
        }

        public void exitGame()
        {
            if (activeGame == null)
            {
                throw new InvalidOperationException("Войдите в игру, прежде чем выходить.");
            }

            activeGame = null;
        }
    }
}