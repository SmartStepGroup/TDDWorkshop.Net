using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    public class Player
    {
        private Game currentGame;
        private int chipsCount;
        public Guid Id;
        private Bet bet;

        public Player()
        {
            Id = new Guid();
            currentGame = null;
            chipsCount = 0;
        }

        public void Enter(Game game)
        {
            if (currentGame != null) throw new InvalidOperationException("Можешь играть в одну игру ты только, юный падован");
            game.CheckMaxPlayers();

            currentGame = game;
            game.Join();
        }

        public bool IsIn(Game game)
        {
            return currentGame == game;
        }

        public void Exit()
        {
            if (currentGame == null) throw new InvalidOperationException("Не войдя не можешь ты выйти, юный падован");
            currentGame.Leave();
            currentGame = null;
        }

        public void BuyChips(int count)
        {
            chipsCount += count;
        }

        public bool CanDoBets(int chipsInBet = 0)
        {
            return chipsCount >= chipsInBet;
        }

        public bool HasBets()
        {
            return bet != null;
        }

        public void DoBet(Bet m_bet)
        {
            if (currentGame == null) throw new InvalidOperationException("Нельзя делать ставки не находясь в игре");
            if (m_bet.DiceValue < 1 || m_bet.DiceValue > 6) throw new InvalidOperationException("Значение ставки должно быть от 1 до 6");
            if (!CanDoBets(m_bet.ChipsCount)) throw new InvalidOperationException("Ты не можешь фишек больше поставить чем у тебя есть");
            bet = m_bet;
        }

        public void ChangeBet(Bet changedBet)
        {
            if (currentGame.isStarted)
            {
                throw new InvalidOperationException("Нельзя поменять ставку в игре которая уже началась");
            }
            bet = changedBet;
        }
    }
}
