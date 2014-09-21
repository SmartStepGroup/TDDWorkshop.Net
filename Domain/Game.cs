using System;
using System.Collections.Generic;

namespace Domain
{
    public class Game
    {
        List<Player> players = new List<Player>(); 
        public bool CanJoinPlayer(Player player)
        {
            return players.Count < 6;
        }

        public void JoinPlayer(Player player)
        {
            if (!CanJoinPlayer(player)) throw new InvalidOperationException("В игре не может быть более " + players.Count + " игроков");
            players.Add(player);
        }
    }
}