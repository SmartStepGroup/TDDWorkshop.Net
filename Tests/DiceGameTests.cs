using System;
using Domain;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class DiceGameTests
    {
        [Test]
        public void Players_Count_NotExceeded6()
        {
            var game = new DiceGame();
            var player = new Player();
            Assert.IsTrue(game.CanJoin(player));
        }

        [Test]
        public void Players_Exceeded6_ThrowsException()
        {
            var game = new DiceGame();
            for (int i = 1; i <= 6; i++)
            {
                var player = new Player();
                player.Enter(game);
            }

            Assert.Throws<InvalidOperationException>(() => new Player().Enter(game));
        }
    }
}