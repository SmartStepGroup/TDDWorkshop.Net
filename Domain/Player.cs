using System;

namespace Domain
{
    public class Player
    {
        private Game activeGame;
        private int chips = 0;
        private int face;
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

        public int getFace()
        {
            return face;
        }

        public void makeBet(int face, int chips)
        {
            if (activeGame == null)
            {
                throw new InvalidOperationException("Чтобы далить ставки нужно чтобы игрок был в игре");    
            }
            if (face < 1 || face > 6) 
            {
                throw new InvalidOperationException("Ставка должна быть от 1 до 6");
            }
            if (chips > chipsCount())
            {
                throw new InvalidOperationException("Ставка превышает количество фишек");
            }
            this.bet = chips;
            this.face = face;
            this.chips -= chips;
        }

        public int getBet()
        {
            return bet;
        }

        public void playGame()
        {
            chips+=activeGame.play(bet, face);
            bet = 0;
        }
    }
}