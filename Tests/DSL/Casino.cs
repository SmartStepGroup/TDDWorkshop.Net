using Domain;
using FakeItEasy;

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

        public Casino Chips(int i)
        {
            player.buyChips(i);
            return this;
        }

        public Casino makeBet(int face, int chips)
        {
            player.makeBet(face, chips);
            return this;
        }

        public Casino enterWinningGame()
        {
            
            var game = A.Fake<Game>();
            A.CallTo(() => game.play(100,6)).Returns(600);
            player.enterGame(game);
            
            return this;
        }
    }
}