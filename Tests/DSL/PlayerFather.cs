namespace Tests.DSL
{
    public class PlayerFather
    {
        private Player player = new Player();

        public PlayerFather In(Game game)
        {
            player.Enter(game);
            return this;
        }

        public static implicit operator Player(PlayerFather father)
        {
            return father.player;
        }

        public PlayerFather WithBet(Bet bet)
        {
            player.DoBet(bet);
            return this;
        }

        public PlayerFather WithChips(int chipsCount)
        {
            player.BuyChips(chipsCount);
            return this;
        }
    }
}