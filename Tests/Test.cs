using Domain;

namespace Tests
{
    public abstract class Test
    {
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