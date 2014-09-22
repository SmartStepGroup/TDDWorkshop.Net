using System;
using System.Dynamic;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using Domain;
using NUnit.Framework;

namespace Tests {
    [TestFixture]
    public class PlayerTest : Test {
        
        [SetUp]
        public void Setup() {
            base.Setup();
        }

        [Test]
        public void ByDefault_PlayerNotInGame() {
            Game game = Create.Game;
            Player player = Create.Player;

            bool isPlayerInGame = player.IsIn(game);

            Assert.IsFalse(isPlayerInGame);
        }

        [Test]
        public void EntersGame_EnterToSecondGame_ThrowInvalidOperationException() {
            Game game = Create.Game;
            Player player = Create.Player.In(game);
            Game secondGame = Create.Game;

           Assert.Throws<InvalidOperationException>(() => player.Enter(secondGame)).WithMessage("Нельзя войти во вторую игру");
        }

        [Test]
        public void EntersGame_EntersTheSameGame_ThrowInvalidOperationException() {
            Game game = Create.Game;
            Player player = Create.Player.In(game);

            Assert.Throws<InvalidOperationException>(() => player.Enter(game))
                .WithMessage("Ты уже в этой игре!");
        }

        [Test]
        public void EntersGame_SinglePlayer_InGame() {
            Game game = Create.Game;
            Player player = Create.Player.In(game);

            Assert.IsTrue(player.IsIn(game));
        }

        [Test]
        public void EntersGame_WhereHaveAnotherPlayers_FirstPlayerInGame() {
            Game game = Create.Game;
            Player player = Create.Player.In(game);
            Player secondPlayer = Create.Player;

            secondPlayer.Enter(game);

            Assert.True(player.IsIn(game));
        }

        [Test]
        public void Exit_PlayerInGame_ExitFromGame() {
            Game game = Create.Game;
            Player player = Create.Player.In(game);

            player.Exit();

            Assert.IsFalse(player.IsIn(game));
        }

        [Test]
        public void Exit_PlayerNotInGame_ThrowInvalidOperationException() {
            Game game = Create.Game;
            Player player = Create.Player;

            Assert.Throws<InvalidOperationException>(player.Exit)
                .WithMessage("Нельзя просто так выйти из игры не войдя");
        }

        [Test]
        public void BuyChips_PlayerBuy1Chips_PlayerHas1Chips() {
            Game game = Create.Game;
            Player player = Create
                .Player
                .In(game)
                .With(100.Chips());

            int playerChipsBallance = player.AvailiableChips();

            Assert.AreEqual(100.Chips(), playerChipsBallance);
        }

        [Test]
        public void BuyChips_PlayerBuyNegativeChipsCount_ThrowInvalidOperationException() {
            Game game = Create.Game;
            Player player = Create.Player.In(game);

            Assert.Throws<InvalidOperationException>(() => player.BuyChips(-1.Chips()))
                .WithMessage("Нельзя купить количество фишек меньше 1");
        }

        [Test]
        public void BuyChips_PlayerBuyZeroChipsCount_ThrowInvalidOperationException() {
            Game game = Create.Game;
            Player player = Create.Player.In(game);

            Assert.Throws<InvalidOperationException>(() => player.BuyChips(0.Chips()))
                .WithMessage("Нельзя купить количество фишек меньше 1");
        }

        [Test]
        public void MakeBet_PlayerBet1Chips_Bet1Chips() {
            Game game = Create.Game;
            Player player = Create
                .Player
                .In(game)
                .With(100.Chips());
            player.MakeBet(100.Chips(),1);
            
            bool hasBet = player.HasBet();

            Assert.True(hasBet);
        }

        [Test]
        public void MakeBet_PlayerBetZero_ThrowInvalidOperationException() {
            Game game = Create.Game;
            Player player = Create
                .Player
                .In(game)
                .With(100.Chips());

            Assert.Throws<InvalidOperationException>(() => player.MakeBet(100.Chips(),0)).WithMessage("Принимаются ставки от 1 до 6");
        }
        [Test]
        public void MakeBet_PlayerBetMoreThenSixChips_ThrowInvalidOperationException() {
            Game game = Create.Game;
            Player player = Create
                .Player
                .In(game)
                .With(100.Chips());
            Assert.Throws<InvalidOperationException>(() => player.MakeBet(100.Chips(),7)).WithMessage("Принимаются ставки от 1 до 6");
        }

        [Test]
        public void MakeBet_BetMoreThetAvailableChips_ThrowInvalidOperationException(){
            int betAmount = 110;
            Game dslGame = Create.Game;
            Player dslPlayer = Create.Player
                .In(dslGame)
                .With(100.Chips())
                .WithBet(100.Chips().On(1));
            Assert
                .Throws<InvalidOperationException>(() => dslPlayer.MakeBet(betAmount, 1))
                .WithMessage("Недостаточно фишек для ставки");
        }

    }

    

    public class MainFather {
        public GameSon Game {
            get {
                return new GameSon();
            }
        }
        public PlayerSon Player {
            get {
                return new PlayerSon();
            }
        }
    }

    public class GameSon {
        private Game game = new Game();
        public static implicit operator Game(GameSon gameSon)
        {
            return new Game();
        }
    }

    public class PlayerSon {
        private Player player = new Player();
        public PlayerSon In(Game game) {
            player.Enter(game);
            return this;
        }

        public PlayerSon With(int chips) {
            player.BuyChips(chips);
            return this;
        }

        public PlayerSon WithBet(Bet bet) {
            player.MakeBet(bet.Chips, bet.Score);
            return this;
        }

        public static implicit operator Player(PlayerSon playerSon) {
            return playerSon.player;
        }
    }

    public static class IntExtension {
        public static int Chips(this int chips) {
            return chips;
        }

        public static Bet On(this int chips, int face) {
            return new Bet(chips,face);
        }
    }
   

    public static class ExceptionExceptions{
        public static void WithMessage(this Exception ex, string message)
        {
            Assert.AreEqual(message, ex.Message);
        } 
    }
}