using Domain;

namespace Tests
{
    public class DiceGameBuilder
    {
        private readonly DiceGame game = new DiceGame();

        public static implicit operator DiceGame(DiceGameBuilder builder)
        {
            return builder.game;
        }
    }
}