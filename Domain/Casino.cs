namespace Domain
{
    public class Casino
    {

        public int Balance { get; set; }

        public void AcquireCoinsFromLoosedBet(int coins)
        {
            Balance += coins;
        }
    }
}