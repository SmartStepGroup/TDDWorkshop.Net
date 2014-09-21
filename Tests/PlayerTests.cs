using System;
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
            var player = CreatePlayer();
            var game = CreateGame();

            player.Enter(game);

            Assert.IsTrue(player.IsIn(game));
        }

        [Test]
        public void ByDefault_Player_NotInGame()
        {


            var player = new Player();
            var game = new Game();

            Assert.IsFalse(player.IsIn(game));
        }

        [Test]
        public void Exit_PlayerInGame_NotInGame()
        {
            var player = new Player();
            var game = new Game();
            player.Enter(game);

            player.Exit();

            Assert.IsFalse(player.IsIn(game));
        }

        [Test]
        public void Exit_PlayerNotInGame_ThrowsInvalidOperationException()
        {
            var player = new Player();
            
            var e = Assert.Throws<InvalidOperationException>(player.Exit);
            Assert.AreEqual("Нельзя выйти из игры, не войдя", e.Message);
        }

        [Test]
        public void ByDefault_HasNoChipsAvailable()
        {
            var player = new Player();

            Assert.AreEqual(0, player.AvailableChips);
        }

        [Test]
        public void BuyChips_OneChip_MakesOneChipAvailable()
        {
            var player = new Player();

            player.BuyChips(1);

            Assert.AreEqual(1, player.AvailableChips);
        }

        [Test]
        public void BuyChips_TwoTimes_AddsChipsAvailable()
        {
            var player = new Player();

            player.BuyChips(1);
            player.BuyChips(1);

            Assert.AreEqual(1 + 1, player.AvailableChips);
        }

        [Test]
        public void ByDefault_HasNoBets()
        {
            var player = new Player();

            Assert.False(player.HasBets);
        }

        [Test]
        public void Bet_BeforeJoiningGame_ThrowsInvalidOperationException()
        {
            var player = new Player();

            Assert.Throws<InvalidOperationException>(() => player.Bet(1)).WithMessage("Please join the game before making a bet");
        }

        private static Player CreatePlayer()
        {
            return new Player();
        }

        private static Game CreateGame()
        {
            return new Game();
        }



        [Test]
        public void DateTimeTest() {
            Assert.AreEqual(new DateTime(2014, 09, 21), 21.09.of2014());
            Assert.AreEqual(new DateTime(2014, 09, 01), 01.09.of2014());
            Assert.AreEqual(new DateTime(2014, 01, 01), 1.01.of2014());
        }
    }

    public static class ExceptionExtensions {
        public static void WithMessage(this Exception e, string expectedMessage) {
            Assert.AreEqual(expectedMessage, e.Message);
        }


        public static DateTime of2014(this double date) {
            int day = (int) Math.Floor(date);
            int month = (int) (Math.Round((date - day) * 100));
            return new DateTime(2014, month, day);
        }
    }
}