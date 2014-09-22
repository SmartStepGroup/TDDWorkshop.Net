using System;
using Domain;
using NUnit.Framework;

namespace Tests {
    [TestFixture]
    public class PlayerCan : Test {

        [Test]
        public void byDefaultPlayerHasNotGame() {
            Player player = Create.Player;

            Assert.IsNull(player.getActiveGame());
        }

        [Test]
        public void EnterToGame() {
            Game game = Create.Game;
            Player player = Create.Player.In(game);

            Assert.IsTrue(player.getActiveGame() == game);
        }

        [Test]
        public void ExitGame() {
            Game game = Create.Game;
            Player player = Create.Player.In(game);

            player.exit();

            Assert.IsNull(player.getActiveGame());
        }

        [Test]
        public void playSingleGame() {
            Game firstGame = Create.Game;
            Player player = Create.Player.In(firstGame);
            Game secondGame = Create.Game;

            var e = Assert.Throws<InvalidOperationException>(() => { player.setActiveGame(secondGame); });
            Assert.IsTrue(e.Message.Equals("Можно играть только в одну игру одновременно"));
        }

        [Test]
        public void playWhenOthersPlayerInGame() {
            Game game = Create.Game;
            Player firstPlayer = Create.Player.In(game);
            Player secondPlayer = Create.Player.In(game);


            Assert.IsTrue(secondPlayer.getActiveGame() == game);
        }

        [Test]
        public void bye11Chips() {
            Player player = Create.Player.With(cheaps: 11);

            Assert.AreEqual(11, player.getChipsCount());
        }

        protected Father Create = new Father();

    }
}


    