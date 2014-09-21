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

        public override void MakeBet(Player player, int coins)
        {
            if (coins <= 0) throw new ArgumentException("Ставка должна быть положительным числом");
            if (coins > 6) throw new ArgumentException("Ставка не должна превышать 6");
            if (inProgress) throw new InvalidOperationException("Нельзя менять ставку во время хода");
            bets[player] = coins;
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
