using Domain;

namespace Tests.DSL
{
    public class Casino
    {
        private static Player player;
        private static Game game;
        public Casino Player()
        {
            player = new Player();
            return this;
        }

        public Casino Game()
        {
            game = new Game();
            return this;
        }

        public static implicit operator Game(Casino casino)
        {
            return game;
        }

        public static implicit operator Player(Casino casino)
        {
            return player;
        }

        public Casino enterGame()
        {
            game = new Game();
            player.enterGame(game);
            return this;
        }
    }
}