using System;
using System.Runtime.CompilerServices;
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

        private void CreateDefaultBoardAndAnna(int coins = 0)
        {
            DiceBoard = Create.DiceGame;
            Anna = Create.Player
                .BuyCoins(coins);
        }

        protected Builder Create;

        [Test]
        public void Can_SinglePlayer_BuyCoins()
        {
            CreateDefaultBoardAndAnna();
            Anna.BuyCoins(1);
            Assert.AreEqual(1, Anna.GetAvailableCoins());
        }

        [Test]
        public void CancelAllBets_Succeeded()
        {
            CreateDefaultBoardAndAnna(1000);
            Anna.Enter(DiceBoard);
            Anna.MakeBet(2.Edge(), 10.Coins());
            Anna.MakeBet(3.Edge(), 20.Coins());
            Anna.CancelAllBets();
            Assert.IsFalse(Anna.HasAnyBet());
        }

        [Test]
        public void CannotEnter_SinglePlayer_TwoGames()
        {
            CreateDefaultBoardAndAnna();
            Anna.Enter(DiceBoard);
            var game2 = new DiceGame();
            Assert.Throws<InvalidOperationException>(() => Anna.Enter(game2));
        }

        [Test]
        public void EnterGame_SecondPlayerNotInGame_SecondPlayerInGame()
        {
            CreateDefaultBoardAndAnna();
            Player Belle = Create.Player;
            Anna.Enter(DiceBoard);
            Belle.Enter(DiceBoard);
            Assert.IsTrue(Belle.IsIn(DiceBoard));
        }

        [Test]
        public void EntersGame_SinglePlayer_InGame()
        {
            CreateDefaultBoardAndAnna();
            Anna.Enter(DiceBoard);
            Assert.IsTrue(Anna.IsIn(DiceBoard));
        }

        [Test]
        public void ExitGame_PlayerNotInGame_ThrowsException()
        {
            CreateDefaultBoardAndAnna();
            Assert.Throws<InvalidOperationException>(Anna.Exit);
        }

        [Test]
        public void ExitGame_Player_NotInGame()
        {
            CreateDefaultBoardAndAnna();
            Anna.Enter(DiceBoard);
            Anna.Exit();
            Assert.IsFalse(Anna.IsIn(DiceBoard));
        }

        [Test]
        public void MakeBet_DiceGame_Succeeded()
        {
            CreateDefaultBoardAndAnna(1000.Coins());
            Anna.Enter(DiceBoard);
            Anna.MakeBet(3.Edge(), 1.Coins());
            Assert.AreEqual(DiceBoard.BetsBank(), 1);
        }

        [Test]
        public void MakeBet_Exceed6_ThrowsException()
        {
            CreateDefaultBoardAndAnna(1000.Coins());
            Anna.Enter(DiceBoard);
            Assert.Throws<ArgumentException>(() => Anna.MakeBet(7.Edge(), 3.Coins()));
        }

        [Test]
        public void MakeBet_RoundBegin_ThrowsException()
        {
            CreateDefaultBoardAndAnna(1000.Coins());
            Anna.Enter(DiceBoard);
            DiceBoard.BeginRound();

            Assert.Throws<InvalidOperationException>(() => Anna.MakeBet(3.Edge(), 1.Coins()));
        }

        [Test]
        public void MakeBet_ZeroRoll_ThrowsException()
        {
            CreateDefaultBoardAndAnna(1000.Coins());
            Anna.Enter(DiceBoard);

            Assert.Throws<ArgumentException>(() => Anna.MakeBet(0.Edge(), 3.Coins()));
        }

        [Test]
        public void MakeBet_Roll_LooseBet()
        {
            CreateDefaultBoardAndAnna(1000.Coins());
            IDice dice = Create.UnluckyDice;
            Anna.Enter(DiceBoard);

            Anna.MakeBet(1.Edge(), 100.Coins());
            DiceBoard.BeginRound();

            Assert.AreNotEqual(DiceBoard.Roll(dice), 1.Edge());
        }

        [Test]
        public void MakeCorrectBet_Player_Wins()
        {
            CreateDefaultBoardAndAnna(100.Coins());
            IDice dice = Create.LuckyDice;
          
            Anna.Enter(DiceBoard);
            int betAmount = 10;
            Anna.MakeBet(6.Edge(),betAmount.Coins());
            DiceBoard.BeginRound();
            Assert.AreEqual(6,DiceBoard.Roll(dice));
        }


        [Test]
        public void MakeCorrectBet_Player_Wins6Times()
        {
            int firstCoins = 100;
            CreateDefaultBoardAndAnna(firstCoins.Coins());
            IDice dice = Create.LuckyDice;
            int winnerFactor = 6;
            Anna.Enter(DiceBoard);
            int betAmount = 10;
            Anna.MakeBet(6.Edge(), betAmount.Coins());
            DiceBoard.BeginRound();
            DiceBoard.Roll(dice);

            Assert.AreEqual(firstCoins-betAmount +betAmount* winnerFactor, Anna.GetAvailableCoins());
        }



    }

    public static class CoinsExtension
    {
        public static int Coins(this int value)
        {
            return value;
        }
    }

    public static class DiceExtension
    {
        public static int Edge(this int value)
        {
            return value;
        }
    }
}