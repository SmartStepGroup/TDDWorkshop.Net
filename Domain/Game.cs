using System;
using System.Collections.Generic;

namespace Domain
{
    public class Game
    {
        List<Player> players = new List<Player>();
        public Dictionary<Player, int> bets = new Dictionary<Player, int>();

        public bool CanJoin(Player player)
        {
            return players.Count < 6;
        }

        public void Join(Player player)
        {
            if (!CanJoin(player)) throw new InvalidOperationException("В игре не может быть более " + players.Count + " игроков");
            players.Add(player);
        }

        public void Leave(Player player)
        {
            if (players.Contains(player))
            {
                players.Remove(player);
            }
        }

        public void MakeBet(Player player, int coins)
        {
            bets[player] = coins;
        }

        public int BetsBank()
        {
            int summ = 0;
            foreach (Player player in bets.Keys)
            {
                summ += bets[player];
            }
            return summ;
        }
    }
}