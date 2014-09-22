using System;
using Domain;
using NUnit.Framework;
using Tests;

namespace Tests {
    [TestFixture]
    public class PlayerCan : Test  {

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

            Assert
                .Throws<InvalidOperationException>(() => player.setActiveGame(secondGame))
                .MessageIs("Можно играть только в одну игру одновременно");
        }

        [Test]
        public void playWhenOthersPlayerInGame() {
            Game game = Create.Game;
            Player firstPlayer = Create.Player.In(game);
            Player secondPlayer = Create.Player;

            secondPlayer.setActiveGame(game);

            Assert.AreSame(game, secondPlayer.getActiveGame());
        }

        [Test]
        public void bye11Chips() {
            Player player = Create.Player.With(cheaps: 11);

            Assert.AreEqual(11, player.getChipsCount());
        }

        [Test]
        public void bet11Chips() {
            Game game = Create.Game;
            Player player = Create.Player
                .With(cheaps: 20)
                .In(game);

            player.bets(11.Chips().On(2));

            Assert.AreEqual(11, player.getBetAmount());
        }

        [Test]
        public void looseWhenBets2() {
            Game game = Create.Game;
            Player player = Create.Player
                .With(cheaps: 20)
                .In(game)
                .Bets(11.Chips().On(2));

            player.play();

            Assert.AreEqual(20 - 11, player.getChipsCount());
        }

             
        

    }

    public static class ExceptionExtentions {
        public static void MessageIs(this Exception exception, string expectedMessage) {
            Assert.AreEqual(expectedMessage, exception.Message);
        }
    }
    
    public static class IntExtentions {
        public static Bet Chips(this int amount) {
            return new Bet(amount, 1);
        }
        
        public static Bet On(this Bet bet, int score) {
            return new Bet(bet.Amount, score);
        }
    }
}


    