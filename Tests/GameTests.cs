using System;
using System.Dynamic;
using Domain;
using NUnit.Framework;

namespace Tests {
    public abstract class Test {
        protected MainFather Create;
        [SetUp]
        public void Setup() {
            Create = new MainFather();
        }
    }

    [TestFixture]
    public class GameTests : Test {
        [Test]
        public void ByDefault_NoPlayersInGame() {
            Game game = Create.Game;
            bool hasPlayers = game.HasPlayers();
            Assert.IsFalse(hasPlayers);
        }
        


        
    }
}