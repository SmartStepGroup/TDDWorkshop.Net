using System;
using System.Collections.Generic;

namespace Domain
{
    public class Game
    {
        List<Player> players = new List<Player>(); 
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
    }
}