using Domain;

namespace Tests
{
    public class CasinoBuilder
    {
        private Casino casino = new Casino();
        private int balance = 0;
        internal CasinoBuilder WithBalance(int p)
        {
            balance = 0;
            return this;
        }

        public static implicit operator Casino(CasinoBuilder builder)
        {
            if (builder.balance != 0)
            {
                builder.casino.Balance = builder.balance;
            }
            return builder.casino;
        }
    }
}