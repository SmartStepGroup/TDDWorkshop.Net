namespace Domain
{
    public class Player
    {
        private Game activeGame;
 
        public bool inGame(Game game)
        {
            return true;
        }

        public void setActiveGame(Game game)
        {
            this.activeGame = game;
        }

        public Game getActiveGame()
        {
            return activeGame;
        }

        public void enter(Game game)
        {
            
        }

        public void exit()
        {
            setActiveGame(null);
        }
    }
}