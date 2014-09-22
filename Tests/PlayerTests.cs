using System;
using Domain;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class PlayerTests
    {
        [SetUp]
        public void SetupTest()
        {
            Create = new Builder();
        }

        protected DiceGame DiceBoard;
        protected Player Anna;

        private void CreateDefaultBoardAndAnna(int coins)
        {
            DiceBoard = Create.DiceGame;
            Anna = Create.Player
                .BuyCoins(coins);
        }

        protected Builder Create;

        [Test]
        public void Can_SinglePlayer_BuyCoins()
        {
            CreateDefaultBoardAndAnna(0);
            Anna.BuyCoins(1);
            Assert.IsTrue(1 == Anna.GetAvailableCoins());
        }

        [Test]
        public void CancelAllBets_Succeeded()
        {
            CreateDefaultBoardAndAnna(1000);
            Anna.Enter(DiceBoard);
            Anna.MakeBet(2, 10);
            Anna.MakeBet(3, 20);
            Anna.CancelAllBets();
            Assert.IsFalse(Anna.HasAnyBet());
        }

        [Test]
        public void CannotEnter_SinglePlayer_TwoGames()
        {
            CreateDefaultBoardAndAnna(0);
            Anna.Enter(DiceBoard);
            var game2 = new DiceGame();
            Assert.Throws<InvalidOperationException>(() => Anna.Enter(game2));
        }

        [Test]
        public void EnterGame_PlayerNotInGame_MultiplayerGame()
        {
            CreateDefaultBoardAndAnna(0);
            var Belle = new Player();
            Anna.Enter(DiceBoard);
            Belle.Enter(DiceBoard);
            Assert.IsTrue(Belle.IsIn(DiceBoard));
        }

        [Test]
        public void EntersGame_SinglePlayer_InGame()
        {
            CreateDefaultBoardAndAnna(0);
            Anna.Enter(DiceBoard);
            Assert.IsTrue(Anna.IsIn(DiceBoard));
        }

        [Test]
        public void ExitGame_PlayerNotInGame_ThrowsException()
        {
            CreateDefaultBoardAndAnna(0);
            Assert.Throws<InvalidOperationException>(Anna.Exit);
        }

        [Test]
        public void ExitGame_Player_NotInGame()
        {
            CreateDefaultBoardAndAnna(0);
            Anna.Enter(DiceBoard);
            Anna.Exit();
            Assert.IsFalse(Anna.IsIn(DiceBoard));
        }

        [Test]
        public void MakeBet_DiceGame_Succeeded()
        {
            CreateDefaultBoardAndAnna(1000);
            Anna.Enter(DiceBoard);
            Anna.MakeBet(3, 1);
            Assert.IsTrue(DiceBoard.BetsBank() == 1);
        }

        [Test]
        public void MakeBet_Exceed6_ThrowsException()
        {
            CreateDefaultBoardAndAnna(1000);
            Anna.Enter(DiceBoard);
            Assert.Throws<ArgumentException>(() => Anna.MakeBet(7, 3));
        }

        [Test]
        public void MakeBet_RoundBegin_ThrowsException()
        {
            CreateDefaultBoardAndAnna(1000);
            Anna.Enter(DiceBoard);
            DiceBoard.BeginRound();

            Assert.Throws<InvalidOperationException>(() => Anna.MakeBet(3, 1));
        }

        [Test]
        public void MakeBet_ZeroRoll_ThrowsException()
        {
            CreateDefaultBoardAndAnna(1000);
            Anna.Enter(DiceBoard);

            Assert.Throws<ArgumentException>(() => Anna.MakeBet(0, 3));
        }
    }

    public class Builder
    {
        public Builder()
        {
            DiceGame = new DiceGameBuilder();
            Player = new PlayerBuilder();
        }

        public DiceGameBuilder DiceGame { get; private set; }
        public PlayerBuilder Player { get; private set; }
    }

    public class PlayerBuilder
    {
        private readonly Player player = new Player();
        private int coins;

        public PlayerBuilder BuyCoins(int new_coins)
        {
            coins += new_coins;
            return this;
        }

        public static implicit operator Player(PlayerBuilder builder)
        {
            if (builder.coins > 0)
            {
                builder.player.BuyCoins(builder.coins);
            }
            return builder.player;
        }
    }

    public class DiceGameBuilder
    {
        private readonly DiceGame game = new DiceGame();

        public static implicit operator DiceGame(DiceGameBuilder builder)
        {
            return builder.game;
        }
    }
}