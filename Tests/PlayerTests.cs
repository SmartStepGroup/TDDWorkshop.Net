﻿using System;
using Domain;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class PlayerTests
    {
        [Test]
        public void EntersGame_SinglePlayer_InGame()
        {
            var player = new Player();
            var game = new DiceGame();
            player.Enter(game);
            Assert.IsTrue(player.IsIn(game));
        }

        [Test]
        public void ExitGame_Player_NotInGame()
        {
            var player = new Player();
            var game = new DiceGame();
            player.Enter(game);
            player.Exit();
            Assert.IsFalse(player.IsIn(game));
        }

        [Test]
        public void ExitGame_PlayerNotInGame_ThrowsException()
        {
            var player = new Player();
            Assert.Throws<InvalidOperationException>(player.Exit);
        }

        [Test]
        public void EnterGame_PlayerNotInGame_MultiplayerGame()
        {
            var player = new Player();
            var player2 = new Player();
            var game = new DiceGame();
            player.Enter(game);
            player2.Enter(game);
            Assert.IsTrue(player2.IsIn(game));
        }
        [Test]
        public void CannotEnter_SinglePlayer_TwoGames()
        {
            var player = new Player();
            var game1 = new DiceGame();
            player.Enter(game1);
            var game2 = new DiceGame();
            Assert.Throws <InvalidOperationException>(()=>player.Enter(game2));

        }

        [Test]
        public void Can_SinglePlayer_BuyCoins()
        {
            var player = new Player();
            player.BuyCoins(1);
            Assert.IsTrue(1 == player.GetAvailableCoins());
        }

        [Test]
        public void MakeBet_DiceGame_Succeeded()
        {
            var DiceBoard = new DiceGame();
            var Anna = new Player();
            Anna.Enter(DiceBoard);
            Anna.MakeBet(1);
            Assert.IsTrue(DiceBoard.BetsBank() == 1);
        }

        [Test]
        public void MakeBet_ZeroAmount_ThrowsException()
        {
            var DiceBoard = new DiceGame();
            var Anna = new Player();
            Anna.Enter(DiceBoard);
            Assert.Throws<ArgumentException>(() => Anna.MakeBet(0));
        }

        [Test]
        public void MakeBet_Exceed6_ThrowsException()
        {
            var DiceBoard = new DiceGame();
            var Anna = new Player();
            Anna.Enter(DiceBoard);
            Assert.Throws<ArgumentException>(() => Anna.MakeBet(7));
        }

        [Test]
        public void MakeBet_RoundBegin_ThrowsException()
        {
            var DiceBoard = new DiceGame();
            var Anna = new Player();
            Anna.Enter(DiceBoard);
            DiceBoard.BeginRound();
            Assert.Throws<InvalidOperationException>(() => Anna.MakeBet(1));
        }
    }
}