using System;
using NUnit.Framework;
using Tests.DSL;

namespace Tests
{
    [TestFixture]
    public class CasinoTests : BaseTest
    {
        [SetUp]
        public void SetUp()
        {
            Create = new Father();
        }
        [Test]
        public void CasinoIsNull_Casino_ThrowInvalidException()
        {
            Game game = Create.Game.WithLuckyScore(6);
            Player player = Create.Player.In(game).WithChips(200).WithBet(100.Chips().On(1));
            Casino casino = Create.Casino;

            Assert.Throws<InvalidOperationException>(() => game.Start(player, null)).WithMessage("Нельзя начать игру без казино");
        }

        [Test]
        public void PlayerLose_Casino_WinChips()
        {
            Game game = Create.Game.WithLuckyScore(6);
            Player player = Create.Player.In(game).WithChips(200).WithBet(100.Chips().On(1));
            Casino casino = Create.Casino;

            game.Start(player, casino);

            Assert.AreEqual(100, casino.GetCountChips());
        }
    }
}