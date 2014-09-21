using Domain;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class PlayerCan
    {
        [Test]
        public void EnterToGame()
        {
            Player player = CreatePlayer();
            Game game = CreateGame();

            player.setActiveGame(game);

            Assert.IsTrue(player.getActiveGame() == game);
        }

        [Test]
        public void ExitGame()
        {
            Player player = CreatePlayer();

            player.exit();

            Assert.IsNull(player.getActiveGame());
        }

        private static Game CreateGame()
        {
            return new Game();
        }

        private static Player CreatePlayer()
        {
            return new Player();
        }
    }
}