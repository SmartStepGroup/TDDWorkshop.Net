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
        public static implicit operator Game(GameFather father)
        {
            return new Game();
        }
    }

    public class PlayerFather {
        private Game game;
        private int chipsCount;

        public static implicit operator Player(PlayerFather father) {
            var player = new Player();
            if (father.game != null) player.setActiveGame(father.game);
            if (father.chipsCount != null) player.byeChips(father.chipsCount);

            return player;
        }

        public PlayerFather In(Game game) {
            this.game = game;
            return this;
        }

        public PlayerFather With(int cheaps = 5) {
            this.chipsCount = cheaps;
            return this;
        }
    }
}