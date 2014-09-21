using System.Security.Policy;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class PlayerTests
    {
        [Test]
        public void EnterGame_Player_InGame()
        {
            var player = new Player();
            var game = new Game();

            player.Enter(game);

            Assert.IsTrue(player.IsIn(game));
        }

        [Test]
        public void ExitGame_Player_NotInGame()
        {
            var player = new Player();
            var game = new Game();

            player.Exit();

            Assert.IsFalse(player.IsIn(game));
        }
    }
}