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
        private List<Bet> bets = new List<Bet>();
        private bool isStarted = false;

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

        public void DoBet(Bet bet)
        {
            if (!bet.GetOwner().IsIn(this)) throw new InvalidOperationException("Нельзя делать ставки не находясь в игре");
            if (bet.DiceValue < 1 || bet.DiceValue > 6) throw new InvalidOperationException("Значение ставки должно быть от 1 до 6");
            if (!bet.GetOwner().CanDoBets(bet.ChipsCount)) throw new InvalidOperationException("Ты не можешь фишек больше поставить чем у тебя есть");

            bets.Add(bet);
        }

        public List<Bet> GetBets()
        {
            return bets;
        }

        public void Start()
        {
            isStarted = true;
        }

        public void ChangeBet(Guid id, Bet changedBet)
        {
            if (isStarted)
            {
                throw new InvalidOperationException("Нельзя поменять ставку в игре которая уже началась");
            }
            foreach (var bet in bets)
            {
                if (bet.Id == id)
                {
                    Bet bufBet = bet;
                    bufBet = changedBet;
                    bufBet.Id = id;
                }
            }
        }
    }
}
