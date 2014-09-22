using System;
using System.IO;
using NUnit.Framework;
using Tests.DSL;

namespace Tests
{
    [TestFixture]
    public class PlayerTests : BaseTest
    {
        [SetUp]
        public void SetUp()
        {
            Create = new Father();
        }
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
            Game game = Create.Game;
            Player player = Create.Player.In(game);

            player.DoBet(CreateBet());

            Assert.IsTrue(player.HasBets());
        }

        [Test]
        public void CanBeInGame_Player_DoBet()
        {
            var player = new Player();
            var game = new Game();
            player.Enter(game);
            player.Exit();

            Assert.Throws<InvalidOperationException>(() => player.DoBet(CreateBet())).WithMessage("Нельзя делать ставки не находясь в игре");
        }

        [Test]
        public void DoRightBets_Player_DoBet()
        {
            Player player = new Player();
            Game game = new Game();
            player.Enter(game);

            Assert.Throws<InvalidOperationException>(() => player.DoBet(CreateBet(diceValue: 7))).WithMessage("Значение ставки должно быть от 1 до 6");
        }

        [Test]
        public void NotEnoughChips_Player_DoBet()
        {
            Game game = Create.Game.Started();
            Player player = Create.Player.In(game).WithChips(3);

            Assert.Throws<InvalidOperationException>(() => player.DoBet(5.Chips().On(1))).WithMessage("Ты не можешь фишек больше поставить чем у тебя есть");
        }

        [Test]
        public void ChangeBetBegoreStartingGame_Player_ChangeBet()
        {
            Game game = Create.Game.Started();
            Bet startedBet = CreateBet();
            Player player = Create.Player.In(game).WithChips(100.Chips()).WithBet(startedBet);
            Bet changedBet = 50.Chips().On(1);

            Assert.Throws<InvalidOperationException>(() => player.ChangeBet(startedBet.betId,changedBet)).WithMessage("Нельзя поменять ставку в игре которая уже началась");
        }

        [Test]
        public void BetIsWrong_Player_Lose()
        {
            Game game = Create.Game.WithLuckyScore(6);
            Player player = Create.Player.In(game).WithChips(100).WithBet(80.Chips().On(1));
            Casino casino = Create.Casino;

            game.Start(player, casino);

            Assert.AreEqual(20, player.AvailibleChipsCount());
        }

        [Test]
        public void BetIsRight_Player_Winx6()
        {
            Game game = Create.Game.WithLuckyScore(6);
            Player player = Create.Player.In(game).WithChips(100).WithBet(40.Chips().On(6));
            Casino casino = Create.Casino;

            game.Start(player, casino);

            Assert.AreEqual(300, player.AvailibleChipsCount());
        }

        [Test]
        public void DoSomeBets_Player_GetReward()
        {
            Game game = Create.Game.WithLuckyScore(6);
            Player player = Create.Player
                .In(game)
                .WithChips(100)
                .WithBet(10.Chips().On(6))
                .WithBet(30.Chips().On(3));
            Casino casino = Create.Casino;

            game.Start(player, casino);

            Assert.AreEqual(120, player.AvailibleChipsCount());            
        }
    }

}