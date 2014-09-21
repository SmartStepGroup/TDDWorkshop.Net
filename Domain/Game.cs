namespace Domain
{
    public class Game
    {
        public int playerCount = 0;

        public void Join(Player player)
        {
            playerCount++;
        }
    }
}