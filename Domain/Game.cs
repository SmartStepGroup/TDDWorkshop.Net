using System;

namespace Domain {
    public class Game {
        private int playerCount;

        public void addPlayer() {
            if (playerCount == 6) {
                throw new InvalidOperationException("���� �� ����� ��������� ����� 6 �������");
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