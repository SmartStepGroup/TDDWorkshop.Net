namespace Domain {
    public class Bet {
        public int Amount { get; private set; }
        public int Score { get; private set; }

        public Bet(int amount, int score) {
            Amount = amount;
            Score = score;
        }

    }
}