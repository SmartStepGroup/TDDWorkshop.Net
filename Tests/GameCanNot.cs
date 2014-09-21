using System;
using Domain;
using NUnit.Framework;

namespace Tests {
    [TestFixture]
    public class GameCanNot : Test {
        [Test]
        public void joinSevethPlayerThrowInvalidOperationException() {
            Game game = CreateGameWithSixPlayers();
            Player player7 = CreatePlayer();

            var e = Assert.Throws<InvalidOperationException>(() => player7.setActiveGame(game));
            Assert.AreEqual("Игра не может допустить более 6 игроков", e.Message);
        }

        private static Game CreateGameWithSixPlayers() {
            Game game = CreateGame();
            Player player1 = CreatePlayer();
            Player player2 = CreatePlayer();
            Player player3 = CreatePlayer();
            Player player4 = CreatePlayer();
            Player player5 = CreatePlayer();
            Player player6 = CreatePlayer();

            player1.setActiveGame(game);
            player2.setActiveGame(game);
            player3.setActiveGame(game);
            player4.setActiveGame(game);
            player5.setActiveGame(game);
            player6.setActiveGame(game);

            return game;
        }
    }
}