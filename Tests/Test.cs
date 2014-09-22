using Domain;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public abstract class Test
    {

        protected Father Create;

        [SetUp]
        public void setup()
        {
            Create = new Father();
        }

        protected static Game CreateGame()
        {
            return new Game();
        }

        protected static Player CreatePlayer()
        {
            return new Player();
        }
    }
}