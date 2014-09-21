using System;
using Domain;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class PlayerTests
    {
        [Test]
        public void EntersGame_SinglePlayer_InGame()
        {
            var player = CreatePlayer();
            var game = CreateGame();

            player.Enter(game);

            Assert.IsTrue(player.IsIn(game));
        }

        [Test]
        public void ByDefault_Player_NotInGame()
        {
            var player = new Player();
            var game = new Game();

            Assert.IsFalse(player.IsIn(game));
        }

        [Test]
        public void Exit_PlayerInGame_NotInGame()
        {
            var player = new Player();
            var game = new Game();
            player.Enter(game);

            player.Exit();

            Assert.IsFalse(player.IsIn(game));
        }

        [Test]
        public void Exit_PlayerNotInGame_ThrowsInvalidOperationException()
        {
            var player = new Player();
            
            var e = Assert.Throws<InvalidOperationException>(player.Exit);
            Assert.AreEqual("Нельзя выйти из игры, не войдя", e.Message);
        }

        private static Player CreatePlayer()
        {
            return new Player();
        }

        private static Game CreateGame()
        {
            return new Game();
        }
    }
}