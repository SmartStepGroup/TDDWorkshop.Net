using System;
using System.Dynamic;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
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
            Create = new MainFather(new GameSon(), new PlayerSon());
            

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

        /*[Test]
        public void MakeBet_BetMoreThetAvailableChips_ThrowInvalidOperationException() {
            int betAmount = 110;
            player.Enter(game);
            player.BuyChips(100);

            Assert.Throws<InvalidOperationException>(() => player.MakeBet(betAmount, 1)).WithMessage("Недостаточно фишек для ставки");
        }
        [Test]
        public void MakeBet_ChangeBetIfGameNoStart_ThrowInvalidOperationException() {
            int betAmount = 100;
            player.Enter(game);
            player.BuyChips(100);
            player.MakeBet(betAmount,1);

            game.Started();

            Assert.Throws<InvalidOperationException>(() => player.MakeBet(betAmount, 1)).WithMessage("Нельзя менять ставку когда игра запущена");
        }*/


        [Test]
        public void MakeBet_BetMoreThetAvailableChips_ThrowInvalidOperationException(){
            int betAmount = 110;
            Game dslGame = Create.Game;
            Player dslPlayer = Create.Player
                .In(dslGame)
                .With(100.Chips())
                .WithBet(100.Chips().On(1));
            Assert.Throws<InvalidOperationException>(() => dslPlayer.MakeBet(betAmount, 1)).WithMessage("Недостаточно фишек для ставки");
        }

        public MainFather Create { get; private set; }

        
    }


    public class MainFather {
        public MainFather(GameSon game, PlayerSon player) {
            Player = player;
            Game = game;
        }

        public GameSon Game { get; private set; }
        public PlayerSon Player { get; private set; }
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

    public class Bet {
        public Bet(int chips, int score) {
            Score = score;
            Chips = chips;
        }
        public int Chips { get; private set; }
        public int Score { get; private set; }
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