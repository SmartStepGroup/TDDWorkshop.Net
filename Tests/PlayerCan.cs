using System;
using Domain;
using NUnit.Framework;

namespace Tests {
    [TestFixture]
    public class PlayerCan : Test {

        [Test]
        public void byDefaultPlayerHasNotGame() {
            Player player = CreatePlayer();

            Assert.IsNull(player.getActiveGame());
        }

        [Test]
        public void EnterToGame() {
            Player player = CreatePlayer();
            Game game = CreateGame();

            player.setActiveGame(game);

            Assert.IsTrue(player.getActiveGame() == game);
        }

        [Test]
        public void ExitGame() {
            Game game = CreateGame();
            Player player = Create.Player.In(game);

//            player.setActiveGame(game);

            player.exit();

            Assert.IsNull(player.getActiveGame());
        }

        [Test]
        public void playSingleGame() {
            Player player = CreatePlayer();
            Game firstGame = CreateGame();
            Game secondGame = CreateGame();
            player.setActiveGame(firstGame);

            var e = Assert.Throws<InvalidOperationException>(() => { player.setActiveGame(secondGame); });
            Assert.IsTrue(e.Message.Equals("Можно играть только в одну игру одновременно"));
        }

        [Test]
        public void playWhenOthersPlayerInGame() {
            Game game = CreateGame();
            Player firstPlayer = CreatePlayer();
            Player secondPlayer = CreatePlayer();

            firstPlayer.setActiveGame(game);
            secondPlayer.setActiveGame(game);

            Assert.IsTrue(secondPlayer.getActiveGame() == game);
        }

        [Test]
        public void bye11Chips() {
            Player player = CreatePlayer();

            player.byeChips(11);

            Assert.IsTrue(player.getChipsCount() == 11);
        }

        protected Father Create = new Father();



    }
}


    