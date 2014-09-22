namespace Tests.DSL
{
    public class GameFather
    {
        private Game game = new Game();

        public GameFather Started()
        {
            game.isStarted = true;
            return this;
        }

        public static implicit operator Game(GameFather father)
        {
            return father.game;
        }
    }
}