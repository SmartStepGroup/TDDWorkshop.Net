using Domain;

namespace Tests
{
    public class PlayerBuilder
    {
        private readonly Player player = new Player();
        private int coins;

        public PlayerBuilder BuyCoins(int new_coins)
        {
            coins += new_coins;
            return this;
        }

        public static implicit operator Player(PlayerBuilder builder)
        {
            if (builder.coins > 0)
            {
                builder.player.BuyCoins(builder.coins);
            }
            return builder.player;
        }
    }
}