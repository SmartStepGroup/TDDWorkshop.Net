using System.Collections.Generic;

namespace Domain
{
    public class Game
    {
        public int playerCount = 0;
        private bool _IsStart = false;
        private List<Player> PlayersList = new List<Player>();
        private int _result = 0;

        public void Join(Player player)
        {
            playerCount++;
        }

        public bool IsStart()
        {
            return _IsStart;
        }

        public void Start(Casino casino)
        {
            _IsStart = true;
            _result = GetGenerateResult();
            foreach (var player in PlayersList)
            {
                foreach (var bet in player.GetListBet())
                {
                    if (_result == bet.GetScore())
                    player.AddChips(bet.GetSize()*6);
                    else
                    {
                        casino.AddChips(bet.GetSize());
                    }
                }
            }
        }

        private int GetGenerateResult()
        {
            return 6;
        }

        public int GetResult()
        {
            return _result;
        }

        public void AddPlayer(Player player)
        {
            PlayersList.Add(player);
        }
    }
}