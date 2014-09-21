﻿using System;
using System.IO;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class PlayerTests : BaseTest
    {
        [Test]
        public void EnterGame_Player_InGame()
        {
            var player = CreatePlayer();
            var game = CreateGame();

            player.Enter(game);

            Assert.IsTrue(player.IsIn(game));
        }

        [Test]
        public void ByDefaultExitGame_Player_NotInGame()
        {
            var player = CreatePlayer();
            var game = CreateGame();
            player.Enter(game);
            player.Exit();

            var e = Assert.Throws<InvalidOperationException>(player.Exit);
            Assert.AreEqual("Не войдя не можешь ты выйти, юный падован", e.Message);
        }

        [Test]
        public void ExitGame_Player_NotInGame()
        {
            var player = CreatePlayer();
            var game = CreateGame();

            player.Enter(game);
            player.Exit();

            Assert.IsFalse(player.IsIn(game));
        }

        [Test]
        public void JoinGameWhereHasPlayer_Player_InGame()
        {
            var player = CreatePlayer();
            var anotherPlayer = CreatePlayer();
            var game = CreateGame();

            player.Enter(game);
            anotherPlayer.Enter(game);

            Assert.IsTrue(anotherPlayer.IsIn(game));
        }

        [Test]
        public void OneGame_Player_InGame()
        {
            var player = CreatePlayer();
            var game = CreateGame();

            player.Enter(game);

            var e = Assert.Throws<InvalidOperationException>(() => player.Enter(game));
            Assert.AreEqual("Можешь играть в одну игру ты только, юный падован", e.Message);
        }

        [Test]
        public void BuyChips_Player_CanDoBets()
        {
            var player = new Player();

            player.BuyChips(1);

            Assert.IsTrue(player.CanDoBets());
        }

        [Test]
        public void DoBet_Player_MayWin()
        {
            var player = new Player();
            var game = new Game();
            player.Enter(game);

            game.DoBet(new Bet(player));

            Assert.IsTrue(player.HasBets());
        }

        [Test]
        public void CanBeInGame_Player_DoBet()
        {
            var player = new Player();
            var game = new Game();
            player.Enter(game);
            player.Exit();

            Assert.Throws<InvalidOperationException>(() => game.DoBet(new Bet(player))).WithMessage("Нельзя делать ставки не находясь в игре");
        }
    }

    public class BaseTest
    {
        public Game CreateGame()
        {
            return new Game();
        }

        public Player CreatePlayer()
        {
            return new Player();
        }
    }

    [TestFixture]
    public class GameTests : BaseTest
    {
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
    }

    public static class ExteptionExtensions
    {
        public static void WithMessage(this Exception e, string expectedMessage)
        {
            Assert.AreEqual(expectedMessage, e.Message);
        }
    }
}