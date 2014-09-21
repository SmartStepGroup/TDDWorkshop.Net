using System;

namespace Domain
{
    public class Player
    {
        private Game activeGame;
        private int chips = 0;
        private int bet;

        public bool isInGame()
        {
            return activeGame != null;
        }

        public void enterGame(Game game)
        {
            if (activeGame != null)
            {
                throw new InvalidOperationException("Можно играть только в одну игру");
            }
                
            activeGame = game;
        }

        public void exitGame()
        {
            if (activeGame == null)
            {
                throw new InvalidOperationException("Войдите в игру, прежде чем выходить.");
            }

            activeGame = null;
        }

        public void buyChips(int i)
        {
            chips = i;
        }

        public int chipsCount()
        {
            return chips;
        }

        public int betCount()
        {
            return bet;
        }

        public void makeBet(int i)
        {
            if (activeGame == null)
            {
                throw new InvalidOperationException("Чтобы далить ставки нужно чтобы игрок был в игре");    
            }
            if (i < 1 || i > 6) 
            {
                throw new InvalidOperationException("Ставка должна быть от 1 до 6");
            }


            bet = i;
        }
    }
}