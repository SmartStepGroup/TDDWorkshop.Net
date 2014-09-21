namespace Tests
{
    public class Player
    {
        private Game currentGame;

        public void Enter(Game game)
        {
            currentGame = game;
        }

        public bool IsIn(Game game)
        {
            return currentGame == game;
        }

        public void Exit()
        {
            currentGame = null;
        }
    }

    public class Game
    {
    }
}