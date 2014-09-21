using System;
using Domain;
using NUnit.Framework;

namespace Tests {
    [TestFixture]
    public class PlayerTest {
        private Player player;
        private Game game;

        [SetUp]
        public void Setup() {
            player = new Player();
            game = new Game();
        }

        [Test]
        public void ByDefault_PlayerNotInGame() {
            bool isPlayerInGame = player.IsIn(game);
            Assert.IsFalse(isPlayerInGame);
        }

        [Test]
        public void EntersGame_EnterToSecondGame_ThrowInvalidOperationException() {
            player.Enter(game);
            var secondGame = new Game();

            var e = Assert.Throws<InvalidOperationException>(() => player.Enter(secondGame));

            Assert.AreEqual("Нельзя войти во вторую игру", e.Message);
        }

        [Test]
        public void EntersGame_EntersTheSameGame_ThrowInvalidOperationException() {
            player.Enter(game);

            var e = Assert.Throws<InvalidOperationException>(() => player.Enter(game));

            Assert.AreEqual("Ты уже в этой игре!", e.Message);
        }

        [Test]
        public void EntersGame_SinglePlayer_InGame() {
            player.Enter(game);

            Assert.IsTrue(player.IsIn(game));
        }

        [Test]
        public void EntersGame_WhereHaveAnotherPlayers_AllPlayersInGame() {
            var secondPlayer = new Player();
            secondPlayer.Enter(game);

            player.Enter(game);

            Assert.AreEqual(player.IsIn(game), secondPlayer.IsIn(game));
        }

        [Test]
        public void EntersGame_WhereHaveAnotherPlayers_InGame() {
            var secondPlayer = new Player();
            secondPlayer.Enter(game);

            player.Enter(game);

            Assert.IsTrue(player.IsIn(game));
        }

        [Test]
        public void Exit_PlayerInGame_ExitFromGame() {
            player.Enter(game);

            player.Exit();

            Assert.IsFalse(player.IsIn(game));
        }

        [Test]
        public void Exit_PlayerNotInGame_ThrowInvalidOperationException() {
            var e = Assert.Throws<InvalidOperationException>(player.Exit);
            Assert.AreEqual("Нельзя просто так выйти из игры не войдя", e.Message);
        }
    }
}