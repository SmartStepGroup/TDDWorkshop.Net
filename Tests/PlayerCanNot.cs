using System;
using Domain;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class PlayerCanNot
    {
        [Test]
        public void ExitGameWhenHasNotActiveGame()
        {
            Player player = new Player();
            Game game = new Game();

            player.setActiveGame(game);

            var e = Assert.Throws<InvalidOperationException>(() => { player.exit();});
        }
         
    }
}