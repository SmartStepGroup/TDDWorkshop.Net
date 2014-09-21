using Domain;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class GameTests
    {
        [Test]
        public void Game_Players_CountNotExceeded6()
        {
            var game = new Game();
            var player = new Player();
            Assert.IsTrue(game.CanJoinPlayer(player));
        }
    }
}