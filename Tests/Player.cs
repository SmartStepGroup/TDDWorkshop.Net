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