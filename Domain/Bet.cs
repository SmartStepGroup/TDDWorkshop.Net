namespace Domain
{
    public class Bet
    {
        public Player player;
        public int roll;
        public int coins;

        public Bet(Player player, int roll, int coins)
        {
            this.player = player;
            this.roll = roll;
            this.coins = coins;
        }

    
    }
}