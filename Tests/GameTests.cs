using System;
using Domain;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class GameTests
    {
        private Player player;
        private Game game;
        [SetUp]
        public void setup()
        {
            player = new Player();
            game = new Game();
        }
        [Test]
        public void ByDefault_NoPlayersInGame()
        {
            Assert.AreEqual(0, game.PlayersCount);
        }
        
        
    }
}
