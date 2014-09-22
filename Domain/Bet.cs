using System;

namespace Domain
{
    public class Bet
    {
        private int _size = 0;
        private int _score = 0;

        public Bet(int size, int score)
        {
            _size = size;
            _score = score;
        }

        public int GetSize()
        {
            return _size;
        }

        public decimal GetScore()
        {
            return _score;
        }

        public void Change(int size, int score, Game game)
        {
            if (game.IsStart())
                throw new InvalidOperationException("Игра уже началась.");
            _size = size;
            _score = score;
        }
    }
}