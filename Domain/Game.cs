using System;
using System.Collections.Generic;

namespace Domain
{
    public abstract class Game
    {
        protected List<Bet> bets = new List<Bet>();
        protected List<Player> players = new List<Player>();

        public abstract bool CanJoin(Player player);
        public abstract void MakeBet(Player player, int roll, int coins);
        public abstract void BeginRound();
        public abstract void EndRound();

        public void Join(Player player)
        {
            if (!CanJoin(player))
                throw new InvalidOperationException("В игре не может быть более " + players.Count + " игроков");
            players.Add(player);
        }

        public void Leave(Player player)
        {
            if (players.Contains(player))
            {
                players.Remove(player);
            }
        }
        
        public int BetsBank()
        {
            int summ = 0;
            foreach (var bet in bets)
            {
                summ += bet.coins;
            }
            return summ;
        }
    }
}