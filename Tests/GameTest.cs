using System;
using Domain;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    internal class GameTest
    {
       

        [Test]
        public void Game_DoesNotEnterMore6Players()
        {
            var player1 = CreatePlayer();
            var player2 = CreatePlayer();
            var player3 = CreatePlayer();
            var player4 = CreatePlayer();
            var player5 = CreatePlayer();
            var player6 = CreatePlayer();
            var player7 = CreatePlayer();

            var game = CreateGame();

            player1.EnterInGame(game);
            player2.EnterInGame(game);
            player3.EnterInGame(game);
            player4.EnterInGame(game);
            player5.EnterInGame(game);
            player6.EnterInGame(game);
            

           var ex = Assert.Throws<Exception>(() => player7.EnterInGame(game));

           Assert.AreEqual("Этот стол заполнен. Найдите себе другой.", ex.Message);
            }





        public Player CreatePlayer()
        {
            return new Player();
        }

        public Game CreateGame()
        {
            return new Game();
        }


    }
}
