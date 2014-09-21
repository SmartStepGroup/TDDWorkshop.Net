using System;

namespace Domain {
    public class Game {
        private int playerCount;

        public void addPlayer() {
            if (playerCount == 6) {
                throw new InvalidOperationException("Игра не может допустить более 6 игроков");
            }
            playerCount++;
        }

        public int getPlayersCount() {
            return playerCount;
        }

        public void removePlayer() {
            playerCount--;
        }
    }
}