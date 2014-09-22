namespace Tests
{
    public class Casino
    {
        private int winnedChips;

        public int GetCountChips()
        {
            return winnedChips;
        }

        public void AddWinChips(int chipsCount)
        {
            winnedChips += chipsCount;
        }
    }
}