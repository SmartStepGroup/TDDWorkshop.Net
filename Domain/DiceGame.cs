using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class DiceGame:Game
    {
        protected bool inProgress = false;
        public override bool CanJoin(Player player)
        {
            return players.Count < 6;
        }

        public override void Reset()
        {
            players.Clear();
            bets.Clear();
        }
        public override void MakeBet(Player player, int roll,int coins)
        {
            if (roll <= 0) throw new ArgumentException("Ставка должна быть положительным числом");
            if (roll > 6) throw new ArgumentException("Ставка не должна превышать 6");
            if (inProgress) throw new InvalidOperationException("Нельзя менять ставку во время хода");
            bets.Add(new Bet(player,roll,coins));
        }

        public override void BeginRound()
        {
            inProgress = true;
        }

        public override void EndRound()
        {
            inProgress = false;
        }
    }
}
