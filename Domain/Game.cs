using System;
using FakeItEasy;

namespace Tests
{
    public class Game
    {
        private int playerCount;
        public bool isStarted;
        public int luckyScore;
        public IDice dice;

        public Game()
        {
            playerCount = 0;
            isStarted = false;
        }


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

        public void Start(Player player = null, Casino casino = null)
        {
            isStarted = true;
            if (player == null) return;
            if (!player.HasBets()) throw new InvalidOperationException("Нельзя начать игру без ставок");
            if (casino == null) throw new InvalidOperationException("Нельзя начать игру без казино");
            if (dice == null) return;
            int score = dice.Roll();

            foreach (var bet in player.GetBets())
            {
                if (bet.DiceValue == score)
                {
                    player.BuyChips(bet.ChipsCount * 6);
                }
                else
                {
                    casino.AddWinChips(bet.ChipsCount);
                }                
            }
        }



    }
}
