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
            var player = new Player();
            var game = new Game();
            player.Enter(game);
            Assert.IsTrue(player.IsIn(game));
        }

        [Test]
        public void ExitGame_Player_NotInGame()
        {
            var player = new Player();
            var game = new Game();
            player.Enter(game);
            player.Exit();
            Assert.IsFalse(player.IsIn(game));
        }

        [Test]
        public void ExitGame_PlayerNotInGame_ThrowsException()
        {
            var player = new Player();
            Assert.Throws<InvalidOperationException>(player.Exit);
        }

        [Test]
        public void EnterGame_PlayerNotInGame_MultiplayerGame()
        {
            var player = new Player();
            var player2 = new Player();
            var game = new Game();
            player.Enter(game);
            player2.Enter(game);
            Assert.IsTrue(player2.IsIn(game));
        }
        [Test]
        public void CannotEnter_SinglePlayer_TwoGames()
        {
            var player = new Player();
            var game1 = new Game();
            player.Enter(game1);
            var game2 = new Game();
            Assert.Throws <InvalidOperationException>(()=>player.Enter(game2));

        }
    }
}