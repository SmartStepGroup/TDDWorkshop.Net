using System;
using Domain;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    internal class PlayerTest
    {
        [Test]
        public void SinglePlayer_EnterInGame_CanEnter()
        {
            var player = new Player(); //arrange
            var game = CreateGame();

            player.EnterInGame(game); //act

            Assert.IsTrue(player.InGame(game)); // assert
        }

        [Test]
        public void Player_ExitFromGame_CanExit()
        {
            var player = CreatePlayer(); //arrange
            var game = CreateGame();
            player.EnterInGame(game);

            player.ExitFromGame();

            Assert.IsFalse(player.InGame(game));
        }

        [Test]
        public void PlayerNotInGame_CanNotExitInGame()
        {

            var player = CreatePlayer();

           var ex = Assert.Throws<Exception>(player.ExitFromGame);
           Assert.AreEqual("Выйти из игры не войдя, может только джедай",ex.Message);
        }

        [Test]
        public void PlayerInGame_CanNotEnterInGame()
        {
            var player = CreatePlayer();
            var game = CreateGame();
            player.EnterInGame(game);
            var ex = Assert.Throws<Exception>(() => player.EnterInGame(game));

            Assert.AreEqual("Войти в игру, будучи уже в игре, может только джедай",ex.Message);
        }

        [Test]
        public void TwoPlayers_CanEnterInGame()
        {
            var player1 = CreatePlayer();
            var player2 = CreatePlayer();

            var game = CreateGame();

            player1.EnterInGame(game);
            player2.EnterInGame(game);

            Assert.IsTrue(player2.InGame(game));
        }

        [Test]
        public void TwoPlayers_CanEnterInGame_FirstPlayerInGame()
        {
            var player1 = CreatePlayer();
            var player2 = CreatePlayer();

            var game = CreateGame();

            player1.EnterInGame(game);
            player2.EnterInGame(game);

            Assert.IsTrue(player1.InGame(game));
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
