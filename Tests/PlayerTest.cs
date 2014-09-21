using System;
using Domain;
using NUnit.Framework;

namespace Tests {
    [TestFixture]
    public class PlayerTest {
        private Player player;
        private Game game;
        private const int standartBet = 100;

        [SetUp]
        public void Setup() {
            player = new Player();
            game = new Game();
        }

        [Test]
        public void ByDefault_PlayerNotInGame() {
            bool isPlayerInGame = player.IsIn(game);
            Assert.IsFalse(isPlayerInGame);
        }

        [Test]
        public void EntersGame_EnterToSecondGame_ThrowInvalidOperationException() {
            player.Enter(game);
            var secondGame = new Game();

           Assert.Throws<InvalidOperationException>(() => player.Enter(secondGame)).WithMessage("Нельзя войти во вторую игру");
        }

        [Test]
        public void EntersGame_EntersTheSameGame_ThrowInvalidOperationException() {
            player.Enter(game);

            Assert.Throws<InvalidOperationException>(() => player.Enter(game))
                .WithMessage("Ты уже в этой игре!");
        }

        [Test]
        public void EntersGame_SinglePlayer_InGame() {
            player.Enter(game);

            Assert.IsTrue(player.IsIn(game));
        }

        [Test]
        public void EntersGame_WhereHaveAnotherPlayers_AllPlayersInGame() {
            var secondPlayer = new Player();
            secondPlayer.Enter(game);

            player.Enter(game);

            Assert.AreEqual(player.IsIn(game), secondPlayer.IsIn(game));
        }

        [Test]
        public void EntersGame_WhereHaveAnotherPlayers_InGame() {
            var secondPlayer = new Player();
            secondPlayer.Enter(game);

            player.Enter(game);

            Assert.IsTrue(player.IsIn(game));
        }

        [Test]
        public void Exit_PlayerInGame_ExitFromGame() {
            player.Enter(game);

            player.Exit();

            Assert.IsFalse(player.IsIn(game));
        }

        [Test]
        public void Exit_PlayerNotInGame_ThrowInvalidOperationException() {
            Assert.Throws<InvalidOperationException>(player.Exit)
                .WithMessage("Нельзя просто так выйти из игры не войдя");
        }

        [Test]
        public void BuyChips_PlayerBuy1Chips_PlayerHas1Chips() {
            player.BuyChips(1);

            int playerChipsBallance = player.AvailiableChips();

            Assert.AreEqual(1, playerChipsBallance);
        }

        [Test]
        public void BuyChips_PlayerBuyNegativeChipsCount_ThrowInvalidOperationException() {
            Assert.Throws<InvalidOperationException>(() => player.BuyChips(-1))
                .WithMessage("Нельзя купить количество фишек меньше 1");
        }

        [Test]
        public void BuyChips_PlayerBuyZeroChipsCount_ThrowInvalidOperationException() {
            Assert.Throws<InvalidOperationException>(() => player.BuyChips(0))
                .WithMessage("Нельзя купить количество фишек меньше 1");
        }

        [Test]
        public void MakeBet_PlayerBet1Chips_Bet1Chips() {
            player.Enter(game);
            player.BuyChips(1);

            player.MakeBet(1,1);
            bool hasBet = player.HasBet();

            Assert.True(hasBet);
        }

        [Test]
        public void MakeBet_PlayerBetZero_ThrowInvalidOperationException() {
            player.Enter(game);
            player.BuyChips(100);
            
            Assert.Throws<InvalidOperationException>(() => player.MakeBet(100,0)).WithMessage("Принимаются ставки от 1 до 6");
        }
        [Test]
        public void MakeBet_PlayerBetMoreThenSixChips_ThrowInvalidOperationException(){
            player.Enter(game);
            player.BuyChips(100);
            
            Assert.Throws<InvalidOperationException>(() => player.MakeBet(100,7)).WithMessage("Принимаются ставки от 1 до 6");
        }

        [Test]
        public void MakeBet_BetMoreThetAvailableChips_ThrowInvalidOperationException() {
            int betAmount = 110;
            player.Enter(game);
            player.BuyChips(100);

            Assert.Throws<InvalidOperationException>(() => player.MakeBet(betAmount, 1)).WithMessage("Недостаточно фишек для ставки");
        }
        

        /*[Test]
        public void MakeBet_ChangeBetIfGameNoStart_ThrowInvalidOperationException() {
            int betAmount = 100;
            player.Enter(game);
            player.BuyChips(100);
            player.MakeBet(betAmount,1);

            game.Started();

            Assert.Throws<InvalidOperationException>(() => player.MakeBet(betAmount, 1)).WithMessage("Нельзя менять ставку когда игра запущена");
        }*/
    }
    public static class ExceptionExceptions{
        public static void WithMessage(this Exception ex, string message)
        {
            Assert.AreEqual(message, ex.Message);
        } 
    }
}