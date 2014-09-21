namespace Domain
{
    public class Game
    {
        public int playerCount = 0;
        private bool _IsStart = false;

        public void Join(Player player)
        {
            playerCount++;
        }

        public bool IsStart()
        {
            return _IsStart;
        }

        public void Start()
        {
            _IsStart = true;
        }
    }
}