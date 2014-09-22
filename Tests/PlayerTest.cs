﻿using System;
using System.Runtime.Remoting.Messaging;
using Domain;
using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace Tests
{
    [TestFixture]
    internal class PlayerTest : Test
    {
        [Test]
        public void SinglePlayer_EnterInGame_CanEnter()
        {
            var player = CreatePlayer();
            var game = CreateGame();

            player.EnterTo(game);

            Assert.IsTrue(player.HasEntered(game));
        }

        [Test]
        public void Player_ExitFromGame_CanExit()
        {
            var player = CreatePlayer();
            var game = CreateGame();
            player.EnterTo(game);

            player.ExitFromGame();

            Assert.IsFalse(player.HasEntered(game));
        }

        [Test]
        public void PlayerNotInGame_CanNotExitFromGame()
        {
           var player = CreatePlayer();
           
           var ex = Assert.Throws<Exception>(player.ExitFromGame);
           Assert.AreEqual("Выйти из игры не войдя, может только джедай", ex.Message);
        }

        [Test]
        public void PlayerInGame_EntersTheSameGame_ThrowsException()
        {
            var player = CreatePlayer();
            var game = CreateGame();
            player.EnterTo(game);

            var ex = Assert.Throws<Exception>(() => player.EnterTo(game));
            Assert.AreEqual("Войти в игру, будучи уже в игре, может только джедай",ex.Message);
        }

        [Test]
        public void TwoPlayers_CanEnterInGame()
        {
            var player1 = CreatePlayer();
            var player2 = CreatePlayer();

            var game = CreateGame();

            player1.EnterTo(game);
            player2.EnterTo(game);

            Assert.IsTrue(player2.HasEntered(game));
        }

        [Test]
        public void TwoPlayers_CanEnterInGame_FirstPlayerInGame()
        {
            var player1 = CreatePlayer();
            var player2 = CreatePlayer();

            var game = CreateGame();

            player1.EnterTo(game);
            player2.EnterTo(game);

            Assert.IsTrue(player1.HasEntered(game));
        }

        [Test]
        public void Player_CanBuyChips()
        {
            var captainJackSparrow = CreatePlayer();

            captainJackSparrow.BuyChips(5);

            Assert.AreEqual(5, captainJackSparrow.GetBalance());
        }

        [Test]
        public void Player_CanBet_NotThrowsException()
        {
            var captainJackSparrow = CreatePlayer();
            captainJackSparrow.BuyChips(50);
            var game = new Game();
            captainJackSparrow.EnterTo(game);
            var bet = new Bet(14,6);

            Assert.DoesNotThrow(() => captainJackSparrow.Do(bet));
        }

        [Test]
        public void Player_CanNotBetIfBalanceLessBet_ThrowsException()
        {
            var captainJackSparrow = CreatePlayer();
            captainJackSparrow.BuyChips(50);
            var game = CreateGame();
            captainJackSparrow.EnterTo(game);
            var bet = new Bet(55, 6);

            var ex = Assert.Throws<InvalidOperationException>(() => captainJackSparrow.Do(bet));
            Assert.AreEqual("Баланс недостаточен.", ex.Message);
        }

        [Test]
        public void Player_CanNotWrongBetLessOne_ThrowsException()
        {
         //   var captainJackSparrow = new Player();
           // var captainJackSparrow = CreatePlayerWithBet(25, 0);
           // Bet bet = captainJackSparrow.GetListBet()[0];
            var ex = Assert.Throws<InvalidOperationException>(() => CreatePlayerWithBet(25, 0));
            Assert.AreEqual("Ставка не может быть меньше 1", ex.Message);
        }

        private Player CreatePlayerWithBet(int size, int score)
        {
            var captainJackSparrow = CreatePlayer();
            captainJackSparrow.BuyChips(50);
            var game = CreateGame();
            captainJackSparrow.EnterTo(game);
            var bet = new Bet(size, score);
            captainJackSparrow.Do(bet);
           
            return captainJackSparrow;
        }

       
        [Test]
        public void Player_CanNotWrongBetMoreSix_ThrowsException()
        {
          //  var captainJackSparrow = CreatePlayerWithBet(25, 7);
          //  Bet bet = captainJackSparrow.GetListBet()[0];
            var ex = Assert.Throws<InvalidOperationException>(() => CreatePlayerWithBet(25, 7));
            Assert.AreEqual("Ставка не может быть больше 6", ex.Message);
        }

        [Test]
        public void Player_CanChangeBetBeforeGameStarted()
        {
            var casino = CreateCasino();
            var captainJackSparrow = CreatePlayerWithBet(25, 5);
            var game = captainJackSparrow.GetGame();
            captainJackSparrow.GetListBet()[0].Change(56, 1, game);
            game.Start(casino);

            Assert.AreEqual(1, captainJackSparrow.GetListBet()[0].GetScore());
        }

        private Casino CreateCasino()
        {
            return new Casino();
        }

        [Test]
        public void Player_CanCanceledBetBeforeGameStarted()
        {
            Game game1 = new Game();
            Player captainJackSparrow = Create.Player
                .WithBalance(100)
             //   .WithGame(game1)
                .WithBet(15.On(3));
                

           // game = captainJackSparrow.GetGame();

            captainJackSparrow.DeleteBet();
          //  game.Start();

            Assert.Null(captainJackSparrow.GetListBet());
        }

        [Test]
        public void Player_CanAddSomeBet()
        {
            var casino = CreateCasino();
            Player captainJackSparrow = Create.Player
                 .WithBalance(100)
                 .WithGame(CreateGame())
                 .WithBet(15.On(3));
            
            captainJackSparrow.Do(new Bet(10,4));
            captainJackSparrow.Do(new Bet(12, 3));
            captainJackSparrow.GetGame().Start(casino);

            Assert.IsTrue(captainJackSparrow.GetListBet().Count > 1);
        }

        [Test]
        public void Player_LoseOneBet()
        {
            var casino = CreateCasino();
            var game = CreateGame();
            Player looser = Create.Player
                .WithBalance(150)
                .WithBet(15.On(1))
                .WithGame(game);
            game.Start(casino);
            Assert.AreNotEqual(game.GetResult(), looser.GetListBet()[0].GetScore());
        }

        [Test]
        public void Player_WinWithOneBet()
        {
            var game = CreateGame();
            var casino = CreateCasino();
            Player winner = Create.Player
                .WithBalance(150)
                .WithBet(15.On(6))
                .WithGame(game);
            int beforeBalance = winner.GetBalance();

            game.Start(casino);
            int balanceResult = (beforeBalance + (winner.GetListBet()[0].GetSize()*6));
            Assert.AreEqual(winner.GetBalance() , balanceResult);
        }

        [Test]
        public void Casino_Win_PlayersBet()
        {
            var casino = CreateCasino();
            var game = CreateGame();
            Player looser = Create.Player
                .WithBalance(150)
                .WithBet(15.On(1))
                .WithGame(game);
            int casinoBalanceBefore = Casino.GetBalance();

            game.Start(casino);


            Assert.AreEqual(Casino.GetBalance(), casinoBalanceBefore + looser.GetListBet()[0].GetSize());
        }

        [Test]
        public void Player_CanDoManyBetAndWin()
        {
            var casino = CreateCasino();
            var game = CreateGame();
            Player winner = Create.Player
                .WithBalance(150)
                .WithBet(15.On(1)).WithBet(15.On(2)).WithBet(15.On(3)).WithBet(15.On(4)).WithBet(15.On(5)).WithBet(15.On(6))
                .WithGame(game);
            

            game.Start(casino);


            Assert.AreEqual(150, winner.GetBalance());
        }

        [Test]
        public void Player_CanDoManyBetOnOneScoreAndWin()
        {
            var game = CreateGame();
            var casino = CreateCasino();
            Player winner = Create.Player
                .WithBalance(150)
                .WithBet(20.On(1)).WithBet(20.On(1)).WithBet(30.On(3)).WithBet(40.On(4)).WithBet(10.On(6)).WithBet(10.On(6))
                .WithGame(game);
            
          //  int beforeBalance = winner.GetBalance();

            game.Start(casino);

            Assert.AreEqual(150 - 20 - 20 - 30 - 40 -10 -10 + 10*6 + 10*6, winner.GetBalance());
        }


        [SetUp]
        public void Init()
        {
           Create   = new Father();
           
        }

        public Father Create;
    }

    public static class IntExtend
    {
        public static Bet On(this int size, int score)
        {
            return new Bet(size, score);
        }
    }

    public static class StringToInt
    {
        public static int ToInt(this string size)
        {
            return int.Parse(size);
        }
    }

    public class Father
    {
        public PlayerFather Player
        {
            get { return new PlayerFather(); }
        }
    }

    public class PlayerFather
    {
        private Player player = new Player();
       
        public PlayerFather WithGame(Game game)
        {
            player.EnterTo(game);
            return this;
        }

        public PlayerFather WithBalance(int balance)
        {
            player.BuyChips(balance);
            return this;
        }

        public PlayerFather WithBet(Bet bet)
        {
            player.Do(bet);
            return this;
        }

        public static implicit operator Player(PlayerFather father)
        {
            return father.player;
        }

       
    }
}
