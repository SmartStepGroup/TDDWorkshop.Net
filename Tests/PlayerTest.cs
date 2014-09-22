using System;
using Domain;
using NUnit.Framework;

namespace Tests {
    [TestFixture]
    public class PlayerTest : Test {
        [Test]
        public void GameExit_ExitsWhenHas4Players_Has3Players()
        {
            Game game = Create.Game;
            Player player1 = CreatePlayer();
            Player player2 = CreatePlayer();
            Player player3 = CreatePlayer();
            Player player4 = CreatePlayer();
            player1.setActiveGame(game);
            player2.setActiveGame(game);
            player3.setActiveGame(game);
            player4.setActiveGame(game);

            player4.exit();

            Assert.IsTrue(game.getPlayersCount() == 3);
        }

    }
}