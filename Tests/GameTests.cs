using System;
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

        [Test]
        public void Game_Players_Excceded6ThrowsException()
        {
            var game = new Game();
            for (int i = 1; i <= 6; i++)
            {
                var player = new Player();
                player.Enter(game);
            }

            Assert.Throws<InvalidOperationException>(() => new Player().Enter(game));
        }
    }
}