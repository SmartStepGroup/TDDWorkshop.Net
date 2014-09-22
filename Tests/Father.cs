using Domain;

namespace Tests {
    public class Father {
        public PlayerFather Player { get; private set; }
    
        public GameFather Game  { get; private set; }

        public Father() {
            Player = new PlayerFather();
            Game = new GameFather();
        }
    }

    public class GameFather {
    }

    public class PlayerFather {
        private Game game;

        public static implicit operator Player(PlayerFather father) {
            var player = new Player();
            if (father.game != null) player.setActiveGame(father.game);

            return player;
        }

        public PlayerFather In(Game game) {
            this.game = game;
            return this;
        }
    }
}