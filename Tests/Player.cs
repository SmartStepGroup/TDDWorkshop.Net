using System.Net.Configuration;

namespace Tests
{
    public class Player
    {
        private Game activeGame = null;

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
            activeGame = null;
        }
    }
}