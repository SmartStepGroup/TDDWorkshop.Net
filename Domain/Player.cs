using System;

namespace Domain {
    public class Player {
        private Game activeGame;

        public bool inGame(Game game) {
            return true;
        }

        public void setActiveGame(Game game) {
            if (getActiveGame() != null) {
                throw new InvalidOperationException("Можно играть только в одну игру одновременно");
            }
            activeGame = game;
        }

        public Game getActiveGame() {
            return activeGame;
        }

        public void enter(Game game) {
        }

        public void exit() {
            if (getActiveGame() == null) {
                setActiveGame(null);
            }
            else {
                throw new InvalidOperationException("Нельзя выйти из игры, если в неё не входил");
            }
        }
    }
}