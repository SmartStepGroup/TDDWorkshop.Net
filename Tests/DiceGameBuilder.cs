using Domain;

namespace Tests
{
    public class DiceGameBuilder
    {
        private readonly DiceGame game = new DiceGame();
        private Casino gameOwner;
        public DiceGameBuilder In(Casino owner)
        {
            gameOwner = owner;
            return this;
        }

        public static implicit operator DiceGame(DiceGameBuilder builder)
        {
            if (builder.gameOwner != null)
            {
                builder.game.Casino = builder.gameOwner;
            }
            return builder.game;
        }
    }
}