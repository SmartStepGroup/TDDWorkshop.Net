using Domain;

namespace Tests
{
    internal abstract class Test
    {
        protected Player CreatePlayer()
        {
            return new Player();
        }

        protected Game CreateGame()
        {
            return new Game();
        }
    }
}