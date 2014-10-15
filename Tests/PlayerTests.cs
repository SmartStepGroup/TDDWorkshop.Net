using System;
using Domain;
using NUnit.Framework;

namespace Tests {
    [TestFixture]
    public class PlayerTests {
        [Test]
        public void EntersGame_SinglePlayer_InGame() {
            var player = CreatePlayer();
            var game = CreateGame();

            player.Enter(game);

            Assert.IsTrue(player.IsIn(game));
        }

        [Test]
        public void ByDefault_Player_NotInGame() {
            var player = new Player();
            var game = new Game();

            Assert.IsFalse(player.IsIn(game));
        }

        [Test]
        public void Exit_PlayerInGame_NotInGame() {
            var player = new Player();
            var game = new Game();
            player.Enter(game);

            player.Exit();

            Assert.IsFalse(player.IsIn(game));
        }

        [Test]
        public void Exit_PlayerNotInGame_ThrowsInvalidOperationException() {
            var player = new Player();

            var e = Assert.Throws<InvalidOperationException>(player.Exit);
            Assert.AreEqual("Нельзя выйти из игры, не войдя", e.Message);
        }

        [Test]
        public void Exit_PlayerNotInGame_ThrowsInvalidOperationException2() {
            var player = new Player();

            var e = Assert.Throws<InvalidOperationException>(player.Exit);
            Assert.AreEqual("Нельзя выйти из игры, не войдя", e.Message);
        }

        [Test]
        public void ByDefault_HasNoChipsAvailable() {
            var player = new Player();

            Assert.AreEqual(0, player.AvailableChips);
        }

        [Test]
        public void BuyChips_OneChip_MakesOneChipAvailable() {
            var player = new Player();

            player.BuyChips(1);

            Assert.AreEqual(1, player.AvailableChips);
        }

        [Test]
        public void BuyChips_TwoTimes_AddsChipsAvailable() {
            var player = new Player();

            player.BuyChips(1);
            player.BuyChips(1);

            Assert.AreEqual(1 + 1, player.AvailableChips);
        }

        [Test]
        public void ByDefault_HasNoBets() {
            var player = new Player();

            Assert.IsNull(player.CurrentBet);
        }

        [Test]
        public void Bet_BeforeJoiningGame_ThrowsInvalidOperationException() {
            var player = new Player();

            Assert
                .Throws<InvalidOperationException>(
                    () => player.Bet(CreateBet(score: 1)))
                .WithMessage("Please join the game before making a bet");
        }

        [Test]
        public void Join_Game_SetsActiveGame() {
            var player = new Player();
            var game = new Game();

            player.Enter(game);

            Assert.AreEqual(game, player.ActiveGame);
        }

        [Test]
        public void Bet_PlayerInGame_SetsBet() {
            var player = CreatePlayerInGame();
            player.BuyChips(1);

            var bet = CreateBet(score: 1);
            player.Bet(bet);

            Assert.AreEqual(bet, player.CurrentBet);
        }

        [Test]
        public void Bet_ScoreLessThan1_IsNotAllowed() {
            var player = CreatePlayerInGame();

            var bet = CreateBet(score: -1);

            Assert.Throws<InvalidOperationException>(() => player.Bet(bet));
        }

        [Test]
        public void Bet_ScoreMoreThan6_IsNotAllowed() {
            var player = CreatePlayerInGame();

            var bet = CreateBet(score: 7);

            Assert.Throws<InvalidOperationException>(() => player.Bet(bet));
        }

        [Test]
        public void Bet_MoreThanChipsAvailable_IsNotAllowed() {
            var player = CreatePlayerInGame();
            player.BuyChips(1);

            var bet = CreateBet(chips: 2);

            Assert.Throws<InvalidOperationException>(() => player.Bet(bet));
        }

        [Test]
        public void ReplaceBet_WithExistingBet_ReplacesBet() {
            var player = CreatePlayerInGame();
            player.BuyChips(100);
            var bet = CreateBet(chips: 1, score: 1);

            player.ReplaceBet(CreateBet(chips:2, score: 2));

            Assert.AreEqual(CreateBet(chips: 2, score: 2), player.CurrentBet);

        }

        private Bet CreateBet(int chips = 1, int score = 1) {
            return new Bet(chips, score);
        }

        private static Player CreatePlayerInGame() {
            var player = new Player();
            var game = new Game();
            player.Enter(game);
            return player;
        }

        private static Player CreatePlayer() {
            return new Player();
        }

        private static Game CreateGame() {
            return new Game();
        }
    }

    public static class ExceptionExtensions {
        public static void WithMessage(this Exception e, string expectedMessage) {
            Assert.AreEqual(expectedMessage, e.Message);
        }


        public static DateTime of2014(this double date) {
            var day = (int) Math.Floor(date);
            var month = (int) (Math.Round((date - day)*100));
            return new DateTime(2014, month, day);
        }
    }
}