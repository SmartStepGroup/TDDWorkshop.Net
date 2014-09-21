using System;
using Domain;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    internal class GameTest : Test
    {
        [Test]
        public void Game_DoesNotEnterMore6Players()
        {
            var game = CreateGame();
            SixPlayersJoinGame(game);
            var seventhPlayer = CreatePlayer();

            var ex = Assert.Throws<Exception>(() => seventhPlayer.EnterTo(game));
            Assert.AreEqual("Этот стол заполнен. Найдите себе другой.", ex.Message);
        }

        private void SixPlayersJoinGame(Game game)
        {
            var player1 = CreatePlayer();
            var player2 = CreatePlayer();
            var player3 = CreatePlayer();
            var player4 = CreatePlayer();
            var player5 = CreatePlayer();
            var player6 = CreatePlayer();

            player1.EnterTo(game);
            player2.EnterTo(game);
            player3.EnterTo(game);
            player4.EnterTo(game);
            player5.EnterTo(game);
            player6.EnterTo(game);
        }
    }
}
