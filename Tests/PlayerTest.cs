using System;
using Domain;
using NUnit.Framework;
using Tests.DSL;

namespace Tests
{
    [TestFixture]
    public class PlayerTest
    {
        [Test]
        public void EnterGame_Player_InGame()
        {
            Player player = new Casino().Player();
            Game game = new Casino().Game();

            player.enterGame(game);

            Assert.IsTrue(player.isInGame());
        }

        [Test]
        public void Default_Player_NotInGame()
        {
            Player player = new Casino().Player();

            Assert.IsFalse(player.isInGame());
        }

        [Test]
        public void ExitGame_Player_NotInGame()
        {
            Player player = new Casino().Player().enterGame();

            player.exitGame();

            Assert.IsFalse(player.isInGame());
        }

        [Test]
        public void ExitGame_PlayerNotInGame_ThrowInvalidOperationException()
        {
            Player player = new Casino().Player();
            Assert.Throws<InvalidOperationException>(() => player.exitGame()).withMessage("Войдите в игру, прежде чем выходить.");
        }

        [Test]
        public void EnterGame_PlayerInGame_ThrowInvalidOperationException()
        {
            var player = new Player();
            var game = new Game();
            
            player.enterGame(game);

            var e = Assert.Throws<InvalidOperationException>(() => player.enterGame(game)); 
            Assert.AreEqual("Можно играть только в одну игру", e.Message);
        }

        [Test]
        public void EnterGame_WithPlayers_Success()
        {
            var game = new Game();
            var player1 = new Player();
            var player2 = new Player();

            player1.enterGame(game);
            player2.enterGame(game);

            Assert.IsTrue(player2.isInGame());
        }

        [Test]
        public void BuyChips_Player_Success()
        {
            var player = new Player();

            player.buyChips(5);

            Assert.AreEqual(5, player.chipsCount());
        }

        [Test]
        public void Default_Player_ZeroChips()
        {
            var player = new Player();

            Assert.AreEqual(0, player.chipsCount());
        }

        [Test]
        public void Default_Player_ZeroBet()
        {
            var player = new Player();

            Assert.AreEqual(0, player.getFace());
            Assert.AreEqual(0, player.getBet());
        }

        [Test]
        public void MakeBet_PlayerInGame_Success()
        {
            var player = new Player();
            var game = new Game();
            player.buyChips(10);
            player.enterGame(game);

            player.makeBet(5, 3);
            
            Assert.AreEqual(5, player.getFace());
            Assert.AreEqual(3, player.getBet());
        }

        [Test]
        public void MakeBet_PlayerNotInGame_ThrowInvalidOperationException()
        {
            var player = new Player();

            var e = Assert.Throws<InvalidOperationException>(() => player.makeBet(5,3));
            Assert.AreEqual("Чтобы далить ставки нужно чтобы игрок был в игре", e.Message);
        }

        [Test]
        public void MakeBetLessOne_PlayerInGame_ThrowsInvalidOperationException()
        {
            var player = new Player();
            var game = new Game();

            player.enterGame(game);

            var e = Assert.Throws<InvalidOperationException>(() => player.makeBet(-1,3));
            Assert.AreEqual("Ставка должна быть от 1 до 6", e.Message);
        }

        [Test]
        public void MakeBetMoreSix_PlayerInGame_ThrowsInvalidOperationException()
        {
            var player = new Player();
            var game = new Game();

            player.enterGame(game);

            var e = Assert.Throws<InvalidOperationException>(() => player.makeBet(7, 3));
            Assert.AreEqual("Ставка должна быть от 1 до 6", e.Message);
        }

        [Test]
        public void MakeBetMoreThanHave_PlayerInGame_ThrowsInvalidOperationException()
        {
            var player = new Player();
            var game = new Game();
            player.buyChips(10);
            player.enterGame(game);
            var e = Assert.Throws<InvalidOperationException>(() => player.makeBet(4, 100));
            Assert.AreEqual("Ставка превышает количество фишек", e.Message);           
        }

        [Test]
        public void MakeBet_PlayerNoChips_ThrowsInvalidOperationException()
        {
            
        }
    }
}