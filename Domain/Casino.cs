namespace Domain
{
    public class Casino
    {
        private static int _Balance = 0;

        public static int GetBalance()
        {
            return _Balance;
        }

        public void AddChips(int size)
        {
            _Balance += size;
        }
    }
}