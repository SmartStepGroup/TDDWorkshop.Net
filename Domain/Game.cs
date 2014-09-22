using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    public class Game
    {
        private int playerCount = 0;
        public bool isStarted = false;

        public void Join()
        {
            playerCount++;
        }

        public void Leave()
        {
            playerCount--;
        }

        public void CheckMaxPlayers()
        {
            if (playerCount == 6) throw new InvalidOperationException("В игре число игроков максимальное, юный падован"); ;
        }

        public void Start()
        {
            isStarted = true;
        }

    }
}
