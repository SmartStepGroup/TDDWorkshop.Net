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

            player.Enter();

            Assert.IsTrue(player.IsIn(game));
        }
    }

    public class Game
    {
    }

    public class Player
    {
        public void Enter()
        {
        }

        public bool IsIn(Game game)
        {
            return true;
        }
    }
}