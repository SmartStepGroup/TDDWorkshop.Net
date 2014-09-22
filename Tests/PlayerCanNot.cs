using System;
using Domain;
using NUnit.Framework;

namespace Tests {
    [TestFixture]
    public class PlayerCanNot : Test {
        [Test]
        public void ExitGameWhenHasNotActiveGame() {
            Player player = CreatePlayer();

            var e = Assert.Throws<InvalidOperationException>(() => { player.exit(); });
            Assert.IsTrue(e.Message.Equals("Нельзя выйти из игры, если в неё не входил"));
        }
    }
}