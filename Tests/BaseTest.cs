namespace Tests
{
    public class BaseTest
    {
        public Game CreateGame()
        {
            return new Game();
        }

        public Player CreatePlayer()
        {
            return new Player();
        }

        public Bet CreateBet(int diceValue = 1, int chipCount = 0)
        {
            return new Bet(diceValue, chipCount);
        }
    }
}