using System;
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
            var captainJackSparrow = CreatePlayerWithBet(25, 5);
            var game = captainJackSparrow.GetGame();
            captainJackSparrow.GetListBet()[0].Change(56, 1, game);
            game.Start();

            Assert.AreEqual(1, captainJackSparrow.GetListBet()[0].GetScore());
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
            Player captainJackSparrow = Create.Player
                 .WithBalance(100)
                 .WithGame(CreateGame())
                 .WithBet(15.On(3));
            
            captainJackSparrow.Do(new Bet(10,4));
            captainJackSparrow.Do(new Bet(12, 3));
            captainJackSparrow.GetGame().Start();

            Assert.IsTrue(captainJackSparrow.GetListBet().Count > 1);
        }

      

        
        public Father Create = new Father();
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
        public PlayerFather Player = new PlayerFather();



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
