using System;
using NUnit.Framework;
using Tests.DSL;

namespace Tests
{
    [TestFixture]
    public class GameTests : BaseTest
    {
        [SetUp]
        public void SetUp()
        {
            Create = new Father();
        }

        [Test]
        public void EnterGame_Game_Max6Players()
        {
            var game = CreateGame();
            var player = CreatePlayer();
            var player2 = CreatePlayer();
            var player3 = CreatePlayer();
            var player4 = CreatePlayer();
            var player5 = CreatePlayer();
            var player6 = CreatePlayer();
            var player7 = CreatePlayer();

            player.Enter(game);
            player2.Enter(game);
            player3.Enter(game);
            player4.Enter(game);
            player5.Enter(game);
            player6.Enter(game);

            var e = Assert.Throws<InvalidOperationException>(() => player7.Enter(game));
            Assert.AreEqual("В игре число игроков максимальное, юный падован", e.Message);
        }

        [Test]
        public void NoBets_Player_GameNotStart()
        {
            Game game = Create.Game;
            Player player = Create.Player.In(game).WithChips(100);

            Assert.Throws<InvalidOperationException>(() => game.Start(player)).WithMessage("Нельзя начать игру без ставок");
        }
    }
}