namespace Domain
{
    public class Bet
    {
        public Bet(int chips, int score)
        {
            Score = score;
            Chips = chips;
        }
        public int Chips { get; private set; }
        public int Score { get; private set; }
    }
}
