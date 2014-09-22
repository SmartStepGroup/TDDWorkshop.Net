using System;

namespace Domain {
    public class Player {
        private Game activeGame;
        private int chipsCount;

        public bool inGame(Game game) {
            return true;
        }

        public void setActiveGame(Game game) {
            if (getActiveGame() != null) {
                throw new InvalidOperationException("Можно играть только в одну игру одновременно");
            }
            game.addPlayer();
            activeGame = game;
        }

        public Game getActiveGame() {
            return activeGame;
        }

        public void exit() {
            if (getActiveGame() == null) {
                throw new InvalidOperationException("Нельзя выйти из игры, если в неё не входил");
            }
            activeGame.removePlayer();
            activeGame = null;
         }

        public int getChipsCount() {
            return chipsCount;
        }

        public void byeChips(int count) {
            chipsCount += count;
        }
    }
}